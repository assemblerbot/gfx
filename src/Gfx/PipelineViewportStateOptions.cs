namespace Gfx;

public struct PipelineViewportStateOptions
{
	public uint       Flags;
	public Viewport[] Viewports;
	public Scrissor[] Scissors;

	public PipelineViewportStateOptions(uint flags, Viewport[] viewports, Scrissor[] scissors)
	{
		Flags     = flags;
		Viewports = viewports;
		Scissors  = scissors;
	}
}