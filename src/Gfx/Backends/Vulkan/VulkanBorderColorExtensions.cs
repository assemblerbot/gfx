namespace Gfx;

public static class VulkanBorderColorExtensions
{
	public static Silk.NET.Vulkan.BorderColor ToVulkanBorderColor(this BorderColor borderColor)
	{
		return borderColor switch
		{
			BorderColor.FloatTransparentBlack => Silk.NET.Vulkan.BorderColor.FloatTransparentBlack,
			BorderColor.IntTransparentBlack   => Silk.NET.Vulkan.BorderColor.IntTransparentBlack,
			BorderColor.FloatOpaqueBlack      => Silk.NET.Vulkan.BorderColor.FloatOpaqueBlack,
			BorderColor.IntOpaqueBlack        => Silk.NET.Vulkan.BorderColor.IntOpaqueBlack,
			BorderColor.FloatOpaqueWhite      => Silk.NET.Vulkan.BorderColor.FloatOpaqueWhite,
			BorderColor.IntOpaqueWhite        => Silk.NET.Vulkan.BorderColor.IntOpaqueWhite,
			BorderColor.FloatCustomExt        => Silk.NET.Vulkan.BorderColor.FloatCustomExt,
			BorderColor.IntCustomExt          => Silk.NET.Vulkan.BorderColor.IntCustomExt,
		};
	}
}