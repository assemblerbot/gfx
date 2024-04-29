using VkCompareOp = Silk.NET.Vulkan.CompareOp;

namespace Gfx;

public static class VulkanCompareOpExtensions
{
	public static VkCompareOp ToVulkanCompareOp(this CompareOp op)
	{
		return op switch
		{
			CompareOp.Never          => VkCompareOp.Never,
			CompareOp.Less           => VkCompareOp.Less,
			CompareOp.Equal          => VkCompareOp.Equal,
			CompareOp.LessOrEqual    => VkCompareOp.LessOrEqual,
			CompareOp.Greater        => VkCompareOp.Greater,
			CompareOp.NotEqual       => VkCompareOp.NotEqual,
			CompareOp.GreaterOrEqual => VkCompareOp.GreaterOrEqual,
			CompareOp.Always         => VkCompareOp.Always,
		};
	}
}