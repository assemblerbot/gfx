using VkPipelineCreateFlags = Silk.NET.Vulkan.PipelineCreateFlags;

namespace Gfx;

public static class VulkanPipelineCreateFlagsExtensions
{
	public static VkPipelineCreateFlags ToVulkan(this PipelineCreateFlags flags)
	{
		VkPipelineCreateFlags result = VkPipelineCreateFlags.None;

		if ((flags & PipelineCreateFlags.CreateDisableOptimizationBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDisableOptimizationBit;
		}

		if ((flags & PipelineCreateFlags.CreateAllowDerivativesBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateAllowDerivativesBit;
		}

		if ((flags & PipelineCreateFlags.CreateDerivativeBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDerivativeBit;
		}

		if ((flags & PipelineCreateFlags.CreateRenderingFragmentShadingRateAttachmentBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRenderingFragmentShadingRateAttachmentBitKhr;
		}

		if ((flags & PipelineCreateFlags.RasterizationStateCreateFragmentShadingRateAttachmentBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.RasterizationStateCreateFragmentShadingRateAttachmentBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRenderingFragmentDensityMapAttachmentBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRenderingFragmentDensityMapAttachmentBitExt;
		}

		if ((flags & PipelineCreateFlags.RasterizationStateCreateFragmentDensityMapAttachmentBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.RasterizationStateCreateFragmentDensityMapAttachmentBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateViewIndexFromDeviceIndexBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateViewIndexFromDeviceIndexBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateDispatchBaseKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDispatchBaseKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullAnyHitShadersBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingNoNullAnyHitShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullClosestHitShadersBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingNoNullClosestHitShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullMissShadersBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingNoNullMissShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullIntersectionShadersBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingNoNullIntersectionShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingSkipTrianglesBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingSkipTrianglesBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingSkipAabbsBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingSkipAabbsBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingShaderGroupHandleCaptureReplayBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingShaderGroupHandleCaptureReplayBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateDeferCompileBitNV) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDeferCompileBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateCaptureStatisticsBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateCaptureStatisticsBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateCaptureInternalRepresentationsBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateCaptureInternalRepresentationsBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateIndirectBindableBitNV) != 0)
		{
			result |= VkPipelineCreateFlags.CreateIndirectBindableBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateLibraryBitKhr) != 0)
		{
			result |= VkPipelineCreateFlags.CreateLibraryBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateFailOnPipelineCompileRequiredBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateFailOnPipelineCompileRequiredBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateEarlyReturnOnFailureBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateEarlyReturnOnFailureBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateDescriptorBufferBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDescriptorBufferBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRetainLinkTimeOptimizationInfoBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRetainLinkTimeOptimizationInfoBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateLinkTimeOptimizationBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateLinkTimeOptimizationBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingAllowMotionBitNV) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingAllowMotionBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateColorAttachmentFeedbackLoopBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateColorAttachmentFeedbackLoopBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateDepthStencilAttachmentFeedbackLoopBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDepthStencilAttachmentFeedbackLoopBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingOpacityMicromapBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingOpacityMicromapBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingDisplacementMicromapBitNV) != 0)
		{
			result |= VkPipelineCreateFlags.CreateRayTracingDisplacementMicromapBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateNoProtectedAccessBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateNoProtectedAccessBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateProtectedAccessOnlyBitExt) != 0)
		{
			result |= VkPipelineCreateFlags.CreateProtectedAccessOnlyBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateViewIndexFromDeviceIndexBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateViewIndexFromDeviceIndexBit;
		}

		if ((flags & PipelineCreateFlags.CreateDispatchBaseBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDispatchBaseBit;
		}

		if ((flags & PipelineCreateFlags.CreateDispatchBase) != 0)
		{
			result |= VkPipelineCreateFlags.CreateDispatchBase;
		}

		if ((flags & PipelineCreateFlags.CreateFailOnPipelineCompileRequiredBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateFailOnPipelineCompileRequiredBit;
		}

		if ((flags & PipelineCreateFlags.CreateEarlyReturnOnFailureBit) != 0)
		{
			result |= VkPipelineCreateFlags.CreateEarlyReturnOnFailureBit;
		}

		return result;
	}
}