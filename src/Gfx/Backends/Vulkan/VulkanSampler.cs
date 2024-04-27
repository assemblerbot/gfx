using Silk.NET.Vulkan;

namespace Gfx;

public sealed unsafe class VulkanSampler : Sampler
{
	private readonly VulkanApi               _api;
	private readonly VulkanLogicalDevice     _logicalDevice;
	public readonly Silk.NET.Vulkan.Sampler Sampler;

	public VulkanSampler(VulkanApi api, VulkanLogicalDevice logicalDevice, SamplerOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;

		SamplerCreateInfo samplerInfo = new()
		                                {
			                                SType                   = StructureType.SamplerCreateInfo,
			                                PNext                   = null,
			                                Flags                   = options.SamplerFlags.ToVulkanSamplerCreateFlags(),
			                                MagFilter               = options.MagFilter.ToVulkanFilter(),
			                                MinFilter               = options.MinFilter.ToVulkanFilter(),
			                                MipmapMode              = options.MipmapMode.ToVulkanSamplerMipmapMode(),
			                                AddressModeU            = options.AddressModeU.ToVulkanSamplerAddressMode(),
			                                AddressModeV            = options.AddressModeV.ToVulkanSamplerAddressMode(),
			                                AddressModeW            = options.AddressModeW.ToVulkanSamplerAddressMode(),
			                                MipLodBias              = options.MipLodBias,
			                                AnisotropyEnable        = options.AnisotropyEnable,
			                                MaxAnisotropy           = options.MaxAnisotropy,
			                                CompareEnable           = options.CompareEnable,
			                                CompareOp               = options.CompareOp.ToVulkanCompareOp(),
			                                MinLod                  = options.MinLod,
			                                MaxLod                  = options.MaxLod,
			                                BorderColor             = options.BorderColor.ToVulkanBorderColor(),
			                                UnnormalizedCoordinates = options.UnnormalizedCoordinates,
		                                };
		
		fixed (Silk.NET.Vulkan.Sampler* samplerPtr = &Sampler)
		{
			Result result = _api.Vk.CreateSampler(_logicalDevice.Device, samplerInfo, null, samplerPtr);
			if (result != Result.Success)
			{
				throw new GfxException($"Failed to create texture sampler! Result: {result}");
			}
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroySampler(_logicalDevice.Device, Sampler, null);
	}
}