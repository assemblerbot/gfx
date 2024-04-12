namespace Gfx;

public struct LogicalDeviceOptions
{
	public PhysicalDevice PhysicalDevice;
	public ImageFormat    FrameBufferFormat;
	public int            MaxFramesInFlight;

	public LogicalDeviceOptions(PhysicalDevice physicalDevice, ImageFormat frameBufferFormat, int maxFramesInFlight = 2)
	{
		PhysicalDevice    = physicalDevice;
		FrameBufferFormat = frameBufferFormat;
		MaxFramesInFlight = maxFramesInFlight;
	}
}