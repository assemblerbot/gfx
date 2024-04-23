namespace Gfx;

public abstract class CommandBuffer : IDisposable
{
	public abstract void Dispose();

	public abstract void Begin(CommandBufferUsage usage);
	public abstract void End();
}