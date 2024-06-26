using VkSamplerMipmapMode = Silk.NET.Vulkan.SamplerMipmapMode;

namespace Gfx;

public static class VulkanSamplerMipmapModeExtensions
{
	public static VkSamplerMipmapMode ToVulkan(this SamplerMipmapMode mode)
	{
		return mode switch
		{
			SamplerMipmapMode.Nearest => VkSamplerMipmapMode.Nearest,
			SamplerMipmapMode.Linear  => VkSamplerMipmapMode.Linear,
		};
	}
}