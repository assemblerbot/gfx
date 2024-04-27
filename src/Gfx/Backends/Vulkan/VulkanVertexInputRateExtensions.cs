namespace Gfx;

public static class VulkanVertexInputRateExtensions
{
	public static Silk.NET.Vulkan.VertexInputRate ToVulkanVertexInputRate(this VertexInputRate rate)
	{
		return rate switch
		{
			VertexInputRate.Vertex => Silk.NET.Vulkan.VertexInputRate.Vertex,
			VertexInputRate.Instance => Silk.NET.Vulkan.VertexInputRate.Instance,
		};
	}
}