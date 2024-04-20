using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDeviceMemoryPropertiesExtensions
{
	public static DeviceMemoryProperties ToDeviceMemoryKind(this MemoryPropertyFlags flags)
	{
		DeviceMemoryProperties properties = DeviceMemoryProperties.None;
		if ((flags & MemoryPropertyFlags.DeviceLocalBit) != 0)
		{
			properties |= DeviceMemoryProperties.DeviceLocal;
		}

		if ((flags & MemoryPropertyFlags.HostVisibleBit) != 0)
		{
			properties |= DeviceMemoryProperties.HostVisible;
		}

		if ((flags & MemoryPropertyFlags.HostCachedBit) != 0)
		{
			properties |= DeviceMemoryProperties.Cached;
		}

		if ((flags & MemoryPropertyFlags.HostCoherentBit) != 0)
		{
			properties |= DeviceMemoryProperties.HostCoherent;
		}

		return properties;
	}

	public static MemoryPropertyFlags ToMemoryPropertyFlags(this DeviceMemoryProperties properties)
	{
		MemoryPropertyFlags flags = MemoryPropertyFlags.None;
		if ((properties & DeviceMemoryProperties.DeviceLocal) != 0)
		{
			flags |= MemoryPropertyFlags.DeviceLocalBit;
		}

		if ((properties & DeviceMemoryProperties.HostVisible) != 0)
		{
			flags |= MemoryPropertyFlags.HostVisibleBit;
		}

		if ((properties & DeviceMemoryProperties.Cached) != 0)
		{
			flags |= MemoryPropertyFlags.HostCachedBit;
		}

		if ((properties & DeviceMemoryProperties.HostCoherent) != 0)
		{
			flags |= MemoryPropertyFlags.HostCoherentBit;
		}

		return flags;
	}
}