using VkPipelineShaderStageCreateFlags = Silk.NET.Vulkan.PipelineShaderStageCreateFlags;

namespace Gfx;

public static class VulkanPipelineShaderStageCreateFlagsExtensions
{
	public static VkPipelineShaderStageCreateFlags ToVulkanPipelineShaderStageCreateFlags(this PipelineShaderStageCreateFlags flags)
	{
		VkPipelineShaderStageCreateFlags result = VkPipelineShaderStageCreateFlags.None;

		if ((flags & PipelineShaderStageCreateFlags.RequireFullSubgroupsBit) != 0)
		{
			result |= VkPipelineShaderStageCreateFlags.RequireFullSubgroupsBit;
		}

		if ((flags & PipelineShaderStageCreateFlags.AllowVaryingSubgroupSizeBit) != 0)
		{
			result |= VkPipelineShaderStageCreateFlags.AllowVaryingSubgroupSizeBit;
		}

		return result;
	}
}