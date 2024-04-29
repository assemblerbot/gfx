using VkPipelineColorBlendStateCreateFlags = Silk.NET.Vulkan.PipelineColorBlendStateCreateFlags;

namespace Gfx;

public static class VulkanPipelineColorBlendStateCreateFlagsExtensions
{
	public static VkPipelineColorBlendStateCreateFlags ToVulkanPipelineColorBlendStateCreateFlags(this PipelineColorBlendStateCreateFlags createFlags)
	{
		VkPipelineColorBlendStateCreateFlags result = VkPipelineColorBlendStateCreateFlags.None;

		if ((createFlags & PipelineColorBlendStateCreateFlags.RasterizationOrderAttachmentAccess) != 0)
		{
			result |= VkPipelineColorBlendStateCreateFlags.Arm;
		}

		return result;
	}
}