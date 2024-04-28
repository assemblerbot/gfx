namespace Gfx;

public static class VulkanPipelineLayoutCreateFlagsExtensions
{
	public static Silk.NET.Vulkan.PipelineLayoutCreateFlags ToVulkanPipelineLayoutCreateFlags(this PipelineLayoutCreateFlags flags)
	{
		Silk.NET.Vulkan.PipelineLayoutCreateFlags result = Silk.NET.Vulkan.PipelineLayoutCreateFlags.None;

		if ((flags & PipelineLayoutCreateFlags.IndependentSets) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineLayoutCreateFlags.IndependentSetsBitExt;
		}

		return result;
	}
}