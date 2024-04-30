using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanCommandBufferUsageExtensions
{
	public static CommandBufferUsageFlags ToVulkan(this CommandBufferUsage usage)
	{
		CommandBufferUsageFlags flags = CommandBufferUsageFlags.None;
		if ((usage & CommandBufferUsage.OneTimeSubmit) != 0)
		{
			flags |= CommandBufferUsageFlags.OneTimeSubmitBit;
		}

		if ((usage & CommandBufferUsage.RenderPassContinue) != 0)
		{
			flags |= CommandBufferUsageFlags.RenderPassContinueBit;
		}

		if ((usage & CommandBufferUsage.SimultaneousUse) != 0)
		{
			flags |= CommandBufferUsageFlags.SimultaneousUseBit;
		}
		return flags;
	}
}