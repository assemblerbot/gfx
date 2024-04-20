namespace Gfx;

public struct ShaderOptions
{
	public byte[]      Code;	// SPIR-V byte code
	public ShaderStage Stage;
	public string      MainFunction;
}