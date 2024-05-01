namespace Gfx;

public struct PipelineInputAssemblyStateOptions
{
	public PrimitiveTopology Topology;
	public bool              PrimitiveRestartEnable;

	public PipelineInputAssemblyStateOptions(PrimitiveTopology topology, bool primitiveRestartEnable)
	{
		Topology               = topology;
		PrimitiveRestartEnable = primitiveRestartEnable;
	}
}