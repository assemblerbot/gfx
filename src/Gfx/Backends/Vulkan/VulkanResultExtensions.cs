namespace Gfx;

using VkResult = Silk.NET.Vulkan.Result;

public static class VulkanResultExtensions
{
	public static GfxResult ToGfx(this VkResult result)
	{
		return result switch
		{
		    VkResult.ErrorCompressionExhaustedExt => GfxResult.ErrorCompressionExhaustedExt,
		    VkResult.ErrorInvalidVideoStdParametersKhr => GfxResult.ErrorInvalidVideoStdParametersKhr,
		    VkResult.ErrorNoPipelineMatch => GfxResult.ErrorNoPipelineMatch,
		    VkResult.ErrorInvalidPipelineCacheData => GfxResult.ErrorInvalidPipelineCacheData,
		    VkResult.ErrorInvalidDeviceAddressExt => GfxResult.ErrorInvalidDeviceAddressExt,
		    VkResult.ErrorFullScreenExclusiveModeLostExt => GfxResult.ErrorFullScreenExclusiveModeLostExt,
		    VkResult.ErrorNotPermittedExt => GfxResult.ErrorNotPermittedExt,
		    VkResult.ErrorFragmentation => GfxResult.ErrorFragmentation,
		    VkResult.ErrorInvalidDrmFormatModifierPlaneLayoutExt => GfxResult.ErrorInvalidDrmFormatModifierPlaneLayoutExt,
		    VkResult.ErrorInvalidExternalHandle => GfxResult.ErrorInvalidExternalHandle,
		    VkResult.ErrorOutOfPoolMemory => GfxResult.ErrorOutOfPoolMemory,
		    VkResult.ErrorVideoStdVersionNotSupportedKhr => GfxResult.ErrorVideoStdVersionNotSupportedKhr,
		    VkResult.ErrorVideoProfileCodecNotSupportedKhr => GfxResult.ErrorVideoProfileCodecNotSupportedKhr,
		    VkResult.ErrorVideoProfileFormatNotSupportedKhr => GfxResult.ErrorVideoProfileFormatNotSupportedKhr,
		    VkResult.ErrorVideoProfileOperationNotSupportedKhr => GfxResult.ErrorVideoProfileOperationNotSupportedKhr,
		    VkResult.ErrorVideoPictureLayoutNotSupportedKhr => GfxResult.ErrorVideoPictureLayoutNotSupportedKhr,
		    VkResult.ErrorImageUsageNotSupportedKhr => GfxResult.ErrorImageUsageNotSupportedKhr,
		    VkResult.ErrorInvalidShaderNV => GfxResult.ErrorInvalidShaderNV,
		    VkResult.ErrorValidationFailed => GfxResult.ErrorValidationFailed,
		    VkResult.ErrorIncompatibleDisplayKhr => GfxResult.ErrorIncompatibleDisplayKhr,
		    VkResult.ErrorOutOfDateKhr => GfxResult.ErrorOutOfDateKhr,
		    VkResult.ErrorNativeWindowInUseKhr => GfxResult.ErrorNativeWindowInUseKhr,
		    VkResult.ErrorSurfaceLostKhr => GfxResult.ErrorSurfaceLostKhr,
		    VkResult.ErrorUnknown => GfxResult.ErrorUnknown,
		    VkResult.ErrorFragmentedPool => GfxResult.ErrorFragmentedPool,
		    VkResult.ErrorFormatNotSupported => GfxResult.ErrorFormatNotSupported,
		    VkResult.ErrorTooManyObjects => GfxResult.ErrorTooManyObjects,
		    VkResult.ErrorIncompatibleDriver => GfxResult.ErrorIncompatibleDriver,
		    VkResult.ErrorFeatureNotPresent => GfxResult.ErrorFeatureNotPresent,
		    VkResult.ErrorExtensionNotPresent => GfxResult.ErrorExtensionNotPresent,
		    VkResult.ErrorLayerNotPresent => GfxResult.ErrorLayerNotPresent,
		    VkResult.ErrorMemoryMapFailed => GfxResult.ErrorMemoryMapFailed,
		    VkResult.ErrorDeviceLost => GfxResult.ErrorDeviceLost,
		    VkResult.ErrorInitializationFailed => GfxResult.ErrorInitializationFailed,
		    VkResult.ErrorOutOfDeviceMemory => GfxResult.ErrorOutOfDeviceMemory,
		    VkResult.ErrorOutOfHostMemory => GfxResult.ErrorOutOfHostMemory,
		    VkResult.Success => GfxResult.Success,
		    VkResult.NotReady => GfxResult.NotReady,
		    VkResult.Timeout => GfxResult.Timeout,
		    VkResult.EventSet => GfxResult.EventSet,
		    VkResult.EventReset => GfxResult.EventReset,
		    VkResult.Incomplete => GfxResult.Incomplete,
		    VkResult.SuboptimalKhr => GfxResult.SuboptimalKhr,
		    VkResult.ThreadIdleKhr => GfxResult.ThreadIdleKhr,
		    VkResult.ThreadDoneKhr => GfxResult.ThreadDoneKhr,
		    VkResult.OperationDeferredKhr => GfxResult.OperationDeferredKhr,
		    VkResult.OperationNotDeferredKhr => GfxResult.OperationNotDeferredKhr,
		    VkResult.ErrorPipelineCompileRequiredExt => GfxResult.ErrorPipelineCompileRequiredExt,
		    VkResult.ErrorIncompatibleShaderBinaryExt => GfxResult.ErrorIncompatibleShaderBinaryExt,
		};
	}
}