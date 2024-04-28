namespace Gfx;

public static class VulkanPolygonModeExtensions
{
	public static Silk.NET.Vulkan.PolygonMode ToVulkanPolygonMode(this PolygonMode polygonMode)
	{
		return polygonMode switch
		{
			PolygonMode.Fill => Silk.NET.Vulkan.PolygonMode.Fill,
			PolygonMode.Line => Silk.NET.Vulkan.PolygonMode.Line,
			PolygonMode.Point => Silk.NET.Vulkan.PolygonMode.Point,
			PolygonMode.FillRectangleNV => Silk.NET.Vulkan.PolygonMode.FillRectangleNV,
		};
	}
}