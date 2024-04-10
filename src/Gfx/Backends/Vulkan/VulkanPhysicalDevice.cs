using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace Gfx;

public unsafe class VulkanPhysicalDevice : PhysicalDevice
{
	private readonly VulkanApi                      _api;
	private readonly Silk.NET.Vulkan.PhysicalDevice _physicalDevice;

	private PhysicalDeviceKind _kind = PhysicalDeviceKind.Other;
	private string             _name = "";

	internal uint? GraphicsQueueFamily { get; private set; }
	internal uint? PresentQueueFamily  { get; private set; }
	internal uint? ComputeQueueFamily  { get; private set; }

	internal bool                           GraphicsExtensionsSupported = false;
	internal VulkanSwapChainSupportDetails? SwapChainSupportDetails;

	internal PhysicalDeviceFeatures Features;

	public override PhysicalDeviceKind Kind => _kind;
	public override string             Name => _name;
	
	public override bool SupportsGraphics =>
		GraphicsQueueFamily.HasValue                          &&
		PresentQueueFamily.HasValue                           &&
		GraphicsExtensionsSupported                           &&
		(SwapChainSupportDetails?.Formats.Any()      ?? false) &&
		(SwapChainSupportDetails?.PresentModes.Any() ?? false);

	public override bool SupportsCompute => ComputeQueueFamily.HasValue;
	
	internal VulkanPhysicalDevice(
		VulkanApi                      api,
		Silk.NET.Vulkan.PhysicalDevice physicalDevice
	)
	{
		_api            = api;
		_physicalDevice = physicalDevice;
		
		InitKind();
		InitName();
		InitQueueFamilies();
		InitExtensionSupport();
		InitFeatures();
		
		if (GraphicsExtensionsSupported)
		{
			InitSwapChainSupport();
		}
	}

	private void InitKind()
	{
		_kind = _api.Vk.GetPhysicalDeviceProperty(_physicalDevice).DeviceType switch
		{
			PhysicalDeviceType.Other         => PhysicalDeviceKind.Other,
			PhysicalDeviceType.IntegratedGpu => PhysicalDeviceKind.IntegratedGpu,
			PhysicalDeviceType.DiscreteGpu   => PhysicalDeviceKind.DiscreteGpu,
			PhysicalDeviceType.VirtualGpu    => PhysicalDeviceKind.VirtualGpu,
			PhysicalDeviceType.Cpu           => PhysicalDeviceKind.Cpu,
			_                                => throw new ArgumentOutOfRangeException()
		};
	}

	private void InitName()
	{
		PhysicalDeviceProperties properties = _api.Vk.GetPhysicalDeviceProperty(_physicalDevice);
		_name = Marshal.PtrToStringAnsi((IntPtr)properties.DeviceName) ?? "unknown";
	}

	private void InitQueueFamilies()
	{
		GraphicsQueueFamily = null;
		PresentQueueFamily  = null;
		ComputeQueueFamily  = null;
		
		{
			uint queueFamilyCount = 0;
			_api.Vk.GetPhysicalDeviceQueueFamilyProperties(_physicalDevice, ref queueFamilyCount, null);

			var queueFamilies = new QueueFamilyProperties[queueFamilyCount];
			fixed (QueueFamilyProperties* queueFamiliesPtr = queueFamilies)
			{
				_api.Vk.GetPhysicalDeviceQueueFamilyProperties(_physicalDevice, ref queueFamilyCount, queueFamiliesPtr);
			}

			for(uint i=0;i <queueFamilies.Length;++i)
			{
				if (queueFamilies[i].QueueFlags.HasFlag(QueueFlags.GraphicsBit))
				{
					GraphicsQueueFamily = i;
				}

				if (queueFamilies[i].QueueFlags.HasFlag(QueueFlags.ComputeBit))
				{
					ComputeQueueFamily = i;
				}
				
				_api.KhrSurface!.GetPhysicalDeviceSurfaceSupport(_physicalDevice, i, _api.Surface, out var presentSupport);
				if (presentSupport)
				{
					PresentQueueFamily = i;
				}
			}
		}
	}

	private void InitExtensionSupport()
	{
		uint extensionsCount = 0;
		_api.Vk.EnumerateDeviceExtensionProperties(_physicalDevice, (byte*)null, ref extensionsCount, null);

		var availableExtensions = new ExtensionProperties[extensionsCount];
		fixed (ExtensionProperties* availableExtensionsPtr = availableExtensions)
		{
			_api.Vk.EnumerateDeviceExtensionProperties(_physicalDevice, (byte*)null, ref extensionsCount, availableExtensionsPtr);
		}

		HashSet<string?> availableExtensionNames = availableExtensions.Select(extension => { return Marshal.PtrToStringAnsi((IntPtr) extension.ExtensionName); }).ToHashSet();

		// checks
		GraphicsExtensionsSupported = VulkanExtensions.GraphicsExtensions.All(availableExtensionNames.Contains);
	}

	private void InitFeatures()
	{
		_api.Vk.GetPhysicalDeviceFeatures(_physicalDevice, out Features);
	}

	private void InitSwapChainSupport()
	{
		VulkanSwapChainSupportDetails details = new();
		_api.KhrSurface!.GetPhysicalDeviceSurfaceCapabilities(_physicalDevice, _api.Surface, out details.Capabilities);

		uint formatCount = 0;
		_api.KhrSurface.GetPhysicalDeviceSurfaceFormats(_physicalDevice, _api.Surface, ref formatCount, null);

		if (formatCount != 0)
		{
			details.Formats = new SurfaceFormatKHR[formatCount];
			fixed (SurfaceFormatKHR* formatsPtr = details.Formats)
			{
				_api.KhrSurface.GetPhysicalDeviceSurfaceFormats(_physicalDevice, _api.Surface, ref formatCount, formatsPtr);
			}
		}
		else
		{
			details.Formats = Array.Empty<SurfaceFormatKHR>();
		}

		uint presentModeCount = 0;
		_api.KhrSurface.GetPhysicalDeviceSurfacePresentModes(_physicalDevice, _api.Surface, ref presentModeCount, null);

		if (presentModeCount != 0)
		{
			details.PresentModes = new PresentModeKHR[presentModeCount];
			fixed (PresentModeKHR* formatsPtr = details.PresentModes)
			{
				_api.KhrSurface.GetPhysicalDeviceSurfacePresentModes(_physicalDevice, _api.Surface, ref presentModeCount, formatsPtr);
			}

		}
		else
		{
			details.PresentModes = Array.Empty<PresentModeKHR>();
		}

		SwapChainSupportDetails = details;
	}
}