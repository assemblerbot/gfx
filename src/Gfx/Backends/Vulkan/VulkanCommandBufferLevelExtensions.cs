using VkCommandBufferLevel = Silk.NET.Vulkan.CommandBufferLevel;

namespace Gfx;

public static class VulkanCommandBufferLevelExtensions
{
	public static VkCommandBufferLevel ToVulkan(this Gfx.CommandBufferLevel level)
	{
		return level switch
		{
			CommandBufferLevel.Primary => VkCommandBufferLevel.Primary,
			CommandBufferLevel.Secondary => VkCommandBufferLevel.Secondary,
			_ => throw new NotImplementedException()
		};
	}
}