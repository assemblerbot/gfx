using Silk.NET.Vulkan;

namespace Gfx;

using VkVertexInputBindingDescription = Silk.NET.Vulkan.VertexInputBindingDescription;
using VkVertexInputAttributeDescription = Silk.NET.Vulkan.VertexInputAttributeDescription;
using VkViewport = Silk.NET.Vulkan.Viewport;
using VkScissor = Silk.NET.Vulkan.Rect2D;
using VkPipelineColorBlendAttachmentState = Silk.NET.Vulkan.PipelineColorBlendAttachmentState;
using VkDynamicState = Silk.NET.Vulkan.DynamicState;

public sealed unsafe class VulkanGraphicsPipeline : GraphicsPipeline
{
	private readonly VulkanApi                _api;
	private readonly VulkanLogicalDevice      _logicalDevice;
	internal readonly Silk.NET.Vulkan.Pipeline Pipeline;
	
	public VulkanGraphicsPipeline(VulkanApi api, VulkanLogicalDevice logicalDevice, GraphicsPipelineOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;

		PipelineShaderStageCreateInfo* stages = stackalloc PipelineShaderStageCreateInfo[options.Shaders.Length];
		for (int i = 0; i < options.Shaders.Length; ++i)
		{
			stages[i] = ((VulkanShader) options.Shaders[i]).StageInfo;
		}

		VkVertexInputBindingDescription* vertexInputBindingDescriptions =
			stackalloc VkVertexInputBindingDescription[options.VertexInputState.VertexBindingDescriptions.Length];
		for (int i = 0; i < options.VertexInputState.VertexBindingDescriptions.Length; ++i)
		{
			vertexInputBindingDescriptions[i] = new VkVertexInputBindingDescription
			                                    {
				                                    Binding   = options.VertexInputState.VertexBindingDescriptions[i].Binding,
				                                    Stride    = options.VertexInputState.VertexBindingDescriptions[i].Stride,
				                                    InputRate = options.VertexInputState.VertexBindingDescriptions[i].InputRate.ToVulkan(),
			                                    };
		}

		VkVertexInputAttributeDescription* vertexInputAttributeDescriptions =
			stackalloc VkVertexInputAttributeDescription[options.VertexInputState.VertexAttributeDescriptions.Length];
		for (int i = 0; i < options.VertexInputState.VertexAttributeDescriptions.Length; ++i)
		{
			vertexInputAttributeDescriptions[i] = new VkVertexInputAttributeDescription
			                                      {
				                                      Location = options.VertexInputState.VertexAttributeDescriptions[i].Location,
				                                      Binding  = options.VertexInputState.VertexAttributeDescriptions[i].Binding,
				                                      Format   = options.VertexInputState.VertexAttributeDescriptions[i].DeviceFormat.ToVulkan(),
				                                      Offset   = options.VertexInputState.VertexAttributeDescriptions[i].Offset,
			                                      };
		}
		
		PipelineVertexInputStateCreateInfo vertexInputState = new()
		                                                      {
			                                                      SType                           = StructureType.PipelineVertexInputStateCreateInfo,
			                                                      VertexBindingDescriptionCount   = (uint)options.VertexInputState.VertexBindingDescriptions.Length,
			                                                      PVertexBindingDescriptions      = vertexInputBindingDescriptions,
			                                                      VertexAttributeDescriptionCount = (uint)options.VertexInputState.VertexAttributeDescriptions.Length,
			                                                      PVertexAttributeDescriptions    = vertexInputAttributeDescriptions,
		                                                      };

		PipelineInputAssemblyStateCreateInfo inputAssemblyState = new()
		                                                          {
			                                                          SType                  = StructureType.PipelineInputAssemblyStateCreateInfo,
			                                                          Topology               = options.InputAssemblyState.Topology.ToVulkan(),
			                                                          PrimitiveRestartEnable = options.InputAssemblyState.PrimitiveRestartEnable,
		                                                          };
		
		PipelineTessellationStateCreateInfo tessellationState = new()
		                                                        {
			                                                        SType              = StructureType.PipelineTessellationStateCreateInfo,
			                                                        PatchControlPoints = options.TessellationState.PatchControlPoints,
		                                                        };

		VkViewport* viewports = stackalloc VkViewport[options.ViewportState.Viewports.Length];
		for (int i = 0; i < options.ViewportState.Viewports.Length; ++i)
		{
			viewports[i] = new VkViewport
			               {
				               X        = options.ViewportState.Viewports[i].X,
				               Y        = options.ViewportState.Viewports[i].Y,
				               Width    = options.ViewportState.Viewports[i].Width,
				               Height   = options.ViewportState.Viewports[i].Height,
				               MinDepth = options.ViewportState.Viewports[i].MinDepth,
				               MaxDepth = options.ViewportState.Viewports[i].MaxDepth,
			               };
		}

		VkScissor* scissors = stackalloc VkScissor[options.ViewportState.Scissors.Length];
		for (int i = 0; i < options.ViewportState.Scissors.Length; ++i)
		{
			scissors[i] = new VkScissor
			              {
				              Offset = new Offset2D(options.ViewportState.Scissors[i].OffsetX, options.ViewportState.Scissors[i].OffsetY),
				              Extent = new Extent2D(options.ViewportState.Scissors[i].Width, options.ViewportState.Scissors[i].Height),
			              };
		}

		PipelineViewportStateCreateInfo viewportState = new()
		                                                {
			                                                SType         = StructureType.PipelineViewportStateCreateInfo,
			                                                ViewportCount = (uint)options.ViewportState.Viewports.Length,
			                                                PViewports    = viewports,
			                                                ScissorCount  = (uint)options.ViewportState.Scissors.Length,
			                                                PScissors     = scissors,
		                                                };

		PipelineRasterizationStateCreateInfo rasterizationState = new()
		                                                          {
			                                                          SType                   = StructureType.PipelineRasterizationStateCreateInfo,
			                                                          DepthClampEnable        = options.RasterizationState.DepthClampEnable,
			                                                          RasterizerDiscardEnable = options.RasterizationState.RasterizerDiscardEnable,
			                                                          PolygonMode             = options.RasterizationState.PolygonMode.ToVulkan(),
			                                                          CullMode                = options.RasterizationState.CullMode.ToVulkan(),
			                                                          FrontFace               = options.RasterizationState.FrontFace.ToVulkan(),
			                                                          DepthBiasEnable         = options.RasterizationState.DepthBiasEnable,
			                                                          DepthBiasConstantFactor = options.RasterizationState.DepthBiasConstantFactor,
			                                                          DepthBiasClamp          = options.RasterizationState.DepthBiasClamp,
			                                                          DepthBiasSlopeFactor    = options.RasterizationState.DepthBiasSlopeFactor,
			                                                          LineWidth               = options.RasterizationState.LineWidth,
		                                                          };

		PipelineMultisampleStateCreateInfo multisampleState = new()
		                                                      {
			                                                      SType                = StructureType.PipelineMultisampleStateCreateInfo,
			                                                      RasterizationSamples = options.MultisampleState.SampleCount.ToVulkan(),
			                                                      SampleShadingEnable  = options.MultisampleState.SampleShadingEnable,
			                                                      MinSampleShading     = options.MultisampleState.MinSampleShading,
			                                                      //PSampleMask = ???, // not supported yet
			                                                      AlphaToCoverageEnable = options.MultisampleState.AlphaToCoverageEnable,
			                                                      AlphaToOneEnable      = options.MultisampleState.AlphaToOneEnable,
		                                                      };

		PipelineDepthStencilStateCreateInfo depthStencilState = new()
		                                                        {
			                                                        SType = StructureType.PipelineDepthStencilStateCreateInfo,
			                                                        Flags = options.DepthStencilState.DepthStencilStateCreateFlags.ToVulkan(),
			                                                        DepthTestEnable = options.DepthStencilState.DepthTestEnable,
			                                                        DepthWriteEnable = options.DepthStencilState.DepthWriteEnable,
			                                                        DepthCompareOp = options.DepthStencilState.DepthCompareOp.ToVulkan(),
			                                                        DepthBoundsTestEnable = options.DepthStencilState.DepthBoundsTestEnable,
			                                                        StencilTestEnable = options.DepthStencilState.StencilTestEnable,
			                                                        Front = new Silk.NET.Vulkan.StencilOpState{
				                                                                                                  FailOp = options.DepthStencilState.Front.FailOp.ToVulkan(),
				                                                                                                  PassOp = options.DepthStencilState.Front.PassOp.ToVulkan(),
				                                                                                                  DepthFailOp = options.DepthStencilState.Front.DepthFailOp.ToVulkan(),
				                                                                                                  CompareOp = options.DepthStencilState.Front.CompareOp.ToVulkan(),
				                                                                                                  CompareMask = options.DepthStencilState.Front.CompareMask,
				                                                                                                  WriteMask = options.DepthStencilState.Front.WriteMask,
				                                                                                                  Reference = options.DepthStencilState.Front.Reference,
			                                                                                                  },
			                                                        Back = new Silk.NET.Vulkan.StencilOpState{
				                                                                                                 FailOp = options.DepthStencilState.Back.FailOp.ToVulkan(),
				                                                                                                 PassOp = options.DepthStencilState.Back.PassOp.ToVulkan(),
				                                                                                                 DepthFailOp = options.DepthStencilState.Back.DepthFailOp.ToVulkan(),
				                                                                                                 CompareOp = options.DepthStencilState.Back.CompareOp.ToVulkan(),
				                                                                                                 CompareMask = options.DepthStencilState.Back.CompareMask,
				                                                                                                 WriteMask = options.DepthStencilState.Back.WriteMask,
				                                                                                                 Reference = options.DepthStencilState.Back.Reference,
			                                                                                                 },
			                                                        MinDepthBounds = options.DepthStencilState.MinDepthBounds,
			                                                        MaxDepthBounds = options.DepthStencilState.MaxDepthBounds,
		                                                        };

		VkPipelineColorBlendAttachmentState* attachments = stackalloc VkPipelineColorBlendAttachmentState[options.ColorBlendState.Attachments.Length];
		for (int i = 0; i < options.ColorBlendState.Attachments.Length; ++i)
		{
			attachments[i] = new VkPipelineColorBlendAttachmentState
			                 {
				                 BlendEnable         = options.ColorBlendState.Attachments[i].BlendEnable,
				                 SrcColorBlendFactor = options.ColorBlendState.Attachments[i].SrcColorBlendFactor.ToVulkan(),
				                 DstColorBlendFactor = options.ColorBlendState.Attachments[i].DstColorBlendFactor.ToVulkan(),
				                 ColorBlendOp        = options.ColorBlendState.Attachments[i].ColorBlendOp.ToVulkan(),
				                 SrcAlphaBlendFactor = options.ColorBlendState.Attachments[i].SrcAlphaBlendFactor.ToVulkan(),
				                 DstAlphaBlendFactor = options.ColorBlendState.Attachments[i].DstAlphaBlendFactor.ToVulkan(),
				                 AlphaBlendOp        = options.ColorBlendState.Attachments[i].AlphaBlendOp.ToVulkan(),
				                 ColorWriteMask      = options.ColorBlendState.Attachments[i].ColorWriteMask.ToVulkan(),
			                 };
		}

		PipelineColorBlendStateCreateInfo colorBlendState = new()
		                                                    {
			                                                    SType           = StructureType.PipelineColorBlendStateCreateInfo,
			                                                    Flags           = options.ColorBlendState.Flags.ToVulkan(),
			                                                    LogicOpEnable   = options.ColorBlendState.LogicOpEnable,
			                                                    LogicOp         = options.ColorBlendState.LogicOp.ToVulkan(),
			                                                    AttachmentCount = (uint)options.ColorBlendState.Attachments.Length,
			                                                    PAttachments    = attachments,
		                                                    };
		colorBlendState.BlendConstants[0] = options.ColorBlendState.BlendConstants[0];
		colorBlendState.BlendConstants[1] = options.ColorBlendState.BlendConstants[1];
		colorBlendState.BlendConstants[2] = options.ColorBlendState.BlendConstants[2];
		colorBlendState.BlendConstants[3] = options.ColorBlendState.BlendConstants[3];

		PipelineDynamicStateCreateInfo* dynamicStatePtr = null;
		if (options.DynamicState.DynamicStates is not null)
		{
			VkDynamicState* dynamicStates = stackalloc VkDynamicState[options.DynamicState.DynamicStates.Length];
			for (int i = 0; i < options.DynamicState.DynamicStates.Length; ++i)
			{
				dynamicStates[i] = options.DynamicState.DynamicStates[i].ToVulkan();
			}

			PipelineDynamicStateCreateInfo dynamicState = new()
			                                              {
				                                              SType             = StructureType.PipelineDynamicStateCreateInfo,
				                                              DynamicStateCount = (uint) options.DynamicState.DynamicStates.Length,
				                                              PDynamicStates    = dynamicStates,
			                                              };

			dynamicStatePtr = &dynamicState;
		}

		GraphicsPipelineCreateInfo pipelineInfo = new()
		                                          {
			                                          SType      = StructureType.GraphicsPipelineCreateInfo,
				                                          
			                                          Flags = options.Flags.ToVulkan(),
				                                          
			                                          StageCount = (uint) options.Shaders.Length,
			                                          PStages    = stages,
				                                          
			                                          PVertexInputState   = &vertexInputState,
			                                          PInputAssemblyState = &inputAssemblyState,
			                                          PTessellationState  = &tessellationState,
			                                          PViewportState      = &viewportState,
			                                          PRasterizationState = &rasterizationState,
			                                          PMultisampleState   = &multisampleState,
			                                          PDepthStencilState  = &depthStencilState,
			                                          PColorBlendState    = &colorBlendState,
			                                          PDynamicState       = dynamicStatePtr,
				                                          
			                                          Layout     = ((VulkanPipelineLayout)options.Layout).PipelineLayout,
			                                          RenderPass = ((VulkanRenderPass)options.RenderPass).RenderPass,
			                                          Subpass    = options.Subpass,
				                                          
			                                          // BasePipelineHandle = ??? not supported yet
			                                          // BasePipelineIndex = ??? not supported yet
		                                          };

		Result result = _api.Vk.CreateGraphicsPipelines(_logicalDevice.Device, default, 1, pipelineInfo, null, out Pipeline);
		if (result != Result.Success)
		{
			throw new GfxException($"Failed to create graphics pipeline! Result:{result}");
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyPipeline(_logicalDevice.Device, Pipeline, null);
	}
}