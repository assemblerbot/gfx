namespace Gfx;

public struct PipelineMultisampleStateOptions
{
	public SampleCount SampleCount;
	public bool        SampleShadingEnable;
	public float       MinSampleShading;
	public uint[]      SampleMask;
	public bool        AlphaToCoverageEnable;
	public bool        AlphaToOneEnable;

	public PipelineMultisampleStateOptions(SampleCount sampleCount, bool sampleShadingEnable, float minSampleShading, uint[] sampleMask, bool alphaToCoverageEnable, bool alphaToOneEnable)
	{
		SampleCount           = sampleCount;
		SampleShadingEnable   = sampleShadingEnable;
		MinSampleShading      = minSampleShading;
		SampleMask            = sampleMask;
		AlphaToCoverageEnable = alphaToCoverageEnable;
		AlphaToOneEnable      = alphaToOneEnable;
	}
}