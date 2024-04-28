namespace Gfx;

public static class VulkanFrontFaceExtensions
{
	public static Silk.NET.Vulkan.FrontFace ToVulkanFrontFace(this FrontFace frontFace)
	{
		return frontFace switch
		{
			FrontFace.CounterClockwise => Silk.NET.Vulkan.FrontFace.CounterClockwise,
			FrontFace.Clockwise => Silk.NET.Vulkan.FrontFace.Clockwise,
		};
	}
}