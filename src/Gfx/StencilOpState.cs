namespace Gfx;

public struct StencilOpState
{
	public StencilOp FailOp;
	public StencilOp PassOp;
	public StencilOp DepthFailOp;
	public CompareOp CompareOp;
	public uint      CompareMask;
	public uint      WriteMask;
	public uint      Reference;

	public StencilOpState(StencilOp failOp, StencilOp passOp, StencilOp depthFailOp, CompareOp compareOp, uint compareMask, uint writeMask, uint reference)
	{
		FailOp      = failOp;
		PassOp      = passOp;
		DepthFailOp = depthFailOp;
		CompareOp   = compareOp;
		CompareMask = compareMask;
		WriteMask   = writeMask;
		Reference   = reference;
	}
}