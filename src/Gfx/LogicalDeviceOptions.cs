namespace Gfx;

public struct LogicalDeviceOptions
{
	public PhysicalDevice PhysicalDevice;
	public ImageFormat    FrameBufferFormat;

	public LogicalDeviceOptions(PhysicalDevice physicalDevice, ImageFormat frameBufferFormat)
	{
		PhysicalDevice    = physicalDevice;
		FrameBufferFormat = frameBufferFormat;
	}
}