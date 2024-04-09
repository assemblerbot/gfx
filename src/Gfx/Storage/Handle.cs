namespace Gfx;

// ReSharper disable once UnusedTypeParameter
public readonly struct Handle<T>(int index, int generation)
{
    public readonly int Index = index;
    public readonly int Generation = generation;
}