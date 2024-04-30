using Silk.NET.Core.Native;
using Silk.NET.Vulkan;

namespace Gfx;

public sealed unsafe class VulkanShader : Shader
{
	private readonly VulkanApi                     _api;
	private readonly VulkanLogicalDevice           _logicalDevice;
	private readonly PipelineShaderStageCreateInfo _stageInfo;
	private readonly ShaderModule                  _shaderModule;
	
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

			if (_api.Vk.CreateShaderModule(_logicalDevice.Device, createInfo, null, out _shaderModule) != Result.Success)
			{
				throw new GfxException("Shader module creation failed!");
			}
		}
		
		_stageInfo = new()
		             {
			             SType  = StructureType.PipelineShaderStageCreateInfo,
			             Stage  = options.Stage.ToVulkan(),
			             Module = _shaderModule,
			             PName  = (byte*)SilkMarshal.StringToPtr(options.MainFunction)
		             };
	}

	public override void Dispose()
	{
		_api.Vk.DestroyShaderModule(_logicalDevice.Device, _shaderModule, null);
		SilkMarshal.Free((nint) _stageInfo.PName);
	}
}