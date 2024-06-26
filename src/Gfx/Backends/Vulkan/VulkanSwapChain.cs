using Silk.NET.Maths;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using VkFormat = Silk.NET.Vulkan.Format;
using VkDeviceMemory = Silk.NET.Vulkan.DeviceMemory;
using VkSemaphore = Silk.NET.Vulkan.Semaphore;
using VkSharingMode = Silk.NET.Vulkan.SharingMode;

namespace Gfx;

public sealed unsafe class VulkanSwapChain : SwapChain
{
	private readonly int _framesInFlight;      // max number of frames that can be rendered at the same time
	
	private readonly VulkanApi            _api;
	private readonly VulkanPhysicalDevice _physicalDevice;
	private readonly VulkanLogicalDevice  _logicalDevice;

	internal readonly SampleCountFlags MsaaSampleCount;

	private  KhrSwapchain?  _khrSwapChain;
	private  SwapchainKHR   _swapChain;
	private  Image[]?       _swapChainImages;
	internal VkFormat       SwapChainImageFormat;
	internal VkFormat       SwapChainDepthStencilFormat;
	private  Extent2D       _swapChainExtent;
	private  ImageView[]?   _swapChainImageViews;
	internal Framebuffer[]? SwapChainFramebuffers;
	
	private Image          _colorImage;
	private VkDeviceMemory _colorImageMemory;
	private ImageView      _colorImageView;

	private Image          _depthImage;
	private VkDeviceMemory _depthImageMemory;
	private ImageView      _depthImageView;

	private VkSemaphore[]? _imageAvailableSemaphores;
	private VkSemaphore[]? _renderFinishedSemaphores;
	private Fence[]?       _framesInFlightFences;
	//private Fence[]?       _imagesInFlightFences;

	private VulkanRenderPass? _renderPass;
	
	internal bool HasDepthStencil => SwapChainDepthStencilFormat != VkFormat.Undefined;

	public override uint Width          => _swapChainExtent.Width;
	public override uint Height         => _swapChainExtent.Height;
	public override int  FramesInFlight => _framesInFlight; 
	
	public VulkanSwapChain(VulkanApi api, VulkanPhysicalDevice physicalDevice, VulkanLogicalDevice logicalDevice, SwapChainOptions options)
	{
		_api               = api;
		_physicalDevice    = physicalDevice;
		_logicalDevice     = logicalDevice;

		InitMsaaSampleCount(out MsaaSampleCount, options.MsaaSampleCount);
		InitSwapChain((uint)options.MaxFramesInFlight, options.NeedDepthStencil, out _framesInFlight, ref _khrSwapChain, ref _swapChain, ref _swapChainImages, ref SwapChainImageFormat, ref SwapChainDepthStencilFormat, ref _swapChainExtent);
		InitImageViews(out _swapChainImageViews);
		InitRenderPass(out _renderPass);
		InitColorResources(ref _colorImage, ref _colorImageMemory, ref _colorImageView);
		InitDepthResources(ref _depthImage, ref _depthImageMemory, ref _depthImageView);
		InitFrameBuffers(out SwapChainFramebuffers);
		InitSyncObjects(out _imageAvailableSemaphores, out _renderFinishedSemaphores, out _framesInFlightFences);
	}

	public void DisposeOnResized()
	{
		if (HasDepthStencil)
		{
			_api.Vk.DestroyImageView(_logicalDevice.Device, _depthImageView, null);
			_api.Vk.DestroyImage(_logicalDevice.Device, _depthImage, null);
			_api.Vk.FreeMemory(_logicalDevice.Device, _depthImageMemory, null);
		}

		_api.Vk.DestroyImageView(_logicalDevice.Device, _colorImageView, null);
		_api.Vk.DestroyImage(_logicalDevice.Device, _colorImage, null);
		_api.Vk.FreeMemory(_logicalDevice.Device, _colorImageMemory, null);

		foreach (var framebuffer in SwapChainFramebuffers!)
		{
			_api.Vk.DestroyFramebuffer(_logicalDevice.Device, framebuffer, null);
		}

		_renderPass?.Dispose();
		
		foreach (var imageView in _swapChainImageViews!)
		{
			_api.Vk.DestroyImageView(_logicalDevice.Device, imageView, null);
		}

		_khrSwapChain!.DestroySwapchain(_logicalDevice.Device, _swapChain, null);

		// for (int i = 0; i < _swapChainImages!.Length; i++)
		// {
		// 	_api.Vk.DestroyBuffer(_logicalDevice.Device, uniformBuffers![i], null);
		// 	_api.Vk.FreeMemory(_logicalDevice.Device, uniformBuffersMemory![i], null);
		// }
	}

	public override void Dispose()
	{
		DisposeOnResized();

		for (int i = 0; i < _framesInFlight; ++i)
		{
			_api.Vk.DestroySemaphore(_logicalDevice.Device, _renderFinishedSemaphores![i], null);
			_api.Vk.DestroySemaphore(_logicalDevice.Device, _imageAvailableSemaphores![i], null);
			_api.Vk.DestroyFence(_logicalDevice.Device, _framesInFlightFences![i], null);
		}
	}

	public override void WaitForFence(int swapChainBufferIndex, ulong timeout = ulong.MaxValue)
	{
		_api.Vk.WaitForFences(_logicalDevice.Device, 1, _framesInFlightFences[swapChainBufferIndex], true, timeout);
	}

	public override void ResetFence(int swapChainBufferIndex)
	{
		_api.Vk.ResetFences(_logicalDevice.Device, 1, _framesInFlightFences[swapChainBufferIndex]);
	}

	public override GfxResult AcquireNextImage(int swapChainBufferIndex, ref uint imageIndex, ulong timeout = ulong.MaxValue)
	{
		return _khrSwapChain.AcquireNextImage(_logicalDevice.Device, _swapChain, timeout, _imageAvailableSemaphores[swapChainBufferIndex], default, ref imageIndex).ToGfx();
	}

	public override void Submit(int swapChainBufferIndex, CommandBuffer commandBuffer)
	{
		var waitSemaphores   = stackalloc[] {_imageAvailableSemaphores[swapChainBufferIndex]};
		var waitStages       = stackalloc[] {PipelineStageFlags.ColorAttachmentOutputBit}; // TODO - customizable?
		var signalSemaphores = stackalloc[] {_renderFinishedSemaphores[swapChainBufferIndex]};
		var buffer           = ((VulkanCommandBuffer) commandBuffer).CommandBuffer;

		SubmitInfo submitInfo = new()
		                        {
			                        SType                = StructureType.SubmitInfo,
			                        WaitSemaphoreCount   = 1,
			                        PWaitSemaphores      = waitSemaphores,
			                        PWaitDstStageMask    = waitStages,
			                        CommandBufferCount   = 1,
			                        PCommandBuffers      = &buffer,
			                        SignalSemaphoreCount = 1,
			                        PSignalSemaphores    = signalSemaphores,
		                        };

		Result result = _api.Vk.QueueSubmit(_logicalDevice.GraphicsQueue, 1, submitInfo, _framesInFlightFences[swapChainBufferIndex]);
		if (result != Result.Success)
		{
			throw new GfxException($"Failed to submit draw command buffer! Result: {result}");
		}
	}

	public override void Present(int swapChainBufferIndex, uint imageIndex)
	{
		var swapChains       = stackalloc[] {_swapChain};
		var signalSemaphores = stackalloc[] {_renderFinishedSemaphores[swapChainBufferIndex]};
		
		PresentInfoKHR presentInfo = new()
		                             {
			                             SType = StructureType.PresentInfoKhr,

			                             WaitSemaphoreCount = 1,
			                             PWaitSemaphores    = signalSemaphores,

			                             SwapchainCount = 1,
			                             PSwapchains    = swapChains,

			                             PImageIndices = &imageIndex
		                             };

		Result result = _khrSwapChain.QueuePresent(_logicalDevice.PresentQueue, presentInfo);

		// TODO
		// if (result == Result.ErrorOutOfDateKhr || result == Result.SuboptimalKhr || frameBufferResized)
		// {
		// 	frameBufferResized = false;
		// 	RecreateSwapChain();
		// }
		// else
		if (result != Result.Success)
		{
			throw new GfxException($"Failed to present swap chain image! Result:{result}");
		}
	}

	#region Initialization
	private void InitMsaaSampleCount(out SampleCountFlags msaaSampleCount, SampleCount desiredMsaaSampleCount)
	{
		SampleCountFlags physicalMax = _physicalDevice.GetMaxMsaaSamplesCount();
		SampleCountFlags desired     = desiredMsaaSampleCount.ToVulkan();
		msaaSampleCount = physicalMax < desired ? physicalMax : desired;
	}

	private void InitSwapChain(
		uint              desiredImageCount,
		bool              needDepthStencil,
		out int framesInFlight,
		ref KhrSwapchain? khrSwapChain,
		ref SwapchainKHR  swapChain,
		ref Image[]?      swapChainImages,
		ref VkFormat      swapChainImageFormat,
		ref VkFormat      swapChainDepthStencilFormat,
		ref Extent2D      swapChainExtent
	)
	{
		VulkanSwapChainSupportDetails swapChainSupport = _physicalDevice.SwapChainSupportDetails!;

		var surfaceFormat = ChooseSwapSurfaceFormat(swapChainSupport.Formats);
		var presentMode   = ChoosePresentMode(swapChainSupport.PresentModes);
		var extent        = ChooseSwapExtent(swapChainSupport.Capabilities);

		uint imageCount = desiredImageCount;
		if (imageCount < swapChainSupport.Capabilities.MinImageCount)
		{
			imageCount = swapChainSupport.Capabilities.MinImageCount;
		}
		if (swapChainSupport.Capabilities.MaxImageCount > 0 && imageCount > swapChainSupport.Capabilities.MaxImageCount)
		{
			imageCount = swapChainSupport.Capabilities.MaxImageCount;
		}

		framesInFlight = (int) imageCount;

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
				             ImageSharingMode = VkSharingMode.Concurrent,
				             QueueFamilyIndexCount = 2,
				             PQueueFamilyIndices = queueFamilyIndices,
			             };
		}
		else
		{
			createInfo.ImageSharingMode = VkSharingMode.Exclusive;
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
			if (!_api.Vk.TryGetDeviceExtension(_api.Instance, _logicalDevice.Device, out khrSwapChain))
			{
				throw new NotSupportedException($"{KhrSwapchain.ExtensionName} extension not found.");
			}
		}

		if (khrSwapChain!.CreateSwapchain(_logicalDevice.Device, createInfo, null, out swapChain) != Result.Success)
		{
			throw new GfxException("Failed to create swap chain!");
		}

		khrSwapChain.GetSwapchainImages(_logicalDevice.Device, swapChain, ref imageCount, null);
		swapChainImages = new Image[imageCount];
		fixed (Image* swapChainImagesPtr = swapChainImages)
		{
			khrSwapChain.GetSwapchainImages(_logicalDevice.Device, swapChain, ref imageCount, swapChainImagesPtr);
		}

		swapChainImageFormat = surfaceFormat.Format;
		swapChainExtent      = extent;

		swapChainDepthStencilFormat =
			needDepthStencil
				? _physicalDevice.FindSupportedFormat(new[] {VkFormat.D32Sfloat, VkFormat.D32SfloatS8Uint, VkFormat.D24UnormS8Uint}, ImageTiling.Optimal, FormatFeatureFlags.DepthStencilAttachmentBit)
				: VkFormat.Undefined;
	}

	private void InitImageViews(out ImageView[]? imageViews)
	{
		imageViews = new ImageView[_swapChainImages!.Length];

		for (int i = 0; i < _swapChainImages.Length; i++)
		{
			imageViews[i] = CreateImageView(_swapChainImages[i], SwapChainImageFormat, ImageAspectFlags.ColorBit, 1);
		}
	}
	
	private void InitFrameBuffers(out Framebuffer[]? frameBuffers)
	{
		frameBuffers = new Framebuffer[_swapChainImageViews!.Length];

		for (int i = 0; i < _swapChainImageViews.Length; i++)
		{
			var attachments = new[] {_colorImageView, _depthImageView, _swapChainImageViews[i]};

			fixed (ImageView* attachmentsPtr = attachments)
			{
				//ImageView attachment = _swapChainImageViews[i];

				FramebufferCreateInfo framebufferInfo = new()
				                                        {
					                                        SType      = StructureType.FramebufferCreateInfo,
					                                        RenderPass = _renderPass.RenderPass,
					                                        //AttachmentCount = 1,
					                                        //PAttachments    = &attachment,
					                                        AttachmentCount = (uint)attachments.Length,
					                                        PAttachments    = attachmentsPtr,
					                                        Width           = _swapChainExtent.Width,
					                                        Height          = _swapChainExtent.Height,
					                                        Layers          = 1,
				                                        };

				if (_api.Vk.CreateFramebuffer(_logicalDevice.Device, framebufferInfo, null, out frameBuffers[i]) != Result.Success)
				{
					throw new GfxException("Failed to create frame buffer!");
				}
			}
		}
	}

	private void InitColorResources(ref Image colorImage, ref VkDeviceMemory colorImageMemory, ref ImageView colorImageView)
	{
		VkFormat colorFormat = SwapChainImageFormat;
	
		CreateImage(_swapChainExtent.Width,
			_swapChainExtent.Height,
			1,
			MsaaSampleCount,
			colorFormat,
			ImageTiling.Optimal,
			ImageLayout.Undefined,
			ImageUsageFlags.TransientAttachmentBit | ImageUsageFlags.ColorAttachmentBit,
			MemoryPropertyFlags.DeviceLocalBit,
			ref colorImage,
			ref colorImageMemory);
		colorImageView = CreateImageView(colorImage, colorFormat, ImageAspectFlags.ColorBit, 1);
	}
	
	private void InitDepthResources(ref Image depthImage, ref VkDeviceMemory depthImageMemory, ref ImageView depthImageView)
	{
		if (SwapChainDepthStencilFormat == VkFormat.Undefined)
		{
			depthImage       = default;
			depthImageMemory = default;
			depthImageView   = default;
			return;
		}

		VkFormat depthFormat = SwapChainDepthStencilFormat;
	
		CreateImage(_swapChainExtent.Width,
			_swapChainExtent.Height,
			1,
			MsaaSampleCount,
			depthFormat,
			ImageTiling.Optimal,
			ImageLayout.Undefined,
			ImageUsageFlags.DepthStencilAttachmentBit,
			MemoryPropertyFlags.DeviceLocalBit,
			ref depthImage,
			ref depthImageMemory);
		depthImageView = CreateImageView(depthImage, depthFormat, ImageAspectFlags.DepthBit, 1);
	}

	private void InitSyncObjects(
		out VkSemaphore[]? imageAvailableSemaphores,
		out VkSemaphore[]? renderFinishedSemaphores,
		out Fence[]?       inFlightFences
	)
	{
		imageAvailableSemaphores = new VkSemaphore[_framesInFlight];
		renderFinishedSemaphores = new VkSemaphore[_framesInFlight];
		inFlightFences           = new Fence[_framesInFlight];

		SemaphoreCreateInfo semaphoreInfo = new()
		                                    {
			                                    SType = StructureType.SemaphoreCreateInfo,
		                                    };

		FenceCreateInfo fenceInfo = new()
		                            {
			                            SType = StructureType.FenceCreateInfo,
			                            Flags = FenceCreateFlags.SignaledBit,
		                            };

		for (var i = 0; i < _framesInFlight; i++)
		{
			if (_api.Vk.CreateSemaphore(_logicalDevice.Device, semaphoreInfo, null, out imageAvailableSemaphores[i]) != Result.Success ||
			    _api.Vk.CreateSemaphore(_logicalDevice.Device, semaphoreInfo, null, out renderFinishedSemaphores[i]) != Result.Success ||
			    _api.Vk.CreateFence(_logicalDevice.Device, fenceInfo, null, out inFlightFences[i])                   != Result.Success)
			{
				throw new GfxException("Failed to create synchronization objects for a frame!");
			}
		}
	}

	private void InitRenderPass(out VulkanRenderPass? renderPass)
	{
		renderPass = new VulkanRenderPass(_api, _logicalDevice, this, new RenderPassOptions());
	}
	#endregion
	
	#region Initialization helpers
	private SurfaceFormatKHR ChooseSwapSurfaceFormat(IReadOnlyList<SurfaceFormatKHR> availableFormats)
	{
		foreach (var availableFormat in availableFormats)
		{
			if (availableFormat.ColorSpace == ColorSpaceKHR.SpaceSrgbNonlinearKhr)
			{
				if (availableFormat.Format is Format.B8G8R8A8Srgb or Format.R8G8B8A8Srgb)
				{
					return availableFormat;
				}
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
	
	private ImageView CreateImageView(Image image, VkFormat format, ImageAspectFlags aspectFlags, uint mipLevels)
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

		if (_api.Vk.CreateImageView(_logicalDevice.Device, createInfo, null, out ImageView imageView) != Result.Success)
		{
			throw new GfxException("Failed to create image view!");
		}

		return imageView;
	}
	
	private void CreateImage(uint width, uint height, uint mipLevels, SampleCountFlags numSamples, VkFormat format, ImageTiling tiling, ImageLayout layout, ImageUsageFlags usage, MemoryPropertyFlags properties, ref Image image, ref VkDeviceMemory imageMemory)
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
			                            InitialLayout = layout,
			                            Usage         = usage,
			                            Samples       = numSamples,
			                            SharingMode   = VkSharingMode.Exclusive,
		                            };

		fixed (Image* imagePtr = &image)
		{
			if (_api.Vk.CreateImage(_logicalDevice.Device, imageInfo, null, imagePtr) != Result.Success)
			{
				throw new GfxException("Failed to create image!");
			}
		}

		_api.Vk.GetImageMemoryRequirements(_logicalDevice.Device, image, out MemoryRequirements memRequirements);

		MemoryAllocateInfo allocInfo = new()
		                               {
			                               SType           = StructureType.MemoryAllocateInfo,
			                               AllocationSize  = memRequirements.Size,
			                               MemoryTypeIndex = FindMemoryType(memRequirements.MemoryTypeBits, properties),
		                               };

		fixed (VkDeviceMemory* imageMemoryPtr = &imageMemory)
		{
			if (_api.Vk.AllocateMemory(_logicalDevice.Device, allocInfo, null, imageMemoryPtr) != Result.Success)
			{
				throw new GfxException("Failed to allocate image memory!");
			}
		}

		_api.Vk.BindImageMemory(_logicalDevice.Device, image, imageMemory, 0);
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