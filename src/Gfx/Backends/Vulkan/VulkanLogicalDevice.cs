using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.Maths;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using Semaphore = Silk.NET.Vulkan.Semaphore;

namespace Gfx;

// TODO - this is just for graphical device, not for compute
public unsafe class VulkanLogicalDevice : LogicalDevice
{
	private const    float _queuePriority     = 1f; // from every queue family we need just one queue (priority: 0..1)
	private readonly int   _maxFramesInFlight;      // max number of frames that can be rendered at the same time
	
	private readonly VulkanApi            _api;
	private readonly VulkanPhysicalDevice _physicalDevice;
    
	internal readonly Device Device;
	private readonly Queue  _graphicsQueue;
	private readonly Queue  _presentQueue;

	internal readonly SampleCountFlags MsaaSampleCount;
	
	private  KhrSwapchain? _khrSwapChain;
	private  SwapchainKHR  _swapChain;
	private  Image[]?      _swapChainImages;
	internal Format        SwapChainImageFormat;
	internal Format        SwapChainDepthStencilFormat;
	private  Extent2D      _swapChainExtent;
	private  ImageView[]?  _swapChainImageViews;
	//private Framebuffer[]? _swapChainFramebuffers;
	internal bool HasDepthStencil => SwapChainDepthStencilFormat != Format.Undefined;

	private Image        _colorImage;
	private DeviceMemory _colorImageMemory;
	private ImageView    _colorImageView;

	private Image        _depthImage;
	private DeviceMemory _depthImageMemory;
	private ImageView    _depthImageView;
	
	private CommandPool _commandPool;
    
	private Semaphore[]? _imageAvailableSemaphores;
	private Semaphore[]? _renderFinishedSemaphores;
	private Fence[]?     _inFlightFences;
	private Fence[]?     _imagesInFlight;
	
	internal VulkanLogicalDevice(VulkanApi api, LogicalDeviceOptions options)
	{
		_api               = api;
		_physicalDevice    = (VulkanPhysicalDevice)options.PhysicalDevice;
		_maxFramesInFlight = options.MaxFramesInFlight;

		InitDeviceAndQueues(out Device, out _graphicsQueue, out _presentQueue);
		InitMsaaSampleCount(out MsaaSampleCount, options.MsaaSampleCount);
		InitSwapChain(options.FrameBufferFormat, options.NeedDepthStencil, ref _khrSwapChain, ref _swapChain, ref _swapChainImages, ref SwapChainImageFormat, ref SwapChainDepthStencilFormat, ref _swapChainExtent);
		InitImageViews(out _swapChainImageViews);
		//InitFrameBuffers(out _swapChainFramebuffers);
		InitColorResources(ref _colorImage, ref _colorImageMemory, ref _colorImageView);
		InitDepthResources(ref _depthImage, ref _depthImageMemory, ref _depthImageView);
		InitCommandPool(out _commandPool);
		InitSyncObjects(out _imageAvailableSemaphores, out _renderFinishedSemaphores, out _inFlightFences, out _imagesInFlight);
	}

	public void DisposeSwapChain()
	{
		if (HasDepthStencil)
		{
			_api.Vk.DestroyImageView(Device, _depthImageView, null);
			_api.Vk.DestroyImage(Device, _depthImage, null);
			_api.Vk.FreeMemory(Device, _depthImageMemory, null);
		}

		// foreach (var framebuffer in _swapChainFramebuffers!)
		// {
		// 	_api.Vk!.DestroyFramebuffer(_device, framebuffer, null);
		// }
		
		foreach (var imageView in _swapChainImageViews!)
		{
			_api.Vk.DestroyImageView(Device, imageView, null);
		}

		_khrSwapChain!.DestroySwapchain(Device, _swapChain, null);

		// for (int i = 0; i < _swapChainImages!.Length; i++)
		// {
		// 	_api.Vk.DestroyBuffer(_device, uniformBuffers![i], null);
		// 	_api.Vk.FreeMemory(_device, uniformBuffersMemory![i], null);
		// }
	}

	public override void Dispose()
	{
		DisposeSwapChain();

		for (int i = 0; i < _maxFramesInFlight; ++i)
		{
			_api.Vk.DestroySemaphore(Device, _renderFinishedSemaphores![i], null);
			_api.Vk.DestroySemaphore(Device, _imageAvailableSemaphores![i], null);
			_api.Vk.DestroyFence(Device, _inFlightFences![i], null);
		}
		
		_api.Vk.DestroyCommandPool(Device, _commandPool, null);
		
		_api.Vk.DestroyDevice(Device, null);
	}

	public override void WaitIdle()
	{
		_api.Vk.DeviceWaitIdle(Device);
	}

	public override RenderPass CreateRenderPass(RenderPassOptions options)
	{
		return new VulkanRenderPass(_api, this, options);
	}

	#region Initialization
	private void InitDeviceAndQueues(out Device device, out Queue graphicsQueue, out Queue presentQueue)
	{
		uint[] uniqueQueueFamilies = { _physicalDevice.GraphicsQueueFamily!.Value, _physicalDevice.PresentQueueFamily!.Value };
		uniqueQueueFamilies = uniqueQueueFamilies.Distinct().ToArray();

		using var mem              = GlobalMemory.Allocate(uniqueQueueFamilies.Length * sizeof(DeviceQueueCreateInfo));
		var       queueCreateInfos = (DeviceQueueCreateInfo*)Unsafe.AsPointer(ref mem.GetPinnableReference());

		float queuePriority = _queuePriority;
		for (int i = 0; i < uniqueQueueFamilies.Length; i++)
		{
			queueCreateInfos[i] = new()
			                      {
				                      SType            = StructureType.DeviceQueueCreateInfo,
				                      QueueFamilyIndex = uniqueQueueFamilies[i],
				                      QueueCount       = 1,
				                      PQueuePriorities = &queuePriority
			                      };
		}

		PhysicalDeviceFeatures deviceFeatures = new()
		                                        {
			                                        SamplerAnisotropy = true,
		                                        };

		DeviceCreateInfo createInfo = new()
		                              {
			                              SType                = StructureType.DeviceCreateInfo,
			                              QueueCreateInfoCount = (uint)uniqueQueueFamilies.Length,
			                              PQueueCreateInfos    = queueCreateInfos,

			                              PEnabledFeatures = &deviceFeatures,

			                              EnabledExtensionCount   = (uint)VulkanExtensions.GraphicsExtensions.Length,
			                              PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanExtensions.GraphicsExtensions)
		                              };

		if (_api.IsDebugEnabled)
		{
			createInfo.EnabledLayerCount   = (uint)VulkanValidationLayers.DebugValidationLayers.Length;
			createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanValidationLayers.DebugValidationLayers);
		}
		else
		{
			createInfo.EnabledLayerCount = 0;
		}

		if (_api.Vk.CreateDevice(_physicalDevice.Device, in createInfo, null, out device) != Result.Success)
		{
			throw new GfxException("Failed to create logical device!");
		}

		_api.Vk.GetDeviceQueue(Device, _physicalDevice.GraphicsQueueFamily!.Value, 0, out graphicsQueue);
		_api.Vk.GetDeviceQueue(Device, _physicalDevice.PresentQueueFamily!.Value,  0, out presentQueue);

		if (_api.IsDebugEnabled)
		{
			SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
		}

		SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
	}
	
	private void InitMsaaSampleCount(out SampleCountFlags msaaSampleCount, SampleCount desiredMsaaSampleCount)
	{
		SampleCountFlags physicalMax = _physicalDevice.GetMaxMsaaSamplesCount();
		SampleCountFlags desired     = desiredMsaaSampleCount.ToVulkan();
		msaaSampleCount = physicalMax < desired ? physicalMax : desired;
	}
	
	private void InitSwapChain(
		ImageFormat       desiredFormat,
		bool              needDepthStencil,
		ref KhrSwapchain? khrSwapChain,
		ref SwapchainKHR  swapChain,
		ref Image[]?      swapChainImages,
		ref Format        swapChainImageFormat,
		ref Format        swapChainDepthStencilFormat,
		ref Extent2D      swapChainExtent
	)
	{
		VulkanSwapChainSupportDetails swapChainSupport = _physicalDevice.SwapChainSupportDetails!;

		var surfaceFormat = ChooseSwapSurfaceFormat(swapChainSupport.Formats, desiredFormat.ToVulkan());
		var presentMode   = ChoosePresentMode(swapChainSupport.PresentModes);
		var extent        = ChooseSwapExtent(swapChainSupport.Capabilities);

		var imageCount = swapChainSupport.Capabilities.MinImageCount + 1; // TODO - required image count from options
		if (swapChainSupport.Capabilities.MaxImageCount > 0 && imageCount > swapChainSupport.Capabilities.MaxImageCount)
		{
			imageCount = swapChainSupport.Capabilities.MaxImageCount;
		}

		SwapchainCreateInfoKHR createInfo = new()
		                                    {
			                                    SType   = StructureType.SwapchainCreateInfoKhr,
			                                    Surface = _api.Surface,

			                                    MinImageCount    = imageCount,
			                                    ImageFormat      = surfaceFormat.Format,
			                                    ImageColorSpace  = surfaceFormat.ColorSpace,
			                                    ImageExtent      = extent,
			                                    ImageArrayLayers = 1,
			                                    ImageUsage       = ImageUsageFlags.ColorAttachmentBit,
		                                    };

		uint* queueFamilyIndices = stackalloc[] {_physicalDevice.GraphicsQueueFamily!.Value, _physicalDevice.PresentQueueFamily!.Value};
		if (_physicalDevice.GraphicsQueueFamily != _physicalDevice.PresentQueueFamily)
		{
			createInfo = createInfo with
			             {
				             ImageSharingMode = SharingMode.Concurrent,
				             QueueFamilyIndexCount = 2,
				             PQueueFamilyIndices = queueFamilyIndices,
			             };
		}
		else
		{
			createInfo.ImageSharingMode = SharingMode.Exclusive;
		}

		createInfo = createInfo with
		             {
			             PreTransform = swapChainSupport.Capabilities.CurrentTransform,
			             CompositeAlpha = CompositeAlphaFlagsKHR.OpaqueBitKhr,
			             PresentMode = presentMode,
			             Clipped = true,
		             };

		if (khrSwapChain is null)
		{
			if (!_api.Vk.TryGetDeviceExtension(_api.Instance, Device, out khrSwapChain))
			{
				throw new NotSupportedException($"{KhrSwapchain.ExtensionName} extension not found.");
			}
		}

		if (khrSwapChain!.CreateSwapchain(Device, createInfo, null, out swapChain) != Result.Success)
		{
			throw new GfxException("Failed to create swap chain!");
		}

		khrSwapChain.GetSwapchainImages(Device, swapChain, ref imageCount, null);
		swapChainImages = new Image[imageCount];
		fixed (Image* swapChainImagesPtr = swapChainImages)
		{
			khrSwapChain.GetSwapchainImages(Device, swapChain, ref imageCount, swapChainImagesPtr);
		}

		swapChainImageFormat = surfaceFormat.Format;
		swapChainExtent      = extent;

		swapChainDepthStencilFormat =
			needDepthStencil
				? _physicalDevice.FindSupportedFormat(new[] {Format.D32Sfloat, Format.D32SfloatS8Uint, Format.D24UnormS8Uint}, ImageTiling.Optimal, FormatFeatureFlags.DepthStencilAttachmentBit)
				: Format.Undefined;
	}

	private void InitImageViews(out ImageView[]? imageViews)
	{
		imageViews = new ImageView[_swapChainImages!.Length];

		for (int i = 0; i < _swapChainImages.Length; i++)
		{
			imageViews[i] = CreateImageView(_swapChainImages[i], SwapChainImageFormat, ImageAspectFlags.ColorBit, 1);
		}
	}
	
	// TODO - this has dependency on render pass, cannot be used as simple init function hidden inside device
	/*
	private void InitFrameBuffers(out Framebuffer[]? frameBuffers)
	{
		frameBuffers = new Framebuffer[_swapChainImageViews!.Length];

		for (int i = 0; i < _swapChainImageViews.Length; i++)
		{
			ImageView attachment = _swapChainImageViews[i];

			FramebufferCreateInfo framebufferInfo = new()
			                                        {
				                                        SType           = StructureType.FramebufferCreateInfo,
				                                        RenderPass      = renderPass,
				                                        AttachmentCount = 1,
				                                        PAttachments    = &attachment,
				                                        Width           = _swapChainExtent.Width,
				                                        Height          = _swapChainExtent.Height,
				                                        Layers          = 1,
			                                        };

			if (_api.Vk.CreateFramebuffer(_device, framebufferInfo, null, out frameBuffers[i]) != Result.Success)
			{
				throw new Exception("failed to create framebuffer!");
			}
		}
	}
	*/

	private void InitColorResources(ref Image colorImage, ref DeviceMemory colorImageMemory, ref ImageView colorImageView)
	{
		Format colorFormat = SwapChainImageFormat;
	
		CreateImage(_swapChainExtent.Width, _swapChainExtent.Height, 1, MsaaSampleCount, colorFormat, ImageTiling.Optimal, ImageUsageFlags.TransientAttachmentBit | ImageUsageFlags.ColorAttachmentBit, MemoryPropertyFlags.DeviceLocalBit, ref colorImage, ref colorImageMemory);
		colorImageView = CreateImageView(colorImage, colorFormat, ImageAspectFlags.ColorBit, 1);
	}
	
	private void InitDepthResources(ref Image depthImage, ref DeviceMemory depthImageMemory, ref ImageView depthImageView)
	{
		if (SwapChainDepthStencilFormat == Format.Undefined)
		{
			depthImage       = default;
			depthImageMemory = default;
			depthImageView   = default;
			return;
		}

		Format depthFormat = SwapChainDepthStencilFormat;
	
		CreateImage(_swapChainExtent.Width, _swapChainExtent.Height, 1, MsaaSampleCount, depthFormat, ImageTiling.Optimal, ImageUsageFlags.DepthStencilAttachmentBit, MemoryPropertyFlags.DeviceLocalBit, ref depthImage, ref depthImageMemory);
		depthImageView = CreateImageView(depthImage, depthFormat, ImageAspectFlags.DepthBit, 1);
	}

	private void InitCommandPool(out CommandPool commandPool)
	{
		CommandPoolCreateInfo poolInfo = new()
		                                 {
			                                 SType            = StructureType.CommandPoolCreateInfo,
			                                 QueueFamilyIndex = _physicalDevice.GraphicsQueueFamily!.Value,
		                                 };

		if (_api.Vk.CreateCommandPool(Device, poolInfo, null, out commandPool) != Result.Success)
		{
			throw new Exception("failed to create command pool!");
		}
	}

	private void InitSyncObjects(
		out Semaphore[]? imageAvailableSemaphores,
		out Semaphore[]? renderFinishedSemaphores,
		out Fence[]?     inFlightFences,
		out Fence[]?     imagesInFlight
	)
	{
		imageAvailableSemaphores = new Semaphore[_maxFramesInFlight];
		renderFinishedSemaphores = new Semaphore[_maxFramesInFlight];
		inFlightFences           = new Fence[_maxFramesInFlight];
		imagesInFlight           = new Fence[_swapChainImages!.Length];

		SemaphoreCreateInfo semaphoreInfo = new()
		                                    {
			                                    SType = StructureType.SemaphoreCreateInfo,
		                                    };

		FenceCreateInfo fenceInfo = new()
		                            {
			                            SType = StructureType.FenceCreateInfo,
			                            Flags = FenceCreateFlags.SignaledBit,
		                            };

		for (var i = 0; i < _maxFramesInFlight; i++)
		{
			if (_api.Vk.CreateSemaphore(Device, semaphoreInfo, null, out imageAvailableSemaphores[i]) != Result.Success ||
			    _api.Vk.CreateSemaphore(Device, semaphoreInfo, null, out renderFinishedSemaphores[i]) != Result.Success ||
			    _api.Vk.CreateFence(Device, fenceInfo, null, out inFlightFences[i])                   != Result.Success)
			{
				throw new GfxException("Failed to create synchronization objects for a frame!");
			}
		}
	}
	#endregion
	
	#region Initialization helpers
	private SurfaceFormatKHR ChooseSwapSurfaceFormat(IReadOnlyList<SurfaceFormatKHR> availableFormats, Format desiredFormat)
	{
		foreach (var availableFormat in availableFormats)
		{
			if (availableFormat.Format == desiredFormat && availableFormat.ColorSpace == ColorSpaceKHR.SpaceSrgbNonlinearKhr)
			{
				return availableFormat;
			}
		}

		return availableFormats[0];
	}

	private PresentModeKHR ChoosePresentMode(IReadOnlyList<PresentModeKHR> availablePresentModes)
	{
		foreach (var availablePresentMode in availablePresentModes)
		{
			if (availablePresentMode == PresentModeKHR.MailboxKhr)
			{
				return availablePresentMode;
			}
		}

		return PresentModeKHR.FifoKhr;
	}

	private Extent2D ChooseSwapExtent(SurfaceCapabilitiesKHR capabilities)
	{
		if (capabilities.CurrentExtent.Width != uint.MaxValue)
		{
			return capabilities.CurrentExtent;
		}

		Vector2D<int> frameBufferSize = _api.Window.FramebufferSize;

		Extent2D actualExtent = new()
		                        {
			                        Width  = (uint)frameBufferSize.X,
			                        Height = (uint)frameBufferSize.Y
		                        };

		actualExtent.Width  = Math.Clamp(actualExtent.Width,  capabilities.MinImageExtent.Width,  capabilities.MaxImageExtent.Width);
		actualExtent.Height = Math.Clamp(actualExtent.Height, capabilities.MinImageExtent.Height, capabilities.MaxImageExtent.Height);

		return actualExtent;
	}
	
	private ImageView CreateImageView(Image image, Format format, ImageAspectFlags aspectFlags, uint mipLevels)
	{
		ImageViewCreateInfo createInfo = new()
		                                 {
			                                 SType    = StructureType.ImageViewCreateInfo,
			                                 Image    = image,
			                                 ViewType = ImageViewType.Type2D,
			                                 Format   = format,
			                                 //Components =
			                                 //    {
			                                 //        R = ComponentSwizzle.Identity,
			                                 //        G = ComponentSwizzle.Identity,
			                                 //        B = ComponentSwizzle.Identity,
			                                 //        A = ComponentSwizzle.Identity,
			                                 //    },
			                                 SubresourceRange =
			                                 {
				                                 AspectMask     = aspectFlags,
				                                 BaseMipLevel   = 0,
				                                 LevelCount     = mipLevels,
				                                 BaseArrayLayer = 0,
				                                 LayerCount     = 1,
			                                 }
		                                 };

		if (_api.Vk.CreateImageView(Device, createInfo, null, out ImageView imageView) != Result.Success)
		{
			throw new GfxException("Failed to create image view!");
		}

		return imageView;
	}
	
	private void CreateImage(uint width, uint height, uint mipLevels, SampleCountFlags numSamples, Format format, ImageTiling tiling, ImageUsageFlags usage, MemoryPropertyFlags properties, ref Image image, ref DeviceMemory imageMemory)
	{
		ImageCreateInfo imageInfo = new()
		                            {
			                            SType     = StructureType.ImageCreateInfo,
			                            ImageType = ImageType.Type2D,
			                            Extent =
			                            {
				                            Width  = width,
				                            Height = height,
				                            Depth  = 1,
			                            },
			                            MipLevels     = mipLevels,
			                            ArrayLayers   = 1,
			                            Format        = format,
			                            Tiling        = tiling,
			                            InitialLayout = ImageLayout.Undefined,
			                            Usage         = usage,
			                            Samples       = numSamples,
			                            SharingMode   = SharingMode.Exclusive,
		                            };

		fixed (Image* imagePtr = &image)
		{
			if (_api.Vk.CreateImage(Device, imageInfo, null, imagePtr) != Result.Success)
			{
				throw new GfxException("Failed to create image!");
			}
		}

		_api.Vk.GetImageMemoryRequirements(Device, image, out MemoryRequirements memRequirements);

		MemoryAllocateInfo allocInfo = new()
		                               {
			                               SType           = StructureType.MemoryAllocateInfo,
			                               AllocationSize  = memRequirements.Size,
			                               MemoryTypeIndex = FindMemoryType(memRequirements.MemoryTypeBits, properties),
		                               };

		fixed (DeviceMemory* imageMemoryPtr = &imageMemory)
		{
			if (_api.Vk.AllocateMemory(Device, allocInfo, null, imageMemoryPtr) != Result.Success)
			{
				throw new GfxException("Failed to allocate image memory!");
			}
		}

		_api.Vk.BindImageMemory(Device, image, imageMemory, 0);
	}

	private uint FindMemoryType(uint typeFilter, MemoryPropertyFlags properties)
	{
		_api.Vk.GetPhysicalDeviceMemoryProperties(_physicalDevice.Device, out PhysicalDeviceMemoryProperties memProperties);

		for (int i = 0; i < memProperties.MemoryTypeCount; i++)
		{
			if ((typeFilter & (1 << i)) != 0 && (memProperties.MemoryTypes[i].PropertyFlags & properties) == properties)
			{
				return (uint)i;
			}
		}

		throw new GfxException("Failed to find suitable memory type!");
	}
	#endregion
}