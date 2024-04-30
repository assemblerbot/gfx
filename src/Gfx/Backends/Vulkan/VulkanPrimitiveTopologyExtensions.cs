using VkPrimitiveTopology = Silk.NET.Vulkan.PrimitiveTopology;

namespace Gfx;

public static class VulkanPrimitiveTopologyExtensions
{
	public static VkPrimitiveTopology ToVulkan(this PrimitiveTopology topology)
	{
		return topology switch
		{
			PrimitiveTopology.PointList                  => VkPrimitiveTopology.PointList,
			PrimitiveTopology.LineList                   => VkPrimitiveTopology.LineList,
			PrimitiveTopology.LineStrip                  => VkPrimitiveTopology.LineStrip,
			PrimitiveTopology.TriangleList               => VkPrimitiveTopology.TriangleList,
			PrimitiveTopology.TriangleStrip              => VkPrimitiveTopology.TriangleStrip,
			PrimitiveTopology.TriangleFan                => VkPrimitiveTopology.TriangleFan,
			PrimitiveTopology.LineListWithAdjacency      => VkPrimitiveTopology.LineListWithAdjacency,
			PrimitiveTopology.LineStripWithAdjacency     => VkPrimitiveTopology.LineStripWithAdjacency,
			PrimitiveTopology.TriangleListWithAdjacency  => VkPrimitiveTopology.TriangleListWithAdjacency,
			PrimitiveTopology.TriangleStripWithAdjacency => VkPrimitiveTopology.TriangleStripWithAdjacency,
			PrimitiveTopology.PatchList                  => VkPrimitiveTopology.PatchList,
		};
	}
}