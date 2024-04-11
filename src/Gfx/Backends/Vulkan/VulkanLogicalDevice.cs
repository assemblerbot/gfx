using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.Maths;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace Gfx;

// TODO - this is just for graphical device, not for compute
public unsafe class VulkanLogicalDevice : LogicalDevice
{
	private const    float                _queuePriority = 1f; // from every queue family we need just one queue (priority: 0..1)
	private readonly VulkanApi            _api;
	private readonly VulkanPhysicalDevice _physicalDevice;
    
	private readonly Device _device;
	private readonly Queue  _graphicsQueue;
	private readonly Queue  _presentQueue;
	
	private KhrSwapchain?  _khrSwapChain;
	private SwapchainKHR   _swapChain;
	private Image[]?       _swapChainImages;
	private Format         _swapChainImageFormat;
	private Extent2D       _swapChainExtent;
	// private ImageView[]?   _swapChainImageViews;
	// private Framebuffer[]? _swapChainFramebuffers;
    
	internal VulkanLogicalDevice(VulkanApi api, LogicalDeviceOptions options)
	{
		_api            = api;
		_physicalDevice = (VulkanPhysicalDevice)options.PhysicalDevice;

		InitDeviceAndQueues(out _device, out _graphicsQueue, out _presentQueue);
		InitSwapChain(options.FrameBufferFormat, ref _khrSwapChain, ref _swapChain, ref _swapChainImages, ref _swapChainImageFormat, ref _swapChainExtent);
	}

	public override void Dispose()
	{
		// foreach (var framebuffer in _swapChainFramebuffers!)
		// {
		// 	_api.Vk!.DestroyFramebuffer(_device, framebuffer, null);
		// }
		
		// foreach (var imageView in _swapChainImageViews!)
		// {
		// 	_api.Vk.DestroyImageView(_device, imageView, null);
		// }
		
		_khrSwapChain!.DestroySwapchain(_device, _swapChain, null);

		for (int i = 0; i < _swapChainImages!.Length; i++)
		{
			// _api.Vk.DestroyBuffer(_device, uniformBuffers![i], null);
			// _api.Vk.FreeMemory(_device, uniformBuffersMemory![i], null);
		}
		
		_api.Vk.DestroyDevice(_device, null);
	}

	public override void WaitIdle()
	{
		_api.Vk.DeviceWaitIdle(_device);
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

		_api.Vk.GetDeviceQueue(_device, _physicalDevice.GraphicsQueueFamily!.Value, 0, out graphicsQueue);
		_api.Vk.GetDeviceQueue(_device, _physicalDevice.PresentQueueFamily!.Value,  0, out presentQueue);

		if (_api.IsDebugEnabled)
		{
			SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
		}

		SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
	}
	
	private void InitSwapChain(
		ImageFormat desiredFormat,
		ref KhrSwapchain? khrSwapChain,
		ref SwapchainKHR  swapChain,
		ref Image[]?      swapChainImages,
		ref Format        swapChainImageFormat,
		ref Extent2D      swapChainExtent
	)
	{
		VulkanSwapChainSupportDetails swapChainSupport = _physicalDevice.SwapChainSupportDetails!;

		var surfaceFormat = ChooseSwapSurfaceFormat(swapChainSupport.Formats, desiredFormat.ToVulkan());
		var presentMode   = ChoosePresentMode(swapChainSupport.PresentModes);
		var extent        = ChooseSwapExtent(swapChainSupport.Capabilities);

		var imageCount = swapChainSupport.Capabilities.MinImageCount + 1;
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
			if (!_api.Vk.TryGetDeviceExtension(_api.Instance, _device, out khrSwapChain))
			{
				throw new NotSupportedException($"{KhrSwapchain.ExtensionName} extension not found.");
			}
		}

		if (khrSwapChain!.CreateSwapchain(_device, createInfo, null, out swapChain) != Result.Success)
		{
			throw new GfxException("Failed to create swap chain!");
		}

		khrSwapChain.GetSwapchainImages(_device, swapChain, ref imageCount, null);
		swapChainImages = new Image[imageCount];
		fixed (Image* swapChainImagesPtr = swapChainImages)
		{
			khrSwapChain.GetSwapchainImages(_device, swapChain, ref imageCount, swapChainImagesPtr);
		}

		swapChainImageFormat = surfaceFormat.Format;
		swapChainExtent      = extent;
	}

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
	#endregion
}