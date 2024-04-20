namespace Gfx;

public struct DeviceMemoryOptions
{
	public ulong Size;
	public uint MemoryTypeIndex;

	public DeviceMemoryOptions(ulong size, uint memoryTypeIndex)
	{
		Size     = size;
		MemoryTypeIndex = memoryTypeIndex;
	}
}