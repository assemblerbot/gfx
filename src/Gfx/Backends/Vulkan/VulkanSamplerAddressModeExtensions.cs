namespace Gfx;

public static class VulkanSamplerAddressModeExtensions
{
	public static Silk.NET.Vulkan.SamplerAddressMode ToVulkanSamplerAddressMode(this SamplerAddressMode mode)
	{
		return mode switch
		{
			SamplerAddressMode.Repeat            => Silk.NET.Vulkan.SamplerAddressMode.Repeat,
			SamplerAddressMode.MirroredRepeat    => Silk.NET.Vulkan.SamplerAddressMode.MirroredRepeat,
			SamplerAddressMode.ClampToEdge       => Silk.NET.Vulkan.SamplerAddressMode.ClampToEdge,
			SamplerAddressMode.ClampToBorder     => Silk.NET.Vulkan.SamplerAddressMode.ClampToBorder,
			SamplerAddressMode.MirrorClampToEdge => Silk.NET.Vulkan.SamplerAddressMode.MirrorClampToEdge,
		};
	}
}