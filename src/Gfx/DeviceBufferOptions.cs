namespace Gfx;

public struct DeviceBufferOptions
{
	public ulong             Size;
	public DeviceBufferUsage Usage;
	public SharingMode       SharingMode;

	public DeviceBufferOptions(ulong size, DeviceBufferUsage usage, SharingMode sharingMode)
	{
		Size        = size;
		Usage       = usage;
		SharingMode = sharingMode;
	}
}