namespace Gfx;

public struct PipelineMultisampleStateOptions
{
	public uint        Flags;
	public SampleCount SampleCount;
	public bool        SampleShadingEnable;
	public float       MinSampleShading;
	public uint[]      SampleMask;
	public bool        AlphaToCoverageEnable;
	public bool        AlphaToOneEnable;

	public PipelineMultisampleStateOptions(uint flags, SampleCount sampleCount, bool sampleShadingEnable, float minSampleShading, uint[] sampleMask, bool alphaToCoverageEnable, bool alphaToOneEnable)
	{
		Flags                 = flags;
		SampleCount           = sampleCount;
		SampleShadingEnable   = sampleShadingEnable;
		MinSampleShading      = minSampleShading;
		SampleMask            = sampleMask;
		AlphaToCoverageEnable = alphaToCoverageEnable;
		AlphaToOneEnable      = alphaToOneEnable;
	}
}