namespace Gfx;

public abstract class SwapChain : IDisposable
{
	public abstract uint Width  { get; }
	public abstract uint Height { get; }

	public abstract void Dispose();

	public abstract void      WaitForFence(int     swapChainBufferIndex, ulong    timeout                   = ulong.MaxValue);
	public abstract GfxResult AcquireNextImage(int swapChainBufferIndex, ref uint imageIndex, ulong timeout = ulong.MaxValue);
}