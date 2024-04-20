namespace Gfx;

public abstract class DeviceMemory : IDisposable
{
	public readonly int SizeInBytes;

	public abstract void Dispose();
}