using Silk.NET.Vulkan;

namespace Gfx;

public class VulkanDeviceMemory : DeviceMemory
{
	internal VulkanDeviceMemory(DeviceMemoryOptions options)
	{
		Silk.NET.Vulkan.MemoryPropertyFlags flags = options.Kind switch
		{
			DeviceMemoryKind.DeviceLocal => MemoryPropertyFlags.DeviceLocalBit,
			DeviceMemoryKind.HostVisible => MemoryPropertyFlags.HostVisibleBit | MemoryPropertyFlags.HostCoherentBit
		};
		
		
	}
}