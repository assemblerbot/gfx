using VkBlendOp = Silk.NET.Vulkan.BlendOp;

namespace Gfx;

public static class VulkanBlendOpExtensions
{
	public static VkBlendOp ToVulkanBlendOp(this BlendOp blendOp)
	{
		return blendOp switch
		{
			BlendOp.Add                 => VkBlendOp.Add                 ,
			BlendOp.Subtract            => VkBlendOp.Subtract            ,
			BlendOp.ReverseSubtract     => VkBlendOp.ReverseSubtract     ,
			BlendOp.Min                 => VkBlendOp.Min                 ,
			BlendOp.Max                 => VkBlendOp.Max                 ,
			BlendOp.ZeroExt             => VkBlendOp.ZeroExt             ,
			BlendOp.SrcExt              => VkBlendOp.SrcExt              ,
			BlendOp.DstExt              => VkBlendOp.DstExt              ,
			BlendOp.SrcOverExt          => VkBlendOp.SrcOverExt          ,
			BlendOp.DstOverExt          => VkBlendOp.DstOverExt          ,
			BlendOp.SrcInExt            => VkBlendOp.SrcInExt            ,
			BlendOp.DstInExt            => VkBlendOp.DstInExt            ,
			BlendOp.SrcOutExt           => VkBlendOp.SrcOutExt           ,
			BlendOp.DstOutExt           => VkBlendOp.DstOutExt           ,
			BlendOp.SrcAtopExt          => VkBlendOp.SrcAtopExt          ,
			BlendOp.DstAtopExt          => VkBlendOp.DstAtopExt          ,
			BlendOp.XorExt              => VkBlendOp.XorExt              ,
			BlendOp.MultiplyExt         => VkBlendOp.MultiplyExt         ,
			BlendOp.ScreenExt           => VkBlendOp.ScreenExt           ,
			BlendOp.OverlayExt          => VkBlendOp.OverlayExt          ,
			BlendOp.DarkenExt           => VkBlendOp.DarkenExt           ,
			BlendOp.LightenExt          => VkBlendOp.LightenExt          ,
			BlendOp.ColordodgeExt       => VkBlendOp.ColordodgeExt       ,
			BlendOp.ColorburnExt        => VkBlendOp.ColorburnExt        ,
			BlendOp.HardlightExt        => VkBlendOp.HardlightExt        ,
			BlendOp.SoftlightExt        => VkBlendOp.SoftlightExt        ,
			BlendOp.DifferenceExt       => VkBlendOp.DifferenceExt       ,
			BlendOp.ExclusionExt        => VkBlendOp.ExclusionExt        ,
			BlendOp.InvertExt           => VkBlendOp.InvertExt           ,
			BlendOp.InvertRgbExt        => VkBlendOp.InvertRgbExt        ,
			BlendOp.LineardodgeExt      => VkBlendOp.LineardodgeExt      ,
			BlendOp.LinearburnExt       => VkBlendOp.LinearburnExt       ,
			BlendOp.VividlightExt       => VkBlendOp.VividlightExt       ,
			BlendOp.LinearlightExt      => VkBlendOp.LinearlightExt      ,
			BlendOp.PinlightExt         => VkBlendOp.PinlightExt         ,
			BlendOp.HardmixExt          => VkBlendOp.HardmixExt          ,
			BlendOp.HslHueExt           => VkBlendOp.HslHueExt           ,
			BlendOp.HslSaturationExt    => VkBlendOp.HslSaturationExt    ,
			BlendOp.HslColorExt         => VkBlendOp.HslColorExt         ,
			BlendOp.HslLuminosityExt    => VkBlendOp.HslLuminosityExt    ,
			BlendOp.PlusExt             => VkBlendOp.PlusExt             ,
			BlendOp.PlusClampedExt      => VkBlendOp.PlusClampedExt      ,
			BlendOp.PlusClampedAlphaExt => VkBlendOp.PlusClampedAlphaExt ,
			BlendOp.PlusDarkerExt       => VkBlendOp.PlusDarkerExt       ,
			BlendOp.MinusExt            => VkBlendOp.MinusExt            ,
			BlendOp.MinusClampedExt     => VkBlendOp.MinusClampedExt     ,
			BlendOp.ContrastExt         => VkBlendOp.ContrastExt         ,
			BlendOp.InvertOvgExt        => VkBlendOp.InvertOvgExt        ,
			BlendOp.RedExt              => VkBlendOp.RedExt              ,
			BlendOp.GreenExt            => VkBlendOp.GreenExt            ,
			BlendOp.BlueExt             => VkBlendOp.BlueExt             ,
		};
	}
}