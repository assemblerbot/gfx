namespace Gfx;

public struct PipelineViewportStateOptions
{
	public Viewport[] Viewports;
	public Scissor[] Scissors;

	public PipelineViewportStateOptions(Viewport[] viewports, Scissor[] scissors)
	{
		Viewports = viewports;
		Scissors  = scissors;
	}
}