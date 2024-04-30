using VkFilter = Silk.NET.Vulkan.Filter;

namespace Gfx;

public static class VulkanFilterExtensions
{
	public static VkFilter ToVulkan(this Filter filter)
	{
		return filter switch
		{
			Filter.Nearest => VkFilter.Nearest,
			Filter.Linear => VkFilter.Linear,
			Filter.Cubic => VkFilter.CubicImg,
		};
	}
}