namespace Gfx;

public struct PipelineInputAssemblyStateOptions
{
	public uint              Flags; // TODO - enum
	public PrimitiveTopology Topology;
	public bool              PrimitiveRestartEnable;

	public PipelineInputAssemblyStateOptions(uint flags, PrimitiveTopology topology, bool primitiveRestartEnable)
	{
		Flags                  = flags;
		Topology               = topology;
		PrimitiveRestartEnable = primitiveRestartEnable;
	}
}