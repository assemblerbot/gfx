namespace Gfx;

public static class VulkanPipelineColorBlendStateCreateFlagsExtensions
{
	public static Silk.NET.Vulkan.PipelineColorBlendStateCreateFlags ToVulkanPipelineColorBlendStateCreateFlags(this PipelineColorBlendStateCreateFlags createFlags)
	{
		Silk.NET.Vulkan.PipelineColorBlendStateCreateFlags result = Silk.NET.Vulkan.PipelineColorBlendStateCreateFlags.None;

		if ((createFlags & PipelineColorBlendStateCreateFlags.RasterizationOrderAttachmentAccess) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineColorBlendStateCreateFlags.Arm;
		}

		return result;
	}
}