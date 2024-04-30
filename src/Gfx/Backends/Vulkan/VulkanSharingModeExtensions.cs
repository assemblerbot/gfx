using VkSharingMode = Silk.NET.Vulkan.SharingMode;

namespace Gfx;

public static class VulkanSharingModeExtensions
{
	public static VkSharingMode ToVulkan(this SharingMode mode)
	{
		return mode switch
		{
			SharingMode.Exclusive => VkSharingMode.Exclusive,
			SharingMode.Concurrent => VkSharingMode.Concurrent,
			_ => throw new GfxException("Invalid sharing mode."),
		};
	}
}