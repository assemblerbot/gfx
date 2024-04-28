namespace Gfx;

[Flags]
public enum PipelineShaderStageCreateFlags
{
	None                        = 0,
	AllowVaryingSubgroupSizeBit = 0x00000001,
	RequireFullSubgroupsBit     = 0x00000002,
}