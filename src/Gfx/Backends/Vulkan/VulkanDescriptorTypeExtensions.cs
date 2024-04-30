using VkDescriptorType = Silk.NET.Vulkan.DescriptorType;

namespace Gfx;

public static class VulkanDescriptorTypeExtensions
{
	public static VkDescriptorType ToVulkan(this DescriptorKind type)
	{
		return type switch
		{
		DescriptorKind.Sampler                  => VkDescriptorType.Sampler                 ,
		DescriptorKind.CombinedImageSampler     => VkDescriptorType.CombinedImageSampler    ,
		DescriptorKind.SampledImage             => VkDescriptorType.SampledImage            ,
		DescriptorKind.StorageImage             => VkDescriptorType.StorageImage            ,
		DescriptorKind.UniformTexelBuffer       => VkDescriptorType.UniformTexelBuffer      ,
		DescriptorKind.StorageTexelBuffer       => VkDescriptorType.StorageTexelBuffer      ,
		DescriptorKind.UniformBuffer            => VkDescriptorType.UniformBuffer           ,
		DescriptorKind.StorageBuffer            => VkDescriptorType.StorageBuffer           ,
		DescriptorKind.UniformBufferDynamic     => VkDescriptorType.UniformBufferDynamic    ,
		DescriptorKind.StorageBufferDynamic     => VkDescriptorType.StorageBufferDynamic    ,
		DescriptorKind.InputAttachment          => VkDescriptorType.InputAttachment         ,
		DescriptorKind.InlineUniformBlock       => VkDescriptorType.InlineUniformBlock      ,
		DescriptorKind.AccelerationStructureKhr => VkDescriptorType.AccelerationStructureKhr,
		DescriptorKind.AccelerationStructureNV  => VkDescriptorType.AccelerationStructureNV ,
		DescriptorKind.MutableValve             => VkDescriptorType.MutableValve            ,
		DescriptorKind.SampleWeightImageQCom    => VkDescriptorType.SampleWeightImageQCom   ,
		DescriptorKind.BlockMatchImageQCom      => VkDescriptorType.BlockMatchImageQCom     ,
		};
	}
}