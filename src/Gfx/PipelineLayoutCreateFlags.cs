namespace Gfx;

[Flags]
public enum PipelineLayoutCreateFlags
{
	None = 0,
	IndependentSets = 1 << 0,
}