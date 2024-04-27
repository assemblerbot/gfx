namespace Gfx;

public static class VulkanSamplerMipmapModeExtensions
{
	public static Silk.NET.Vulkan.SamplerMipmapMode ToVulkanSamplerMipmapMode(this SamplerMipmapMode mode)
	{
		return mode switch
		{
			SamplerMipmapMode.Nearest => Silk.NET.Vulkan.SamplerMipmapMode.Nearest,
			SamplerMipmapMode.Linear  => Silk.NET.Vulkan.SamplerMipmapMode.Linear,
		};
	}
}