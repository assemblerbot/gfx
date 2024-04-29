namespace Gfx;

public static class VulkanSharingModeExtensions
{
	public static Silk.NET.Vulkan.SharingMode ToVulkanSharingMode(this SharingMode mode)
	{
		return mode switch
		{
			SharingMode.Exclusive => Silk.NET.Vulkan.SharingMode.Exclusive,
			SharingMode.Concurrent => Silk.NET.Vulkan.SharingMode.Concurrent,
			_ => throw new GfxException("Invalid sharing mode.")
		};
	}
}