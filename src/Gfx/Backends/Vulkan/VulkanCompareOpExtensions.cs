namespace Gfx;

public static class VulkanCompareOpExtensions
{
	public static Silk.NET.Vulkan.CompareOp ToVulkanCompareOp(this CompareOp op)
	{
		return op switch
		{
			CompareOp.Never          => Silk.NET.Vulkan.CompareOp.Never,
			CompareOp.Less           => Silk.NET.Vulkan.CompareOp.Less,
			CompareOp.Equal          => Silk.NET.Vulkan.CompareOp.Equal,
			CompareOp.LessOrEqual    => Silk.NET.Vulkan.CompareOp.LessOrEqual,
			CompareOp.Greater        => Silk.NET.Vulkan.CompareOp.Greater,
			CompareOp.NotEqual       => Silk.NET.Vulkan.CompareOp.NotEqual,
			CompareOp.GreaterOrEqual => Silk.NET.Vulkan.CompareOp.GreaterOrEqual,
			CompareOp.Always         => Silk.NET.Vulkan.CompareOp.Always,
		};
	}
}