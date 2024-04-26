namespace Gfx;

public struct ShaderOptions
{
	public byte[]      Code;	// SPIR-V byte code
	public ShaderStage Stage;
	public string      MainFunction;

	public ShaderOptions(byte[] code, ShaderStage stage, string mainFunction)
	{
		Code         = code;
		Stage        = stage;
		MainFunction = mainFunction;
	}
}