namespace Gfx;

public static class VulkanPipelineShaderStageCreateFlagsExtensions
{
	public static Silk.NET.Vulkan.PipelineShaderStageCreateFlags ToVulkanPipelineShaderStageCreateFlags(this PipelineShaderStageCreateFlags flags)
	{
		Silk.NET.Vulkan.PipelineShaderStageCreateFlags result = Silk.NET.Vulkan.PipelineShaderStageCreateFlags.None;

		if ((flags & PipelineShaderStageCreateFlags.RequireFullSubgroupsBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineShaderStageCreateFlags.RequireFullSubgroupsBit;
		}

		if ((flags & PipelineShaderStageCreateFlags.AllowVaryingSubgroupSizeBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineShaderStageCreateFlags.AllowVaryingSubgroupSizeBit;
		}

		return result;
	}
}