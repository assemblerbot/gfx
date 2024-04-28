namespace Gfx;

public static class VulkanPipelineCreateFlagsExtensions
{
	public static Silk.NET.Vulkan.PipelineCreateFlags ToVulkanPipelineCreateFlags(this PipelineCreateFlags flags)
	{
		Silk.NET.Vulkan.PipelineCreateFlags result = Silk.NET.Vulkan.PipelineCreateFlags.None;

		if ((flags & PipelineCreateFlags.CreateDisableOptimizationBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDisableOptimizationBit;
		}

		if ((flags & PipelineCreateFlags.CreateAllowDerivativesBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateAllowDerivativesBit;
		}

		if ((flags & PipelineCreateFlags.CreateDerivativeBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDerivativeBit;
		}

		if ((flags & PipelineCreateFlags.CreateRenderingFragmentShadingRateAttachmentBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRenderingFragmentShadingRateAttachmentBitKhr;
		}

		if ((flags & PipelineCreateFlags.RasterizationStateCreateFragmentShadingRateAttachmentBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.RasterizationStateCreateFragmentShadingRateAttachmentBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRenderingFragmentDensityMapAttachmentBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRenderingFragmentDensityMapAttachmentBitExt;
		}

		if ((flags & PipelineCreateFlags.RasterizationStateCreateFragmentDensityMapAttachmentBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.RasterizationStateCreateFragmentDensityMapAttachmentBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateViewIndexFromDeviceIndexBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateViewIndexFromDeviceIndexBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateDispatchBaseKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDispatchBaseKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullAnyHitShadersBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingNoNullAnyHitShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullClosestHitShadersBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingNoNullClosestHitShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullMissShadersBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingNoNullMissShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingNoNullIntersectionShadersBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingNoNullIntersectionShadersBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingSkipTrianglesBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingSkipTrianglesBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingSkipAabbsBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingSkipAabbsBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingShaderGroupHandleCaptureReplayBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingShaderGroupHandleCaptureReplayBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateDeferCompileBitNV) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDeferCompileBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateCaptureStatisticsBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateCaptureStatisticsBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateCaptureInternalRepresentationsBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateCaptureInternalRepresentationsBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateIndirectBindableBitNV) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateIndirectBindableBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateLibraryBitKhr) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateLibraryBitKhr;
		}

		if ((flags & PipelineCreateFlags.CreateFailOnPipelineCompileRequiredBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateFailOnPipelineCompileRequiredBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateEarlyReturnOnFailureBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateEarlyReturnOnFailureBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateDescriptorBufferBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDescriptorBufferBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRetainLinkTimeOptimizationInfoBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRetainLinkTimeOptimizationInfoBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateLinkTimeOptimizationBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateLinkTimeOptimizationBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingAllowMotionBitNV) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingAllowMotionBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateColorAttachmentFeedbackLoopBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateColorAttachmentFeedbackLoopBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateDepthStencilAttachmentFeedbackLoopBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDepthStencilAttachmentFeedbackLoopBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingOpacityMicromapBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingOpacityMicromapBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateRayTracingDisplacementMicromapBitNV) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateRayTracingDisplacementMicromapBitNV;
		}

		if ((flags & PipelineCreateFlags.CreateNoProtectedAccessBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateNoProtectedAccessBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateProtectedAccessOnlyBitExt) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateProtectedAccessOnlyBitExt;
		}

		if ((flags & PipelineCreateFlags.CreateViewIndexFromDeviceIndexBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateViewIndexFromDeviceIndexBit;
		}

		if ((flags & PipelineCreateFlags.CreateDispatchBaseBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDispatchBaseBit;
		}

		if ((flags & PipelineCreateFlags.CreateDispatchBase) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateDispatchBase;
		}

		if ((flags & PipelineCreateFlags.CreateFailOnPipelineCompileRequiredBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateFailOnPipelineCompileRequiredBit;
		}

		if ((flags & PipelineCreateFlags.CreateEarlyReturnOnFailureBit) != 0)
		{
			result |= Silk.NET.Vulkan.PipelineCreateFlags.CreateEarlyReturnOnFailureBit;
		}

		return result;
	}
}