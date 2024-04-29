using VkLogicOp = Silk.NET.Vulkan.LogicOp;

namespace Gfx;

public static class VulkanLogicOpExtensions
{
	public static VkLogicOp ToVulkanLogicOp(this LogicOp logicOp)
	{
		return logicOp switch
		{
			LogicOp.Clear        => VkLogicOp.Clear,
			LogicOp.And          => VkLogicOp.And,
			LogicOp.AndReverse   => VkLogicOp.AndReverse,
			LogicOp.Copy         => VkLogicOp.Copy,
			LogicOp.AndInverted  => VkLogicOp.AndInverted,
			LogicOp.NoOp         => VkLogicOp.NoOp,
			LogicOp.Xor          => VkLogicOp.Xor,
			LogicOp.Or           => VkLogicOp.Or,
			LogicOp.Nor          => VkLogicOp.Nor,
			LogicOp.Equivalent   => VkLogicOp.Equivalent,
			LogicOp.Invert       => VkLogicOp.Invert,
			LogicOp.OrReverse    => VkLogicOp.OrReverse,
			LogicOp.CopyInverted => VkLogicOp.CopyInverted,
			LogicOp.OrInverted   => VkLogicOp.OrInverted,
			LogicOp.Nand         => VkLogicOp.Nand,
			LogicOp.Set          => VkLogicOp.Set,
		};
	}
}