namespace Gfx;

public struct LogicalDeviceOptions
{
	public PhysicalDevice PhysicalDevice;
	public ImageFormat    FrameBufferFormat;
	public bool           NeedDepthStencil;
	public int            MaxFramesInFlight;
	public SampleCount    MsaaSampleCount;

	public LogicalDeviceOptions(PhysicalDevice physicalDevice, ImageFormat frameBufferFormat, bool needDepthStencil, int maxFramesInFlight = 2, SampleCount msaaSampleCount = SampleCount.Count1)
	{
		PhysicalDevice    = physicalDevice;
		FrameBufferFormat = frameBufferFormat;
		NeedDepthStencil  = needDepthStencil;
		MaxFramesInFlight = maxFramesInFlight;
		MsaaSampleCount   = msaaSampleCount;
	}
}