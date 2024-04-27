namespace Gfx;

public struct SwapChainOptions
{
	public DeviceFormat FrameBufferDeviceFormat;
	public bool         NeedDepthStencil;
	public int          MaxFramesInFlight;
	public SampleCount  MsaaSampleCount;

	public SwapChainOptions(DeviceFormat frameBufferDeviceFormat, bool needDepthStencil, int maxFramesInFlight = 2, SampleCount msaaSampleCount = SampleCount.Count1)
	{
		FrameBufferDeviceFormat = frameBufferDeviceFormat;
		NeedDepthStencil  = needDepthStencil;
		MaxFramesInFlight = maxFramesInFlight;
		MsaaSampleCount   = msaaSampleCount;
	}
}