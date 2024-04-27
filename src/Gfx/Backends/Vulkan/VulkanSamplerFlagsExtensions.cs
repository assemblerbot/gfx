using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanSamplerFlagsExtensions
{
	public static SamplerCreateFlags ToVulkanSamplerCreateFlags(this SamplerFlags flags)
	{
		SamplerCreateFlags result = SamplerCreateFlags.None;

		if ((flags & SamplerFlags.SubsampledExt) != 0)
		{
			result |= SamplerCreateFlags.SubsampledBitExt;
		}

		if ((flags & SamplerFlags.SubsampledCoarseReconstructionExt) != 0)
		{
			result |= SamplerCreateFlags.SubsampledCoarseReconstructionBitExt;
		}

		if ((flags & SamplerFlags.NonSeamlessCubeMapExt) != 0)
		{
			result |= SamplerCreateFlags.NonSeamlessCubeMapBitExt;
		}

		if ((flags & SamplerFlags.DescriptorBufferCaptureReplayExt) != 0)
		{
			result |= SamplerCreateFlags.DescriptorBufferCaptureReplayBitExt;
		}
		
		if ((flags & SamplerFlags.ImageProcessingQCom) != 0)
		{
			result |= SamplerCreateFlags.ImageProcessingBitQCom;
		}
		
		return result;
	}
}