namespace Gfx;

public abstract class SwapChain : IDisposable
{
	public abstract uint Width          { get; }
	public abstract uint Height         { get; }
	public abstract int  FramesInFlight { get; }

	public abstract void Dispose();

	public abstract void WaitForFence(int swapChainBufferIndex, ulong timeout = ulong.MaxValue);
	public abstract void ResetFence(int   swapChainBufferIndex);
	
	public abstract GfxResult AcquireNextImage(int swapChainBufferIndex, ref uint imageIndex, ulong timeout = ulong.MaxValue);

	public abstract void Submit(int swapChainBufferIndex, CommandBuffer commandBuffer);

	public abstract void Present(int swapChainBufferIndex, uint imageIndex);
}