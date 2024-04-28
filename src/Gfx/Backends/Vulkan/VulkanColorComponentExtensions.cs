using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanColorComponentExtensions
{
	public static ColorComponentFlags ToVulkanColorComponentFlags(this ColorComponent component)
	{
		ColorComponentFlags flags = ColorComponentFlags.None;

		if ((component & ColorComponent.R) != 0)
		{
			flags |= ColorComponentFlags.RBit;
		}
		if ((component & ColorComponent.G) != 0)
		{
			flags |= ColorComponentFlags.GBit;
		}
		if ((component & ColorComponent.B) != 0)
		{
			flags |= ColorComponentFlags.BBit;
		}
		if ((component & ColorComponent.A) != 0)
		{
			flags |= ColorComponentFlags.ABit;
		}

		return flags;
	}
}