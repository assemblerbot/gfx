using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDeviceHeapKindExtensions
{
	public static DeviceHeapKind ToDeviceHeapKind(this MemoryHeapFlags flags)
	{
		DeviceHeapKind result = DeviceHeapKind.None;
		if ((flags & MemoryHeapFlags.DeviceLocalBit) != 0)
		{
			result |= DeviceHeapKind.Local;
		}

		if ((flags & MemoryHeapFlags.MultiInstanceBit) != 0)
		{
			result |= DeviceHeapKind.MultiInstance;
		}

		return result;
	}
}