namespace Gfx;

public static class VulkanLogicOpExtensions
{
	public static Silk.NET.Vulkan.LogicOp ToVulkanLogicOp(this LogicOp logicOp)
	{
		return logicOp switch
		{
			LogicOp.Clear        => Silk.NET.Vulkan.LogicOp.Clear,
			LogicOp.And          => Silk.NET.Vulkan.LogicOp.And,
			LogicOp.AndReverse   => Silk.NET.Vulkan.LogicOp.AndReverse,
			LogicOp.Copy         => Silk.NET.Vulkan.LogicOp.Copy,
			LogicOp.AndInverted  => Silk.NET.Vulkan.LogicOp.AndInverted,
			LogicOp.NoOp         => Silk.NET.Vulkan.LogicOp.NoOp,
			LogicOp.Xor          => Silk.NET.Vulkan.LogicOp.Xor,
			LogicOp.Or           => Silk.NET.Vulkan.LogicOp.Or,
			LogicOp.Nor          => Silk.NET.Vulkan.LogicOp.Nor,
			LogicOp.Equivalent   => Silk.NET.Vulkan.LogicOp.Equivalent,
			LogicOp.Invert       => Silk.NET.Vulkan.LogicOp.Invert,
			LogicOp.OrReverse    => Silk.NET.Vulkan.LogicOp.OrReverse,
			LogicOp.CopyInverted => Silk.NET.Vulkan.LogicOp.CopyInverted,
			LogicOp.OrInverted   => Silk.NET.Vulkan.LogicOp.OrInverted,
			LogicOp.Nand         => Silk.NET.Vulkan.LogicOp.Nand,
			LogicOp.Set          => Silk.NET.Vulkan.LogicOp.Set,
		};
	}
}