using System.Runtime.InteropServices;
using Silk.NET.Vulkan;
using VkPhysicalDevice = Silk.NET.Vulkan.PhysicalDevice;

namespace Gfx;

public sealed unsafe class VulkanPhysicalDevice : PhysicalDevice
{
	private readonly VulkanApi                      _api;
	public readonly  VkPhysicalDevice Device;

	private readonly PhysicalDeviceKind _kind;
	private readonly string             _name;

	internal readonly uint? GraphicsQueueFamily;
	internal readonly uint? PresentQueueFamily;
	internal readonly uint? ComputeQueueFamily;

	internal readonly bool                           GraphicsExtensionsSupported;
	internal readonly VulkanSwapChainSupportDetails? SwapChainSupportDetails;

	internal readonly PhysicalDeviceFeatures   Features;
	internal readonly PhysicalDeviceProperties Properties;

	public override PhysicalDeviceKind Kind => _kind;
	public override string             Name => _name;
	
	public override bool SupportsGraphics =>
		GraphicsQueueFamily.HasValue                          &&
		PresentQueueFamily.HasValue                           &&
		GraphicsExtensionsSupported                           &&
		SwapChainSupportDetails?.Formats.Length != 0 &&
		SwapChainSupportDetails?.PresentModes.Length != 0;

	public override bool SupportsCompute => ComputeQueueFamily.HasValue;
	
	internal VulkanPhysicalDevice(
		VulkanApi                      api,
		VkPhysicalDevice device
	)
	{
		_api            = api;
		Device = device;
		
		InitKind(out _kind);
		InitName(out _name);
		InitQueueFamilies(out GraphicsQueueFamily, out PresentQueueFamily, out ComputeQueueFamily);
		InitExtensionSupport(out GraphicsExtensionsSupported);
		InitFeatures(out Features);
		InitProperties(out Properties);
		
		if (GraphicsExtensionsSupported)
		{
			InitSwapChainSupport(out SwapChainSupportDetails);
		}
	}

	#region Initialization
	private void InitKind(out PhysicalDeviceKind kind)
	{
		kind = _api.Vk.GetPhysicalDeviceProperty(Device).DeviceType switch
		{
			PhysicalDeviceType.Other         => PhysicalDeviceKind.Other,
			PhysicalDeviceType.IntegratedGpu => PhysicalDeviceKind.IntegratedGpu,
			PhysicalDeviceType.DiscreteGpu   => PhysicalDeviceKind.DiscreteGpu,
			PhysicalDeviceType.VirtualGpu    => PhysicalDeviceKind.VirtualGpu,
			PhysicalDeviceType.Cpu           => PhysicalDeviceKind.Cpu,
			_                                => throw new ArgumentOutOfRangeException()
		};
	}

	private void InitName(out string name)
	{
		PhysicalDeviceProperties properties = _api.Vk.GetPhysicalDeviceProperty(Device);
		name = Marshal.PtrToStringAnsi((IntPtr)properties.DeviceName) ?? "unknown";
	}

	private void InitQueueFamilies(out uint? graphicsQueueFamily, out uint? presentQueueFamily, out uint? computeQueueFamily)
	{
		graphicsQueueFamily = null;
		presentQueueFamily  = null;
		computeQueueFamily  = null;
		
		{
			uint queueFamilyCount = 0;
			_api.Vk.GetPhysicalDeviceQueueFamilyProperties(Device, ref queueFamilyCount, null);

			var queueFamilies = new QueueFamilyProperties[queueFamilyCount];
			fixed (QueueFamilyProperties* queueFamiliesPtr = queueFamilies)
			{
				_api.Vk.GetPhysicalDeviceQueueFamilyProperties(Device, ref queueFamilyCount, queueFamiliesPtr);
			}

			for(uint i=0;i <queueFamilies.Length;++i)
			{
				if (queueFamilies[i].QueueFlags.HasFlag(QueueFlags.GraphicsBit))
				{
					graphicsQueueFamily = i;
				}

				if (queueFamilies[i].QueueFlags.HasFlag(QueueFlags.ComputeBit))
				{
					computeQueueFamily = i;
				}
				
				_api.KhrSurface!.GetPhysicalDeviceSurfaceSupport(Device, i, _api.Surface, out var presentSupport);
				if (presentSupport)
				{
					presentQueueFamily = i;
				}
			}
		}
	}

	private void InitExtensionSupport(out bool graphicsExtensionsSupported)
	{
		uint extensionsCount = 0;
		_api.Vk.EnumerateDeviceExtensionProperties(Device, (byte*)null, ref extensionsCount, null);

		var availableExtensions = new ExtensionProperties[extensionsCount];
		fixed (ExtensionProperties* availableExtensionsPtr = availableExtensions)
		{
			_api.Vk.EnumerateDeviceExtensionProperties(Device, (byte*)null, ref extensionsCount, availableExtensionsPtr);
		}

		HashSet<string?> availableExtensionNames = availableExtensions.Select(extension => { return Marshal.PtrToStringAnsi((IntPtr) extension.ExtensionName); }).ToHashSet();

		// checks
		graphicsExtensionsSupported = VulkanExtensions.GraphicsExtensions.All(availableExtensionNames.Contains);
	}

	private void InitFeatures(out PhysicalDeviceFeatures features)
	{
		_api.Vk.GetPhysicalDeviceFeatures(Device, out features);
	}

	private void InitProperties(out PhysicalDeviceProperties properties)
	{
		_api.Vk.GetPhysicalDeviceProperties(Device, out properties);
	}

	private void InitSwapChainSupport(out VulkanSwapChainSupportDetails details)
	{
		details = new VulkanSwapChainSupportDetails();
		_api.KhrSurface!.GetPhysicalDeviceSurfaceCapabilities(Device, _api.Surface, out details.Capabilities);

		uint formatCount = 0;
		_api.KhrSurface.GetPhysicalDeviceSurfaceFormats(Device, _api.Surface, ref formatCount, null);

		if (formatCount != 0)
		{
			details.Formats = new SurfaceFormatKHR[formatCount];
			fixed (SurfaceFormatKHR* formatsPtr = details.Formats)
			{
				_api.KhrSurface.GetPhysicalDeviceSurfaceFormats(Device, _api.Surface, ref formatCount, formatsPtr);
			}
		}
		else
		{
			details.Formats = Array.Empty<SurfaceFormatKHR>();
		}

		uint presentModeCount = 0;
		_api.KhrSurface.GetPhysicalDeviceSurfacePresentModes(Device, _api.Surface, ref presentModeCount, null);

		if (presentModeCount != 0)
		{
			details.PresentModes = new PresentModeKHR[presentModeCount];
			fixed (PresentModeKHR* formatsPtr = details.PresentModes)
			{
				_api.KhrSurface.GetPhysicalDeviceSurfacePresentModes(Device, _api.Surface, ref presentModeCount, formatsPtr);
			}

		}
		else
		{
			details.PresentModes = Array.Empty<PresentModeKHR>();
		}
	}
	#endregion

	#region Public

	public override DeviceMemoryInfo GetMemoryInfo()
	{
		_api.Vk.GetPhysicalDeviceMemoryProperties(Device, out PhysicalDeviceMemoryProperties memProperties);
		
		List<DeviceMemoryInfo.MemoryInfo> memoryInfo = new();
		for (int i = 0; i < memProperties.MemoryTypeCount; ++i)
		{
			memoryInfo.Add(
				new DeviceMemoryInfo.MemoryInfo
				(
					memProperties.MemoryTypes[i].PropertyFlags.ToDeviceMemoryKind(),
					(int)memProperties.MemoryTypes[i].HeapIndex
				)
			);
		}

		List<DeviceMemoryInfo.HeapInfo> heapInfo = new();
		for (int i = 0; i < memProperties.MemoryHeapCount; ++i)
		{
			heapInfo.Add(
				new DeviceMemoryInfo.HeapInfo
				(
					memProperties.MemoryHeaps[i].Flags.ToDeviceHeapKind(),
					memProperties.MemoryHeaps[i].Size
				)
			);
		}

		return new DeviceMemoryInfo(memoryInfo, heapInfo);
	}
	
	#endregion
	
	#region Internal
	internal SampleCountFlags GetMaxMsaaSamplesCount()
	{
		SampleCountFlags counts = Properties.Limits.FramebufferColorSampleCounts & Properties.Limits.FramebufferDepthSampleCounts;
		
		return counts switch
		{
			var c when (c & SampleCountFlags.Count64Bit) != 0 => SampleCountFlags.Count64Bit,
			var c when (c & SampleCountFlags.Count32Bit) != 0 => SampleCountFlags.Count32Bit,
			var c when (c & SampleCountFlags.Count16Bit) != 0 => SampleCountFlags.Count16Bit,
			var c when (c & SampleCountFlags.Count8Bit)  != 0 => SampleCountFlags.Count8Bit,
			var c when (c & SampleCountFlags.Count4Bit)  != 0 => SampleCountFlags.Count4Bit,
			var c when (c & SampleCountFlags.Count2Bit)  != 0 => SampleCountFlags.Count2Bit,
			_                                                 => SampleCountFlags.Count1Bit
		};
	}
	
	internal Silk.NET.Vulkan.Format FindSupportedFormat(IEnumerable<Silk.NET.Vulkan.Format> candidates, ImageTiling tiling, FormatFeatureFlags features)
	{
		foreach (var format in candidates)
		{
			_api.Vk.GetPhysicalDeviceFormatProperties(Device, format, out var props);

			if (tiling == ImageTiling.Linear && (props.LinearTilingFeatures & features) == features)
			{
				return format;
			}
			
			if (tiling == ImageTiling.Optimal && (props.OptimalTilingFeatures & features) == features)
			{
				return format;
			}
		}

		return Silk.NET.Vulkan.Format.Undefined;
	}

	internal uint FindMemoryIndex(uint typeIndexFilter, MemoryPropertyFlags properties)
	{
		_api.Vk.GetPhysicalDeviceMemoryProperties(Device, out PhysicalDeviceMemoryProperties memProperties);
		
		for (int i = 0; i < memProperties.MemoryTypeCount; i++)
		{
			if ((typeIndexFilter & (1 << i)) != 0 && (memProperties.MemoryTypes[i].PropertyFlags & properties) == properties)
			{
				return (uint)i;
			}
		}

		return uint.MaxValue;
	}

	#endregion
}