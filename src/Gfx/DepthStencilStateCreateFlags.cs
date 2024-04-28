namespace Gfx;

public enum DepthStencilStateCreateFlags
{
	None          = 0,
	DepthAccess   = 1 << 0,
	StencilAccess = 1 << 1,
}