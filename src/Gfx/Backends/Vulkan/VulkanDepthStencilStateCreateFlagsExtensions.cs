using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDepthStencilStateCreateFlagsExtensions
{
	public static PipelineDepthStencilStateCreateFlags ToVulkan(this DepthStencilStateCreateFlags depthStencilStateCreateFlags)
	{
		PipelineDepthStencilStateCreateFlags flags = PipelineDepthStencilStateCreateFlags.None;
		if ((depthStencilStateCreateFlags & DepthStencilStateCreateFlags.DepthAccess) != 0)
		{
			flags |= PipelineDepthStencilStateCreateFlags.DepthAccessBitArm;
		}

		if ((depthStencilStateCreateFlags & DepthStencilStateCreateFlags.StencilAccess) != 0)
		{
			flags |= PipelineDepthStencilStateCreateFlags.StencilAccessBitArm;
		}

		return flags;
	}
}