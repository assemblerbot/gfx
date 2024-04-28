namespace Gfx;

public static class VulkanBlendFactorExtensions
{
	public static Silk.NET.Vulkan.BlendFactor ToVulkanBlendFactor(this BlendFactor blendFactor)
	{
		return blendFactor switch
		{
			BlendFactor.Zero                 => Silk.NET.Vulkan.BlendFactor.Zero                 ,
			BlendFactor.One                  => Silk.NET.Vulkan.BlendFactor.One                  ,
			BlendFactor.SrcColor             => Silk.NET.Vulkan.BlendFactor.SrcColor             ,
			BlendFactor.OneMinusSrcColor     => Silk.NET.Vulkan.BlendFactor.OneMinusSrcColor     ,
			BlendFactor.DstColor             => Silk.NET.Vulkan.BlendFactor.DstColor             ,
			BlendFactor.OneMinusDstColor     => Silk.NET.Vulkan.BlendFactor.OneMinusDstColor     ,
			BlendFactor.SrcAlpha             => Silk.NET.Vulkan.BlendFactor.SrcAlpha             ,
			BlendFactor.OneMinusSrcAlpha     => Silk.NET.Vulkan.BlendFactor.OneMinusSrcAlpha     ,
			BlendFactor.DstAlpha             => Silk.NET.Vulkan.BlendFactor.DstAlpha             ,
			BlendFactor.OneMinusDstAlpha     => Silk.NET.Vulkan.BlendFactor.OneMinusDstAlpha     ,
			BlendFactor.ConstantColor        => Silk.NET.Vulkan.BlendFactor.ConstantColor        ,
			BlendFactor.OneMinusConstantColor=> Silk.NET.Vulkan.BlendFactor.OneMinusConstantColor,
			BlendFactor.ConstantAlpha        => Silk.NET.Vulkan.BlendFactor.ConstantAlpha        ,
			BlendFactor.OneMinusConstantAlpha=> Silk.NET.Vulkan.BlendFactor.OneMinusConstantAlpha,
			BlendFactor.SrcAlphaSaturate     => Silk.NET.Vulkan.BlendFactor.SrcAlphaSaturate     ,
			BlendFactor.Src1Color            => Silk.NET.Vulkan.BlendFactor.Src1Color            ,
			BlendFactor.OneMinusSrc1Color    => Silk.NET.Vulkan.BlendFactor.OneMinusSrc1Color    ,
			BlendFactor.Src1Alpha            => Silk.NET.Vulkan.BlendFactor.Src1Alpha            ,
			BlendFactor.OneMinusSrc1Alpha    => Silk.NET.Vulkan.BlendFactor.OneMinusSrc1Alpha    ,
		};
	}
}