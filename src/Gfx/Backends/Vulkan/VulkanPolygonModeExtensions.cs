using VkPolygonMode = Silk.NET.Vulkan.PolygonMode;

namespace Gfx;

public static class VulkanPolygonModeExtensions
{
	public static VkPolygonMode ToVulkanPolygonMode(this PolygonMode polygonMode)
	{
		return polygonMode switch
		{
			PolygonMode.Fill => VkPolygonMode.Fill,
			PolygonMode.Line => VkPolygonMode.Line,
			PolygonMode.Point => VkPolygonMode.Point,
			PolygonMode.FillRectangleNV => VkPolygonMode.FillRectangleNV,
		};
	}
}