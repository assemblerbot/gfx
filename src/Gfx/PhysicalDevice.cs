namespace Gfx;

public abstract class PhysicalDevice
{
	public abstract PhysicalDeviceKind Kind { get; }
	public abstract string             Name { get; }
	
	public abstract bool SupportsGraphics { get; }
	public abstract bool SupportsCompute  { get; }

	public abstract DeviceMemoryProperties GetMemoryProperties();
}