using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanGfxDebugMessageSeverityExtensions
{
	public static DebugMessageSeverity ToGfxDebugMessageSeverity(this DebugUtilsMessageSeverityFlagsEXT @this)
	{
		if ((@this & DebugUtilsMessageSeverityFlagsEXT.ErrorBitExt) != 0)
		{
			return DebugMessageSeverity.Error;
		}

		if ((@this & DebugUtilsMessageSeverityFlagsEXT.WarningBitExt) != 0)
		{
			return DebugMessageSeverity.Warning;
		}

		return DebugMessageSeverity.Info;
	}
}