namespace Gfx;

public abstract class PhysicalDevice
{
	public abstract PhysicalDeviceKind Kind { get; }
	public abstract string             Name { get; }
}