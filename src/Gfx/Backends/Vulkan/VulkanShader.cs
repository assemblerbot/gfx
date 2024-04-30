using Silk.NET.Core.Native;
using Silk.NET.Vulkan;

namespace Gfx;

public sealed unsafe class VulkanShader : Shader
{
	private readonly VulkanApi                     _api;
	private readonly VulkanLogicalDevice           _logicalDevice;
	public readonly PipelineShaderStageCreateInfo StageInfo;
	public readonly ShaderModule                  ShaderModule;
	
	public VulkanShader(VulkanApi api, VulkanLogicalDevice logicalDevice, ShaderOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;
		
		ShaderModuleCreateInfo createInfo = new()
		                                    {
			                                    SType    = StructureType.ShaderModuleCreateInfo,
			                                    CodeSize = (nuint)options.Code.Length,
		                                    };

		fixed (byte* codePtr = options.Code)
		{
			createInfo.PCode = (uint*)codePtr;

			if (_api.Vk.CreateShaderModule(_logicalDevice.Device, createInfo, null, out ShaderModule) != Result.Success)
			{
				throw new GfxException("Shader module creation failed!");
			}
		}
		
		StageInfo = new()
		             {
			             SType  = StructureType.PipelineShaderStageCreateInfo,
			             Stage  = options.Stage.ToVulkan(),
			             Module = ShaderModule,
			             PName  = (byte*)SilkMarshal.StringToPtr(options.MainFunction)
		             };
	}

	public override void Dispose()
	{
		_api.Vk.DestroyShaderModule(_logicalDevice.Device, ShaderModule, null);
		SilkMarshal.Free((nint) StageInfo.PName);
	}
}