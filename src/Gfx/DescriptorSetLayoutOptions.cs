namespace Gfx;

public readonly struct DescriptorSetLayoutOptions
{
	public readonly DescriptorSetLayoutBinding[]? Bindings;

	public DescriptorSetLayoutOptions(DescriptorSetLayoutBinding[] bindings)
	{
		Bindings = bindings;
	}
}