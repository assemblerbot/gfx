namespace Gfx;

public struct StencilOpState
{
	public StencilOp FailOp;
	public StencilOp PassOp;
	public StencilOp DepthFailOp;
	public CompareOp CompareOp;
	public uint      CompareMask;
	public uint      Reference;

	public StencilOpState(StencilOp failOp, StencilOp passOp, StencilOp depthFailOp, CompareOp compareOp, uint compareMask, uint reference)
	{
		FailOp      = failOp;
		PassOp      = passOp;
		DepthFailOp = depthFailOp;
		CompareOp   = compareOp;
		CompareMask = compareMask;
		Reference   = reference;
	}
}