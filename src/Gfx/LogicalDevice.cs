namespace Gfx;

public abstract class LogicalDevice : IDisposable
{
	public abstract void Dispose();

	public abstract void      WaitIdle();
	public abstract SwapChain CreateSwapChain(SwapChainOptions options);
}