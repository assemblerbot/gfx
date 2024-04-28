namespace Gfx;

public struct PushConstantRange
{
	public ShaderStage Stage;
	public uint        Offset;
	public uint        Size;

	public PushConstantRange(ShaderStage stage, uint offset, uint size)
	{
		Stage  = stage;
		Offset = offset;
		Size   = size;
	}
}