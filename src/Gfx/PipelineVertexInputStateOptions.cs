namespace Gfx;

public struct PipelineVertexInputStateOptions
{
	public uint                              Flags; // TODO - enum, by documentation
	public VertexInputBindingDescription[]   VertexBindingDescriptions;
	public VertexInputAttributeDescription[] VertexAttributeDescriptions;

	public PipelineVertexInputStateOptions(uint flags, VertexInputBindingDescription[] vertexBindingDescriptions, VertexInputAttributeDescription[] vertexAttributeDescriptions)
	{
		Flags                       = flags;
		VertexBindingDescriptions   = vertexBindingDescriptions;
		VertexAttributeDescriptions = vertexAttributeDescriptions;
	}
}