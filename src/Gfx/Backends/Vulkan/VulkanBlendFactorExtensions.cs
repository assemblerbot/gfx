using VkBlendFactor = Silk.NET.Vulkan.BlendFactor;

namespace Gfx;

public static class VulkanBlendFactorExtensions
{
	public static VkBlendFactor ToVulkanBlendFactor(this BlendFactor blendFactor)
	{
		return blendFactor switch
		{
			BlendFactor.Zero                 => VkBlendFactor.Zero                 ,
			BlendFactor.One                  => VkBlendFactor.One                  ,
			BlendFactor.SrcColor             => VkBlendFactor.SrcColor             ,
			BlendFactor.OneMinusSrcColor     => VkBlendFactor.OneMinusSrcColor     ,
			BlendFactor.DstColor             => VkBlendFactor.DstColor             ,
			BlendFactor.OneMinusDstColor     => VkBlendFactor.OneMinusDstColor     ,
			BlendFactor.SrcAlpha             => VkBlendFactor.SrcAlpha             ,
			BlendFactor.OneMinusSrcAlpha     => VkBlendFactor.OneMinusSrcAlpha     ,
			BlendFactor.DstAlpha             => VkBlendFactor.DstAlpha             ,
			BlendFactor.OneMinusDstAlpha     => VkBlendFactor.OneMinusDstAlpha     ,
			BlendFactor.ConstantColor        => VkBlendFactor.ConstantColor        ,
			BlendFactor.OneMinusConstantColor=> VkBlendFactor.OneMinusConstantColor,
			BlendFactor.ConstantAlpha        => VkBlendFactor.ConstantAlpha        ,
			BlendFactor.OneMinusConstantAlpha=> VkBlendFactor.OneMinusConstantAlpha,
			BlendFactor.SrcAlphaSaturate     => VkBlendFactor.SrcAlphaSaturate     ,
			BlendFactor.Src1Color            => VkBlendFactor.Src1Color            ,
			BlendFactor.OneMinusSrc1Color    => VkBlendFactor.OneMinusSrc1Color    ,
			BlendFactor.Src1Alpha            => VkBlendFactor.Src1Alpha            ,
			BlendFactor.OneMinusSrc1Alpha    => VkBlendFactor.OneMinusSrc1Alpha    ,
		};
	}
}