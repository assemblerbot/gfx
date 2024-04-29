using VkFrontFace = Silk.NET.Vulkan.FrontFace;

namespace Gfx;

public static class VulkanFrontFaceExtensions
{
	public static VkFrontFace ToVulkanFrontFace(this FrontFace frontFace)
	{
		return frontFace switch
		{
			FrontFace.CounterClockwise => VkFrontFace.CounterClockwise,
			FrontFace.Clockwise => VkFrontFace.Clockwise,
		};
	}
}