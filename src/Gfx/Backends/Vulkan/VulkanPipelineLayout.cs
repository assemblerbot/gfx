using Silk.NET.Vulkan;
using VkPipelineLayout = Silk.NET.Vulkan.PipelineLayout;
using VkDescriptorSetLayout = Silk.NET.Vulkan.DescriptorSetLayout;

namespace Gfx;

public sealed unsafe class VulkanPipelineLayout : PipelineLayout
{
	private readonly VulkanApi                      _api;
	private readonly VulkanLogicalDevice            _logicalDevice;
	private readonly VkPipelineLayout _pipelineLayout;
	
	internal VulkanPipelineLayout(VulkanApi api, VulkanLogicalDevice logicalDevice, DescriptorSetLayout descriptorSetLayout)
	{
		_api           = api;
		_logicalDevice = logicalDevice;

		VulkanDescriptorSetLayout vulkanDescriptorSetLayout = (VulkanDescriptorSetLayout) descriptorSetLayout;

		Result result;
		fixed (VkDescriptorSetLayout* vulkanDescriptorSetLayoutPtr = &vulkanDescriptorSetLayout.Layout)
		{
			PipelineLayoutCreateInfo info = new()
			                                {
				                                SType                  = StructureType.PipelineLayoutCreateInfo,
				                                PushConstantRangeCount = 0,
				                                SetLayoutCount         = 1,
				                                PSetLayouts            = vulkanDescriptorSetLayoutPtr
			                                };

			result = _api.Vk.CreatePipelineLayout(_logicalDevice.Device, info, null, out _pipelineLayout);
		}

		if (result != Result.Success)
		{
			throw new GfxException($"Failed to create pipeline layout! Result: {result}");
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyPipelineLayout(_logicalDevice.Device, _pipelineLayout, null);
	}
}