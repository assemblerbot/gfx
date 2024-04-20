namespace Gfx;

public abstract class DeviceBuffer : IDisposable
{
	public readonly ulong SizeInBytes;

	public abstract void Dispose();
	public abstract bool GetMemoryRequirements(DeviceMemoryProperties requiredProperties, out uint memoryIndex, out ulong alignment, out ulong size);
}