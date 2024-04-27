namespace Gfx;

[Flags]
public enum SamplerFlags
{
	None                              = 0,
	SubsampledExt                     = 1 << 0,
	SubsampledCoarseReconstructionExt = 1 << 1,
	NonSeamlessCubeMapExt             = 1 << 2,
	DescriptorBufferCaptureReplayExt  = 1 << 3,
	ImageProcessingQCom               = 1 << 4,
}