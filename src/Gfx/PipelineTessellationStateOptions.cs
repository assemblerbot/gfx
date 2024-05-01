namespace Gfx;

public struct PipelineTessellationStateOptions
{
	public uint PatchControlPoints;

	public PipelineTessellationStateOptions(uint patchControlPoints)
	{
		PatchControlPoints = patchControlPoints;
	}
}