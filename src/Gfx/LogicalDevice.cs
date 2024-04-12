namespace Gfx;

public abstract class LogicalDevice : IDisposable
{
	public abstract void Dispose();

	public abstract void       WaitIdle();
	public abstract RenderPass CreateRenderPass(RenderPassOptions       options);
}