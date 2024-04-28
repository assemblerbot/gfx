namespace Gfx;

public struct PipelineDynamicStateOptions
{
	public uint           Flags;
	public DynamicState[] DynamicStates;

	public PipelineDynamicStateOptions(uint flags, DynamicState[] dynamicStates)
	{
		Flags         = flags;
		DynamicStates = dynamicStates;
	}
}