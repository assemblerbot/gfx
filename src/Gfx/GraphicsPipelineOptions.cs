namespace Gfx;

public struct GraphicsPipelineOptions
{
	public PipelineCreateFlags               Flags;
	public Shader[]                          Shaders;
	public PipelineVertexInputStateOptions   VertexInputState;
	public PipelineInputAssemblyStateOptions InputAssemblyState;
	public PipelineTessellationStateOptions  TessellationState;
	public PipelineViewportStateOptions      ViewportState;
	public PipelineRasterizationStateOptions RasterizationState;
	public PipelineMultisampleStateOptions   MultisampleState;
	public PipelineDepthStencilStateOptions  DepthStencilState;
	public PipelineColorBlendStateOptions    ColorBlendState;
	public PipelineDynamicStateOptions       DynamicState;
	public PipelineLayout                    Layout;
	public RenderPass                        RenderPass;
	public uint                              Subpass;
	
	// public Pipeline                          BasePipeline; // TODO - what is this?
	// public int                               BasePipelineIndex;

	public GraphicsPipelineOptions(PipelineCreateFlags flags,
		Shader[]                                       shaders,
		PipelineVertexInputStateOptions                vertexInputState,
		PipelineInputAssemblyStateOptions              inputAssemblyState,
		PipelineTessellationStateOptions               tessellationState,
		PipelineViewportStateOptions                   viewportState,
		PipelineRasterizationStateOptions              rasterizationState,
		PipelineMultisampleStateOptions                multisampleState,
		PipelineDepthStencilStateOptions               depthStencilState,
		PipelineColorBlendStateOptions                 colorBlendState,
		PipelineDynamicStateOptions                    dynamicState,
		PipelineLayout                                 layout,
		RenderPass                                     renderPass,
		uint                                           subpass)
	{
		Flags              = flags;
		Shaders            = shaders;
		VertexInputState   = vertexInputState;
		InputAssemblyState = inputAssemblyState;
		TessellationState  = tessellationState;
		ViewportState      = viewportState;
		RasterizationState = rasterizationState;
		MultisampleState   = multisampleState;
		DepthStencilState  = depthStencilState;
		ColorBlendState    = colorBlendState;
		DynamicState       = dynamicState;
		Layout             = layout;
		RenderPass         = renderPass;
		Subpass            = subpass;
	}
}