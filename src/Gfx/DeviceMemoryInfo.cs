namespace Gfx;

public class DeviceMemoryInfo
{
	public readonly struct MemoryInfo
	{
		public readonly DeviceMemoryProperties Properties;
		public readonly int              HeapIndex;

		public MemoryInfo(DeviceMemoryProperties properties, int heapIndex)
		{
			Properties      = properties;
			HeapIndex = heapIndex;
		}
	}

	public readonly struct HeapInfo
	{
		public readonly DeviceHeapProperties Properties;
		public readonly ulong          Size;

		public HeapInfo(DeviceHeapProperties properties, ulong size)
		{
			Properties = properties;
			Size = size;
		}
	}

	public readonly IReadOnlyList<MemoryInfo> Memory;
	public readonly IReadOnlyList<HeapInfo>   Heap;

	public DeviceMemoryInfo(IReadOnlyList<MemoryInfo> memory, IReadOnlyList<HeapInfo> heap)
	{
		Memory = memory;
		Heap   = heap;
	}
}