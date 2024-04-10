using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDebugMessageKindExtensions
{
	public static DebugMessageKind ToGfxDebugMessageKind(this DebugUtilsMessageTypeFlagsEXT @this)
	{
		if ((@this & DebugUtilsMessageTypeFlagsEXT.PerformanceBitExt) != 0)
		{
			return DebugMessageKind.Performance;
		}

		if ((@this & DebugUtilsMessageTypeFlagsEXT.ValidationBitExt) != 0)
		{
			return DebugMessageKind.Validation;
		}

		if ((@this & DebugUtilsMessageTypeFlagsEXT.DeviceAddressBindingBitExt) != 0)
		{
			return DebugMessageKind.Binding;
		}

		return DebugMessageKind.General;
	}
}