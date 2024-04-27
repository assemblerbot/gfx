namespace Gfx;

public static class VulkanDescriptorTypeExtensions
{
	public static Silk.NET.Vulkan.DescriptorType ToVulkanDescriptorType(this DescriptorType type)
	{
		return type switch
		{
		DescriptorType.Sampler                  => Silk.NET.Vulkan.DescriptorType.Sampler                 ,
		DescriptorType.CombinedImageSampler     => Silk.NET.Vulkan.DescriptorType.CombinedImageSampler    ,
		DescriptorType.SampledImage             => Silk.NET.Vulkan.DescriptorType.SampledImage            ,
		DescriptorType.StorageImage             => Silk.NET.Vulkan.DescriptorType.StorageImage            ,
		DescriptorType.UniformTexelBuffer       => Silk.NET.Vulkan.DescriptorType.UniformTexelBuffer      ,
		DescriptorType.StorageTexelBuffer       => Silk.NET.Vulkan.DescriptorType.StorageTexelBuffer      ,
		DescriptorType.UniformBuffer            => Silk.NET.Vulkan.DescriptorType.UniformBuffer           ,
		DescriptorType.StorageBuffer            => Silk.NET.Vulkan.DescriptorType.StorageBuffer           ,
		DescriptorType.UniformBufferDynamic     => Silk.NET.Vulkan.DescriptorType.UniformBufferDynamic    ,
		DescriptorType.StorageBufferDynamic     => Silk.NET.Vulkan.DescriptorType.StorageBufferDynamic    ,
		DescriptorType.InputAttachment          => Silk.NET.Vulkan.DescriptorType.InputAttachment         ,
		DescriptorType.InlineUniformBlock       => Silk.NET.Vulkan.DescriptorType.InlineUniformBlock      ,
		DescriptorType.AccelerationStructureKhr => Silk.NET.Vulkan.DescriptorType.AccelerationStructureKhr,
		DescriptorType.AccelerationStructureNV  => Silk.NET.Vulkan.DescriptorType.AccelerationStructureNV ,
		DescriptorType.MutableValve             => Silk.NET.Vulkan.DescriptorType.MutableValve            ,
		DescriptorType.SampleWeightImageQCom    => Silk.NET.Vulkan.DescriptorType.SampleWeightImageQCom   ,
		DescriptorType.BlockMatchImageQCom      => Silk.NET.Vulkan.DescriptorType.BlockMatchImageQCom     ,
		};
	}
}