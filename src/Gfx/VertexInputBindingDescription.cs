namespace Gfx;

public readonly struct VertexInputBindingDescription
{
	public readonly uint            Binding;
	public readonly uint            Stride;
	public readonly VertexInputRate InputRate;

	public VertexInputBindingDescription(uint binding, uint stride, VertexInputRate inputRate)
	{
		Binding   = binding;
		Stride    = stride;
		InputRate = inputRate;
	}
}