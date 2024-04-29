using VkDescriptorType = Silk.NET.Vulkan.DescriptorType;

namespace Gfx;

public static class VulkanDescriptorTypeExtensions
{
	public static VkDescriptorType ToVulkanDescriptorType(this DescriptorType type)
	{
		return type switch
		{
		DescriptorType.Sampler                  => VkDescriptorType.Sampler                 ,
		DescriptorType.CombinedImageSampler     => VkDescriptorType.CombinedImageSampler    ,
		DescriptorType.SampledImage             => VkDescriptorType.SampledImage            ,
		DescriptorType.StorageImage             => VkDescriptorType.StorageImage            ,
		DescriptorType.UniformTexelBuffer       => VkDescriptorType.UniformTexelBuffer      ,
		DescriptorType.StorageTexelBuffer       => VkDescriptorType.StorageTexelBuffer      ,
		DescriptorType.UniformBuffer            => VkDescriptorType.UniformBuffer           ,
		DescriptorType.StorageBuffer            => VkDescriptorType.StorageBuffer           ,
		DescriptorType.UniformBufferDynamic     => VkDescriptorType.UniformBufferDynamic    ,
		DescriptorType.StorageBufferDynamic     => VkDescriptorType.StorageBufferDynamic    ,
		DescriptorType.InputAttachment          => VkDescriptorType.InputAttachment         ,
		DescriptorType.InlineUniformBlock       => VkDescriptorType.InlineUniformBlock      ,
		DescriptorType.AccelerationStructureKhr => VkDescriptorType.AccelerationStructureKhr,
		DescriptorType.AccelerationStructureNV  => VkDescriptorType.AccelerationStructureNV ,
		DescriptorType.MutableValve             => VkDescriptorType.MutableValve            ,
		DescriptorType.SampleWeightImageQCom    => VkDescriptorType.SampleWeightImageQCom   ,
		DescriptorType.BlockMatchImageQCom      => VkDescriptorType.BlockMatchImageQCom     ,
		};
	}
}