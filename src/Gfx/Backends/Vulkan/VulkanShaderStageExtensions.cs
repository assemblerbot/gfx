using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanShaderStageExtensions
{
	public static ShaderStageFlags ToVulkan(this ShaderStage stage)
	{
		ShaderStageFlags flags = ShaderStageFlags.None;

		if ((stage & ShaderStage.Vertex) != 0)
		{
			flags |= ShaderStageFlags.VertexBit;
		}

		if ((stage & ShaderStage.TessellationControl) != 0)
		{
			flags |= ShaderStageFlags.TessellationControlBit;
		}
		
		if ((stage & ShaderStage.TessellationEvaluation) != 0)
		{
			flags |= ShaderStageFlags.TessellationEvaluationBit;
		}

		if ((stage & ShaderStage.Geometry) != 0)
		{
			flags |= ShaderStageFlags.GeometryBit;
		}

		if ((stage & ShaderStage.Fragment) != 0)
		{
			flags |= ShaderStageFlags.FragmentBit;
		}

		if ((stage & ShaderStage.Compute) != 0)
		{
			flags |= ShaderStageFlags.ComputeBit;
		}
		
		return flags;
	}
}