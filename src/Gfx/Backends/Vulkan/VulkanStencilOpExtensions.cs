using VkStencilOp = Silk.NET.Vulkan.StencilOp;

namespace Gfx;

public static class VulkanStencilOpExtensions
{
	public static VkStencilOp ToVulkanStencilOp(this StencilOp stencilOp)
	{
		return stencilOp switch
		{
			StencilOp.Keep              => VkStencilOp.Keep,
			StencilOp.Zero              => VkStencilOp.Zero,
			StencilOp.Replace           => VkStencilOp.Replace,
			StencilOp.IncrementAndClamp => VkStencilOp.IncrementAndClamp,
			StencilOp.DecrementAndClamp => VkStencilOp.DecrementAndClamp,
			StencilOp.Invert            => VkStencilOp.Invert,
			StencilOp.IncrementAndWrap  => VkStencilOp.IncrementAndWrap,
			StencilOp.DecrementAndWrap  => VkStencilOp.DecrementAndClamp,
		};
	}
}