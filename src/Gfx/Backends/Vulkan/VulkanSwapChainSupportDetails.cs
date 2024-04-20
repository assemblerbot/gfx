using Silk.NET.Vulkan;

namespace Gfx;

public sealed class VulkanSwapChainSupportDetails
{
	public SurfaceCapabilitiesKHR Capabilities;
	public SurfaceFormatKHR[]     Formats;
	public PresentModeKHR[]       PresentModes;
}