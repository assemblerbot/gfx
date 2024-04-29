using VkVertexInputRate = Silk.NET.Vulkan.VertexInputRate;

namespace Gfx;

public static class VulkanVertexInputRateExtensions
{
	public static VkVertexInputRate ToVulkanVertexInputRate(this VertexInputRate rate)
	{
		return rate switch
		{
			VertexInputRate.Vertex => VkVertexInputRate.Vertex,
			VertexInputRate.Instance => VkVertexInputRate.Instance,
		};
	}
}