using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanCullModeExtensions
{
	public static CullModeFlags ToVulkan(this CullMode cullMode)
	{
		CullModeFlags flags = CullModeFlags.None;
		if ((cullMode & CullMode.Front) != 0)
		{
			flags |= CullModeFlags.FrontBit;
		}

		if ((cullMode & CullMode.Back) != 0)
		{
			flags |= CullModeFlags.BackBit;
		}

		return flags;
	}
}