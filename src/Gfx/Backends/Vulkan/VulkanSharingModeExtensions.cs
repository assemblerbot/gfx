namespace Gfx;

public static class VulkanSharingModeExtensions
{
	public static Silk.NET.Vulkan.SharingMode ToVulkanSharingMode(this SharingMode mode)
	{
		return mode switch
		{
			SharingMode.Excludive => Silk.NET.Vulkan.SharingMode.Exclusive,
			SharingMode.Concurent => Silk.NET.Vulkan.SharingMode.Concurrent,
			_ => throw new GfxException("Invalid sharing mode.")
		};
	}
}