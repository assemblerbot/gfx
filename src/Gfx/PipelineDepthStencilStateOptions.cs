namespace Gfx;

public struct PipelineDepthStencilStateOptions
{
	public DepthStencilStateCreateFlags DepthStencilStateCreateFlags;
	public bool                         DepthTestEnable;
	public bool                         DepthWriteEnable;
	public CompareOp                    DepthCompareOp;
	public bool                         DepthBoundsTestEnable;
	public float                        MinDepthBounds;
	public float                        MaxDepthBounds;

	public bool           StencilTestEnable;
	public StencilOpState Front;
	public StencilOpState Back;

	public PipelineDepthStencilStateOptions(DepthStencilStateCreateFlags depthStencilStateCreateFlags, bool depthTestEnable, bool depthWriteEnable, CompareOp depthCompareOp, bool depthBoundsTestEnable, float minDepthBounds, float maxDepthBounds, bool stencilTestEnable, StencilOpState front, StencilOpState back)
	{
		DepthStencilStateCreateFlags = depthStencilStateCreateFlags;
		DepthTestEnable              = depthTestEnable;
		DepthWriteEnable             = depthWriteEnable;
		DepthCompareOp               = depthCompareOp;
		DepthBoundsTestEnable        = depthBoundsTestEnable;
		MinDepthBounds               = minDepthBounds;
		MaxDepthBounds               = maxDepthBounds;
		StencilTestEnable            = stencilTestEnable;
		Front                        = front;
		Back                         = back;
	}
}