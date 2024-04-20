namespace Gfx;

[Flags]
public enum ShaderStage
{
	None                   = 0,
	Vertex                 = 1 << 0,
	TessellationControl    = 1 << 1,
	TessellationEvaluation = 1 << 2,
	Geometry               = 1 << 3,
	Fragment               = 1 << 4,
	Compute                = 1 << 5,
	AllGraphics            = Fragment | Geometry | TessellationEvaluation | TessellationControl | Vertex,
	All                    = 0x7FFFFFFF,
}