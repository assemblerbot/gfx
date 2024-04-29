namespace Gfx;

public readonly struct DescriptorSetLayoutBinding
{
	public readonly uint           Binding;
	public readonly DescriptorKind DescriptorKind;
	public readonly uint           DescriptorCount;
	public readonly ShaderStage    ShaderStage;
	public readonly Sampler[]      Samplers;
}