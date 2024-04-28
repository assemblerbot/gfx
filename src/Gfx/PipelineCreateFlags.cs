namespace Gfx;

[Flags]
public enum PipelineCreateFlags
{
	None                                                        = 0,
	CreateDisableOptimizationBit                                = 0x00000001,
	CreateAllowDerivativesBit                                   = 0x00000002,
	CreateDerivativeBit                                         = 0x00000004,
	CreateRenderingFragmentShadingRateAttachmentBitKhr          = 0x00200000,
	RasterizationStateCreateFragmentShadingRateAttachmentBitKhr = 0x00200000,
	CreateRenderingFragmentDensityMapAttachmentBitExt           = 0x00400000,
	RasterizationStateCreateFragmentDensityMapAttachmentBitExt  = 0x00400000,
	CreateViewIndexFromDeviceIndexBitKhr                        = 0x00000008,
	CreateDispatchBaseKhr                                       = 0x00000010,
	CreateRayTracingNoNullAnyHitShadersBitKhr                   = 0x00004000,
	CreateRayTracingNoNullClosestHitShadersBitKhr               = 0x00008000,
	CreateRayTracingNoNullMissShadersBitKhr                     = 0x00010000,
	CreateRayTracingNoNullIntersectionShadersBitKhr             = 0x00020000,
	CreateRayTracingSkipTrianglesBitKhr                         = 0x00001000,
	CreateRayTracingSkipAabbsBitKhr                             = 0x00002000,
	CreateRayTracingShaderGroupHandleCaptureReplayBitKhr        = 0x00080000,
	CreateDeferCompileBitNV                                     = 0x00000020,
	CreateCaptureStatisticsBitKhr                               = 0x00000040,
	CreateCaptureInternalRepresentationsBitKhr                  = 0x00000080,
	CreateIndirectBindableBitNV                                 = 0x00040000,
	CreateLibraryBitKhr                                         = 0x00000800,
	CreateFailOnPipelineCompileRequiredBitExt                   = 0x00000100,
	CreateEarlyReturnOnFailureBitExt                            = 0x00000200,
	CreateDescriptorBufferBitExt                                = 0x20000000,
	CreateRetainLinkTimeOptimizationInfoBitExt                  = 0x00800000,
	CreateLinkTimeOptimizationBitExt                            = 0x00000400,
	CreateRayTracingAllowMotionBitNV                            = 0x00100000,
	CreateColorAttachmentFeedbackLoopBitExt                     = 0x02000000,
	CreateDepthStencilAttachmentFeedbackLoopBitExt              = 0x04000000,
	CreateRayTracingOpacityMicromapBitExt                       = 0x01000000,
	CreateRayTracingDisplacementMicromapBitNV                   = 0x10000000,
	CreateNoProtectedAccessBitExt                               = 0x08000000,
	CreateProtectedAccessOnlyBitExt                             = 0x40000000,
	CreateViewIndexFromDeviceIndexBit                           = 0x00000008,
	CreateDispatchBaseBit                                       = CreateDispatchBaseKhr,                     // 0x00000010
	CreateDispatchBase                                          = CreateDispatchBaseBit,                     // 0x00000010
	CreateFailOnPipelineCompileRequiredBit                      = CreateFailOnPipelineCompileRequiredBitExt, // 0x00000100
	CreateEarlyReturnOnFailureBit                               = CreateEarlyReturnOnFailureBitExt,          // 0x00000200
}