using Silk.NET.Vulkan.Extensions.KHR;

namespace Gfx;

public static class VulkanExtensions
{
	public static readonly string[] GraphicsExtensions = new[]
    {
        KhrSwapchain.ExtensionName
    };
}