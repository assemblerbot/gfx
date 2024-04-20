using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDeviceHeapPropertiesExtensions
{
	public static DeviceHeapProperties ToDeviceHeapKind(this MemoryHeapFlags flags)
	{
		DeviceHeapProperties result = DeviceHeapProperties.None;
		if ((flags & MemoryHeapFlags.DeviceLocalBit) != 0)
		{
			result |= DeviceHeapProperties.Local;
		}

		if ((flags & MemoryHeapFlags.MultiInstanceBit) != 0)
		{
			result |= DeviceHeapProperties.MultiInstance;
		}

		return result;
	}
}