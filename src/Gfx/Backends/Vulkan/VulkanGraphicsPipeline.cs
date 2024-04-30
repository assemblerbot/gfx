using Silk.NET.Vulkan;

namespace Gfx;

public sealed unsafe class VulkanGraphicsPipeline : GraphicsPipeline
{
	private readonly VulkanApi                _api;
	private readonly VulkanLogicalDevice      _logicalDevice;
	private readonly Silk.NET.Vulkan.Pipeline _pipeline;
	
	public VulkanGraphicsPipeline(VulkanApi api, VulkanLogicalDevice logicalDevice, GraphicsPipelineOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;

		PipelineShaderStageCreateInfo[] stages = new PipelineShaderStageCreateInfo[options.Shaders.Length];
		for (int i = 0; i < options.Shaders.Length; ++i)
		{
			stages[i] = ((VulkanShader) options.Shaders[i]).StageInfo;
		}

		fixed (PipelineShaderStageCreateInfo* stagesPtr = stages)
		{
			GraphicsPipelineCreateInfo pipelineInfo = new()
			                                          {
				                                          SType      = StructureType.GraphicsPipelineCreateInfo,
				                                          
				                                          Flags = options.Flags.ToVulkan(),
				                                          
				                                          StageCount = (uint) options.Shaders.Length,
				                                          PStages    = stagesPtr,
				                                          
				                                          //PVertexInputState = &options.VertexInputState,
				                                          
			                                          };
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyPipeline(_logicalDevice.Device, _pipeline, null);
	}
}