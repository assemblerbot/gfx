namespace Gfx;

public static class VulkanCommandBufferLevelExtensions
{
	public static Silk.NET.Vulkan.CommandBufferLevel ToVulkanCommandBufferLevel(this Gfx.CommandBufferLevel level)
	{
		return level switch
		{
			CommandBufferLevel.Primary => Silk.NET.Vulkan.CommandBufferLevel.Primary,
			CommandBufferLevel.Secondary => Silk.NET.Vulkan.CommandBufferLevel.Secondary,
			_ => throw new NotImplementedException()
		};
	}
}