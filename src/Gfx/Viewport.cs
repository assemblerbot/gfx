namespace Gfx;

public readonly struct Viewport
{
	public readonly float X;
	public readonly float Y;
	public readonly float Width;
	public readonly float Height;
	public readonly float MinDepth;
	public readonly float MaxDepth;

	public Viewport(float x, float y, float width, float height, float minDepth, float maxDepth)
	{
		X        = x;
		Y        = y;
		Width    = width;
		Height   = height;
		MinDepth = minDepth;
		MaxDepth = maxDepth;
	}
}