namespace Gfx;

public static class VulkanFilterExtensions
{
	public static Silk.NET.Vulkan.Filter ToVulkanFilter(this Filter filter)
	{
		return filter switch
		{
			Filter.Nearest => Silk.NET.Vulkan.Filter.Nearest,
			Filter.Linear => Silk.NET.Vulkan.Filter.Linear,
			Filter.Cubic => Silk.NET.Vulkan.Filter.CubicImg,
		};
	}
}