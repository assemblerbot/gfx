using VkBorderColor = Silk.NET.Vulkan.BorderColor;

namespace Gfx;

public static class VulkanBorderColorExtensions
{
	public static VkBorderColor ToVulkan(this BorderColor borderColor)
	{
		return borderColor switch
		{
			BorderColor.FloatTransparentBlack => VkBorderColor.FloatTransparentBlack,
			BorderColor.IntTransparentBlack   => VkBorderColor.IntTransparentBlack,
			BorderColor.FloatOpaqueBlack      => VkBorderColor.FloatOpaqueBlack,
			BorderColor.IntOpaqueBlack        => VkBorderColor.IntOpaqueBlack,
			BorderColor.FloatOpaqueWhite      => VkBorderColor.FloatOpaqueWhite,
			BorderColor.IntOpaqueWhite        => VkBorderColor.IntOpaqueWhite,
			BorderColor.FloatCustomExt        => VkBorderColor.FloatCustomExt,
			BorderColor.IntCustomExt          => VkBorderColor.IntCustomExt,
		};
	}
}