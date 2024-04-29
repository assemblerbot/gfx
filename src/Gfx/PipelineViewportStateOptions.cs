namespace Gfx;

public struct PipelineViewportStateOptions
{
	public uint       Flags;
	public Viewport[] Viewports;
	public Scissor[] Scissors;

	public PipelineViewportStateOptions(uint flags, Viewport[] viewports, Scissor[] scissors)
	{
		Flags     = flags;
		Viewports = viewports;
		Scissors  = scissors;
	}
}