namespace Gfx;

public struct PipelineVertexInputStateOptions
{
	public VertexInputBindingDescription[]   VertexBindingDescriptions;
	public VertexInputAttributeDescription[] VertexAttributeDescriptions;

	public PipelineVertexInputStateOptions(VertexInputBindingDescription[] vertexBindingDescriptions, VertexInputAttributeDescription[] vertexAttributeDescriptions)
	{
		VertexBindingDescriptions   = vertexBindingDescriptions;
		VertexAttributeDescriptions = vertexAttributeDescriptions;
	}
}