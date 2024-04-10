using System.Runtime.CompilerServices;

namespace Gfx;

public class DataStorage<T> : IDisposable
{
    private readonly T[] _data;
    private readonly int[] _generations;
    private readonly Stack<int> _freeIndices;
    
    private readonly int _capacity;

    public DataStorage(int capacity)
    {
        _capacity = capacity;
        _data = new T[capacity];
        _generations = new int[capacity];
        _freeIndices = new Stack<int>(capacity);

        capacity--;
        while (capacity >= 0)
        {
            _freeIndices.Push(capacity);
            capacity--;
        }
    }

    public void Dispose()
    {
        _freeIndices.Clear();
    }

    public T? ByHandle(Handle<T> handle)
    {
        return IsValid(handle) ? _data[handle.Index] : default;
    }

    public Handle<T> Store(ref T data)
    {
        int index = _freeIndices.Pop();
        _data[index] = data;
        int generation = _generations[index];

        return new Handle<T>(index, generation);
    }

    public void Free(Handle<T> handle)
    {
        if (!IsValid(handle))
        {
            return;
        }

        _generations[handle.Index] = handle.Generation + 1;
        _freeIndices.Push(handle.Index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsValid(Handle<T> handle)
    {
        if (handle.Index < 0 || handle.Index >= _capacity)
        {
            return false;
        }

        return _generations[handle.Index] == handle.Generation;
    }
}