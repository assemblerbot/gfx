namespace Gfx;

public static class VulkanBlendOpExtensions
{
	public static Silk.NET.Vulkan.BlendOp ToVulkanBlendOp(this BlendOp blendOp)
	{
		return blendOp switch
		{
			BlendOp.Add                 => Silk.NET.Vulkan.BlendOp.Add                 ,
			BlendOp.Subtract            => Silk.NET.Vulkan.BlendOp.Subtract            ,
			BlendOp.ReverseSubtract     => Silk.NET.Vulkan.BlendOp.ReverseSubtract     ,
			BlendOp.Min                 => Silk.NET.Vulkan.BlendOp.Min                 ,
			BlendOp.Max                 => Silk.NET.Vulkan.BlendOp.Max                 ,
			BlendOp.ZeroExt             => Silk.NET.Vulkan.BlendOp.ZeroExt             ,
			BlendOp.SrcExt              => Silk.NET.Vulkan.BlendOp.SrcExt              ,
			BlendOp.DstExt              => Silk.NET.Vulkan.BlendOp.DstExt              ,
			BlendOp.SrcOverExt          => Silk.NET.Vulkan.BlendOp.SrcOverExt          ,
			BlendOp.DstOverExt          => Silk.NET.Vulkan.BlendOp.DstOverExt          ,
			BlendOp.SrcInExt            => Silk.NET.Vulkan.BlendOp.SrcInExt            ,
			BlendOp.DstInExt            => Silk.NET.Vulkan.BlendOp.DstInExt            ,
			BlendOp.SrcOutExt           => Silk.NET.Vulkan.BlendOp.SrcOutExt           ,
			BlendOp.DstOutExt           => Silk.NET.Vulkan.BlendOp.DstOutExt           ,
			BlendOp.SrcAtopExt          => Silk.NET.Vulkan.BlendOp.SrcAtopExt          ,
			BlendOp.DstAtopExt          => Silk.NET.Vulkan.BlendOp.DstAtopExt          ,
			BlendOp.XorExt              => Silk.NET.Vulkan.BlendOp.XorExt              ,
			BlendOp.MultiplyExt         => Silk.NET.Vulkan.BlendOp.MultiplyExt         ,
			BlendOp.ScreenExt           => Silk.NET.Vulkan.BlendOp.ScreenExt           ,
			BlendOp.OverlayExt          => Silk.NET.Vulkan.BlendOp.OverlayExt          ,
			BlendOp.DarkenExt           => Silk.NET.Vulkan.BlendOp.DarkenExt           ,
			BlendOp.LightenExt          => Silk.NET.Vulkan.BlendOp.LightenExt          ,
			BlendOp.ColordodgeExt       => Silk.NET.Vulkan.BlendOp.ColordodgeExt       ,
			BlendOp.ColorburnExt        => Silk.NET.Vulkan.BlendOp.ColorburnExt        ,
			BlendOp.HardlightExt        => Silk.NET.Vulkan.BlendOp.HardlightExt        ,
			BlendOp.SoftlightExt        => Silk.NET.Vulkan.BlendOp.SoftlightExt        ,
			BlendOp.DifferenceExt       => Silk.NET.Vulkan.BlendOp.DifferenceExt       ,
			BlendOp.ExclusionExt        => Silk.NET.Vulkan.BlendOp.ExclusionExt        ,
			BlendOp.InvertExt           => Silk.NET.Vulkan.BlendOp.InvertExt           ,
			BlendOp.InvertRgbExt        => Silk.NET.Vulkan.BlendOp.InvertRgbExt        ,
			BlendOp.LineardodgeExt      => Silk.NET.Vulkan.BlendOp.LineardodgeExt      ,
			BlendOp.LinearburnExt       => Silk.NET.Vulkan.BlendOp.LinearburnExt       ,
			BlendOp.VividlightExt       => Silk.NET.Vulkan.BlendOp.VividlightExt       ,
			BlendOp.LinearlightExt      => Silk.NET.Vulkan.BlendOp.LinearlightExt      ,
			BlendOp.PinlightExt         => Silk.NET.Vulkan.BlendOp.PinlightExt         ,
			BlendOp.HardmixExt          => Silk.NET.Vulkan.BlendOp.HardmixExt          ,
			BlendOp.HslHueExt           => Silk.NET.Vulkan.BlendOp.HslHueExt           ,
			BlendOp.HslSaturationExt    => Silk.NET.Vulkan.BlendOp.HslSaturationExt    ,
			BlendOp.HslColorExt         => Silk.NET.Vulkan.BlendOp.HslColorExt         ,
			BlendOp.HslLuminosityExt    => Silk.NET.Vulkan.BlendOp.HslLuminosityExt    ,
			BlendOp.PlusExt             => Silk.NET.Vulkan.BlendOp.PlusExt             ,
			BlendOp.PlusClampedExt      => Silk.NET.Vulkan.BlendOp.PlusClampedExt      ,
			BlendOp.PlusClampedAlphaExt => Silk.NET.Vulkan.BlendOp.PlusClampedAlphaExt ,
			BlendOp.PlusDarkerExt       => Silk.NET.Vulkan.BlendOp.PlusDarkerExt       ,
			BlendOp.MinusExt            => Silk.NET.Vulkan.BlendOp.MinusExt            ,
			BlendOp.MinusClampedExt     => Silk.NET.Vulkan.BlendOp.MinusClampedExt     ,
			BlendOp.ContrastExt         => Silk.NET.Vulkan.BlendOp.ContrastExt         ,
			BlendOp.InvertOvgExt        => Silk.NET.Vulkan.BlendOp.InvertOvgExt        ,
			BlendOp.RedExt              => Silk.NET.Vulkan.BlendOp.RedExt              ,
			BlendOp.GreenExt            => Silk.NET.Vulkan.BlendOp.GreenExt            ,
			BlendOp.BlueExt             => Silk.NET.Vulkan.BlendOp.BlueExt             ,
		};
	}
}