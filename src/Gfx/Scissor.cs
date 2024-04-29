namespace Gfx;

public readonly struct Scissor
{
	public readonly int  OffsetX;
	public readonly int  OffsetY;
	public readonly uint Width;
	public readonly uint Height;

	public Scissor(int offsetX, int offsetY, uint width, uint height)
	{
		OffsetX = offsetX;
		OffsetY = offsetY;
		Width   = width;
		Height  = height;
	}
}