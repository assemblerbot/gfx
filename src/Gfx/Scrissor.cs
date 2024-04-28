namespace Gfx;

public readonly struct Scrissor
{
	public readonly int  OffsetX;
	public readonly int  OffsetY;
	public readonly uint Width;
	public readonly uint Height;

	public Scrissor(int offsetX, int offsetY, uint width, uint height)
	{
		OffsetX = offsetX;
		OffsetY = offsetY;
		Width   = width;
		Height  = height;
	}
}