namespace Gfx;

public struct LogicalDeviceOptions
{
	public PhysicalDevice PhysicalDevice;

	public LogicalDeviceOptions(PhysicalDevice physicalDevice)
	{
		PhysicalDevice = physicalDevice;
	}
}