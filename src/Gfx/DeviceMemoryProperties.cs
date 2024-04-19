namespace Gfx;

public class DeviceMemoryProperties
{
	public readonly struct MemoryInfo
	{
		public readonly DeviceMemoryKind Kind;
		public readonly int              HeapIndex;

		public MemoryInfo(DeviceMemoryKind kind, int heapIndex)
		{
			Kind      = kind;
			HeapIndex = heapIndex;
		}
	}

	public readonly struct HeapInfo
	{
		public readonly DeviceHeapKind Kind;
		public readonly ulong          Size;

		public HeapInfo(DeviceHeapKind kind, ulong size)
		{
			Kind = kind;
			Size = size;
		}
	}

	public readonly IReadOnlyList<MemoryInfo> Memory;
	public readonly IReadOnlyList<HeapInfo>   Heap;

	public DeviceMemoryProperties(IReadOnlyList<MemoryInfo> memory, IReadOnlyList<HeapInfo> heap)
	{
		Memory = memory;
		Heap   = heap;
	}
}