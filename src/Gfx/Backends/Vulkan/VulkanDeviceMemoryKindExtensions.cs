using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDeviceMemoryKindExtensions
{
	public static DeviceMemoryKind ToDeviceMemoryKind(this MemoryPropertyFlags properties)
	{
		DeviceMemoryKind kind = DeviceMemoryKind.None;
		if ((properties & MemoryPropertyFlags.DeviceLocalBit) != 0)
		{
			kind |= DeviceMemoryKind.DeviceLocal;
		}

		if ((properties & MemoryPropertyFlags.HostVisibleBit) != 0)
		{
			kind |= DeviceMemoryKind.HostVisible;
		}

		if ((properties & MemoryPropertyFlags.HostCachedBit) != 0)
		{
			kind |= DeviceMemoryKind.Cached;
		}

		if ((properties & MemoryPropertyFlags.HostCoherentBit) != 0)
		{
			kind |= DeviceMemoryKind.HostCoherent;
		}

		return kind;
	}

	public static MemoryPropertyFlags ToMemoryPropertyFlags(this DeviceMemoryKind kind)
	{
		MemoryPropertyFlags flags = MemoryPropertyFlags.None;
		if ((kind & DeviceMemoryKind.DeviceLocal) != 0)
		{
			flags |= MemoryPropertyFlags.DeviceLocalBit;
		}

		if ((kind & DeviceMemoryKind.HostVisible) != 0)
		{
			flags |= MemoryPropertyFlags.HostVisibleBit;
		}

		if ((kind & DeviceMemoryKind.Cached) != 0)
		{
			flags |= MemoryPropertyFlags.HostCachedBit;
		}

		if ((kind & DeviceMemoryKind.HostCoherent) != 0)
		{
			flags |= MemoryPropertyFlags.HostCoherentBit;
		}

		return flags;
	}
}