namespace Gfx;

public struct PipelineColorBlendAttachmentState
{
	public bool        BlendEnable;
	
	public BlendFactor SrcColorBlendFactor;
	public BlendFactor DstColorBlendFactor;
	public BlendOp     ColorBlendOp;

	public BlendFactor SrcAlphaBlendFactor;
	public BlendFactor DstAlphaBlendFactor;
	public BlendOp     AlphaBlendOp;

	public ColorComponent ColorWriteMask;

	public PipelineColorBlendAttachmentState(bool blendEnable, BlendFactor srcColorBlendFactor, BlendFactor dstColorBlendFactor, BlendOp colorBlendOp, BlendFactor srcAlphaBlendFactor, BlendFactor dstAlphaBlendFactor, BlendOp alphaBlendOp, ColorComponent colorWriteMask)
	{
		BlendEnable         = blendEnable;
		SrcColorBlendFactor = srcColorBlendFactor;
		DstColorBlendFactor = dstColorBlendFactor;
		ColorBlendOp        = colorBlendOp;
		SrcAlphaBlendFactor = srcAlphaBlendFactor;
		DstAlphaBlendFactor = dstAlphaBlendFactor;
		AlphaBlendOp        = alphaBlendOp;
		ColorWriteMask      = colorWriteMask;
	}
}