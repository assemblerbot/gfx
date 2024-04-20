namespace Gfx;

[Flags]
public enum DeviceMemoryProperties
{
	None         = 0,
	DeviceLocal  = 1 << 0, // fast GPU access
	HostVisible  = 1 << 1, // accessible by CPU
	Cached       = 1 << 2, // fast CPU read access
	HostCoherent = 1 << 3, // no need to invalidate before write and flush after write
}