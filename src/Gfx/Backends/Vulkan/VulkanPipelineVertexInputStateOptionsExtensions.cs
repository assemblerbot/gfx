using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanPipelineVertexInputStateOptionsExtensions
{
	public static PipelineVertexInputStateCreateInfo ToVulkan(this PipelineVertexInputStateOptions options)
	{
		return new PipelineVertexInputStateCreateInfo
		       {
			       SType = StructureType.PipelineVertexInputStateCreateInfo,
			       VertexAttributeDescriptionCount = (uint)options.VertexBindingDescriptions.Length,
			       
		       };
	}
}