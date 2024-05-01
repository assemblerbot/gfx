namespace Gfx;

public struct PipelineDynamicStateOptions
{
	public DynamicState[] DynamicStates;

	public PipelineDynamicStateOptions(DynamicState[] dynamicStates)
	{
		DynamicStates = dynamicStates;
	}
}