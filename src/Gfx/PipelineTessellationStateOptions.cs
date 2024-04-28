namespace Gfx;

public struct PipelineTessellationStateOptions
{
	public uint Flags;
	public uint PatchControlPoints;

	public PipelineTessellationStateOptions(uint flags, uint patchControlPoints)
	{
		Flags              = flags;
		PatchControlPoints = patchControlPoints;
	}
}