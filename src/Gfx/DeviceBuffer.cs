namespace Gfx;

public abstract class DeviceBuffer : IDisposable
{
	public readonly ulong SizeInBytes;

	public abstract void Dispose();

	public abstract bool GetMemoryRequirements(DeviceMemoryProperties requiredProperties,   out uint  memoryIndex, out ulong alignment, out ulong size);
	public abstract bool GetMemoryRequirements(uint                   suggestedMemoryIndex, out ulong alignment,   out ulong size);

	public abstract void BindToMemory(DeviceMemory memory, ulong memoryOffset);
}