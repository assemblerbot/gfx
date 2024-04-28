namespace Gfx;

public struct PipelineLayoutOptions
{
	public PipelineLayoutCreateFlags Flags;
	public DescriptorSetLayout[]     SetLayouts;
	public PushConstantRange[]       PushConstantRanges;

	public PipelineLayoutOptions(PipelineLayoutCreateFlags flags, DescriptorSetLayout[] setLayouts, PushConstantRange[] pushConstantRanges)
	{
		Flags              = flags;
		SetLayouts         = setLayouts;
		PushConstantRanges = pushConstantRanges;
	}
}