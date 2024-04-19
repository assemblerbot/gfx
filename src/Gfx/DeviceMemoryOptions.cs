namespace Gfx;

public struct DeviceMemoryOptions
{
	public int              SizeInBytes;
	public DeviceMemoryKind Kind;

	public DeviceMemoryOptions(int sizeInBytes, DeviceMemoryKind kind)
	{
		SizeInBytes = sizeInBytes;
		Kind        = kind;
	}
}