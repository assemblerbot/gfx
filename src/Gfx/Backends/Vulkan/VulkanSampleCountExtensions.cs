using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanSampleCountExtensions
{
	public static SampleCountFlags ToVulkan(this SampleCount sampleCount)
	{
		SampleCountFlags result = SampleCountFlags.None;

		result |= (sampleCount & SampleCount.Count1) != 0 ? SampleCountFlags.Count1Bit : 0;
		result |= (sampleCount & SampleCount.Count2) != 0 ? SampleCountFlags.Count2Bit : 0;
		result |= (sampleCount & SampleCount.Count4) != 0 ? SampleCountFlags.Count4Bit : 0;
		result |= (sampleCount & SampleCount.Count8) != 0 ? SampleCountFlags.Count8Bit : 0;
		result |= (sampleCount & SampleCount.Count16) != 0 ? SampleCountFlags.Count16Bit : 0;
		result |= (sampleCount & SampleCount.Count32) != 0 ? SampleCountFlags.Count32Bit : 0;
		result |= (sampleCount & SampleCount.Count64) != 0 ? SampleCountFlags.Count64Bit : 0;
		
		return result;
	}
}