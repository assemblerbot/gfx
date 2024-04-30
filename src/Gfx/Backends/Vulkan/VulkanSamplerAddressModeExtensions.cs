using VkSamplerAddressMode = Silk.NET.Vulkan.SamplerAddressMode;

namespace Gfx;

public static class VulkanSamplerAddressModeExtensions
{
	public static VkSamplerAddressMode ToVulkan(this SamplerAddressMode mode)
	{
		return mode switch
		{
			SamplerAddressMode.Repeat            => VkSamplerAddressMode.Repeat,
			SamplerAddressMode.MirroredRepeat    => VkSamplerAddressMode.MirroredRepeat,
			SamplerAddressMode.ClampToEdge       => VkSamplerAddressMode.ClampToEdge,
			SamplerAddressMode.ClampToBorder     => VkSamplerAddressMode.ClampToBorder,
			SamplerAddressMode.MirrorClampToEdge => VkSamplerAddressMode.MirrorClampToEdge,
		};
	}
}