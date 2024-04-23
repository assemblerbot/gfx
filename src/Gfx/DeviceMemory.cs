namespace Gfx;

public abstract class DeviceMemory : IDisposable
{
	public readonly int SizeInBytes;

	public abstract void Dispose();

	public abstract void Write<T>(ulong offset, Span<T> data) where T : struct;
}