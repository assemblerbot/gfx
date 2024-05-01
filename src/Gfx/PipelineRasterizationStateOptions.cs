namespace Gfx;

public struct PipelineRasterizationStateOptions
{
	public bool        DepthClampEnable;
	public bool        RasterizerDiscardEnable;
	public PolygonMode PolygonMode;
	public CullMode    CullMode;
	public FrontFace   FrontFace;
	public bool        DepthBiasEnable;
	public float       DepthBiasConstantFactor;
	public float       DepthBiasClamp;
	public float       DepthBiasSlopeFactor;
	public float       LineWidth;

	public PipelineRasterizationStateOptions(bool depthClampEnable, bool rasterizerDiscardEnable, PolygonMode polygonMode, CullMode cullMode, FrontFace frontFace, bool depthBiasEnable, float depthBiasConstantFactor, float depthBiasClamp, float depthBiasSlopeFactor, float lineWidth)
	{
		DepthClampEnable        = depthClampEnable;
		RasterizerDiscardEnable = rasterizerDiscardEnable;
		PolygonMode             = polygonMode;
		CullMode                = cullMode;
		FrontFace               = frontFace;
		DepthBiasEnable         = depthBiasEnable;
		DepthBiasConstantFactor = depthBiasConstantFactor;
		DepthBiasClamp          = depthBiasClamp;
		DepthBiasSlopeFactor    = depthBiasSlopeFactor;
		LineWidth               = lineWidth;
	}
}