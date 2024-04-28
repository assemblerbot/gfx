namespace Gfx;

public static class VulkanPrimitiveTopologyExtensions
{
	public static Silk.NET.Vulkan.PrimitiveTopology ToVulkanPrimitiveTopology(this PrimitiveTopology topology)
	{
		return topology switch
		{
			PrimitiveTopology.PointList                  => Silk.NET.Vulkan.PrimitiveTopology.PointList,
			PrimitiveTopology.LineList                   => Silk.NET.Vulkan.PrimitiveTopology.LineList,
			PrimitiveTopology.LineStrip                  => Silk.NET.Vulkan.PrimitiveTopology.LineStrip,
			PrimitiveTopology.TriangleList               => Silk.NET.Vulkan.PrimitiveTopology.TriangleList,
			PrimitiveTopology.TriangleStrip              => Silk.NET.Vulkan.PrimitiveTopology.TriangleStrip,
			PrimitiveTopology.TriangleFan                => Silk.NET.Vulkan.PrimitiveTopology.TriangleFan,
			PrimitiveTopology.LineListWithAdjacency      => Silk.NET.Vulkan.PrimitiveTopology.LineListWithAdjacency,
			PrimitiveTopology.LineStripWithAdjacency     => Silk.NET.Vulkan.PrimitiveTopology.LineStripWithAdjacency,
			PrimitiveTopology.TriangleListWithAdjacency  => Silk.NET.Vulkan.PrimitiveTopology.TriangleListWithAdjacency,
			PrimitiveTopology.TriangleStripWithAdjacency => Silk.NET.Vulkan.PrimitiveTopology.TriangleStripWithAdjacency,
			PrimitiveTopology.PatchList                  => Silk.NET.Vulkan.PrimitiveTopology.PatchList,
		};
	}
}