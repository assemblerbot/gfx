namespace Gfx;

[Flags]
public enum CommandBufferUsage
{
	None               = 0,
	OneTimeSubmit      = 1 << 0,
	RenderPassContinue = 1 << 1,
	SimultaneousUse    = 1 << 2,
}