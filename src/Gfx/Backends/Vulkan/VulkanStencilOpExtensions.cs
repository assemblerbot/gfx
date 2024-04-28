namespace Gfx;

public static class VulkanStencilOpExtensions
{
	public static Silk.NET.Vulkan.StencilOp ToVulkanStencilOp(this StencilOp stencilOp)
	{
		return stencilOp switch
		{
			StencilOp.Keep              => Silk.NET.Vulkan.StencilOp.Keep,
			StencilOp.Zero              => Silk.NET.Vulkan.StencilOp.Zero,
			StencilOp.Replace           => Silk.NET.Vulkan.StencilOp.Replace,
			StencilOp.IncrementAndClamp => Silk.NET.Vulkan.StencilOp.IncrementAndClamp,
			StencilOp.DecrementAndClamp => Silk.NET.Vulkan.StencilOp.DecrementAndClamp,
			StencilOp.Invert            => Silk.NET.Vulkan.StencilOp.Invert,
			StencilOp.IncrementAndWrap  => Silk.NET.Vulkan.StencilOp.IncrementAndWrap,
			StencilOp.DecrementAndWrap  => Silk.NET.Vulkan.StencilOp.DecrementAndClamp,
		};
	}
}