namespace Gfx;

[Flags]
public enum CullMode
{
	None = 0,
	Front,
	Back,
	FrontAndBack = Front | Back,
}