namespace Gfx;

public struct PipelineDynamicStateOptions
{
	public readonly DynamicState[]? DynamicStates;

	public PipelineDynamicStateOptions(DynamicState[] dynamicStates)
	{
		DynamicStates = dynamicStates;
	}
}