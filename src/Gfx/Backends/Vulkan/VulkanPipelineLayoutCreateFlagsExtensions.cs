using VkPipelineLayoutCreateFlags = Silk.NET.Vulkan.PipelineLayoutCreateFlags;

namespace Gfx;

public static class VulkanPipelineLayoutCreateFlagsExtensions
{
	public static VkPipelineLayoutCreateFlags ToVulkan(this PipelineLayoutCreateFlags flags)
	{
		VkPipelineLayoutCreateFlags result = VkPipelineLayoutCreateFlags.None;

		if ((flags & PipelineLayoutCreateFlags.IndependentSets) != 0)
		{
			result |= VkPipelineLayoutCreateFlags.IndependentSetsBitExt;
		}

		return result;
	}
}