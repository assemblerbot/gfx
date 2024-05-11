namespace Gfx;

public struct SwapChainOptions
{
	public bool         NeedDepthStencil;
	public int          MaxFramesInFlight;
	public SampleCount  MsaaSampleCount;

	public SwapChainOptions(bool needDepthStencil, int maxFramesInFlight = 2, SampleCount msaaSampleCount = SampleCount.Count1)
	{
		NeedDepthStencil  = needDepthStencil;
		MaxFramesInFlight = maxFramesInFlight;
		MsaaSampleCount   = msaaSampleCount;
	}
}