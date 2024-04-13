namespace Gfx;

public struct SwapChainOptions
{
	public ImageFormat    FrameBufferFormat;
	public bool           NeedDepthStencil;
	public int            MaxFramesInFlight;
	public SampleCount    MsaaSampleCount;

	public SwapChainOptions(ImageFormat frameBufferFormat, bool needDepthStencil, int maxFramesInFlight = 2, SampleCount msaaSampleCount = SampleCount.Count1)
	{
		FrameBufferFormat = frameBufferFormat;
		NeedDepthStencil  = needDepthStencil;
		MaxFramesInFlight = maxFramesInFlight;
		MsaaSampleCount   = msaaSampleCount;
	}
}