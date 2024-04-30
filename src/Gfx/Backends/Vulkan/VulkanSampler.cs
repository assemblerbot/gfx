using Silk.NET.Vulkan;
using VkSampler = Silk.NET.Vulkan.Sampler;

namespace Gfx;

public sealed unsafe class VulkanSampler : Sampler
{
	private readonly VulkanApi               _api;
	private readonly VulkanLogicalDevice     _logicalDevice;
	public readonly VkSampler Sampler;

	public VulkanSampler(VulkanApi api, VulkanLogicalDevice logicalDevice, SamplerOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;

		SamplerCreateInfo samplerInfo = new()
		                                {
			                                SType                   = StructureType.SamplerCreateInfo,
			                                PNext                   = null,
			                                Flags                   = options.SamplerFlags.ToVulkan(),
			                                MagFilter               = options.MagFilter.ToVulkan(),
			                                MinFilter               = options.MinFilter.ToVulkan(),
			                                MipmapMode              = options.MipmapMode.ToVulkan(),
			                                AddressModeU            = options.AddressModeU.ToVulkan(),
			                                AddressModeV            = options.AddressModeV.ToVulkan(),
			                                AddressModeW            = options.AddressModeW.ToVulkan(),
			                                MipLodBias              = options.MipLodBias,
			                                AnisotropyEnable        = options.AnisotropyEnable,
			                                MaxAnisotropy           = options.MaxAnisotropy,
			                                CompareEnable           = options.CompareEnable,
			                                CompareOp               = options.CompareOp.ToVulkan(),
			                                MinLod                  = options.MinLod,
			                                MaxLod                  = options.MaxLod,
			                                BorderColor             = options.BorderColor.ToVulkan(),
			                                UnnormalizedCoordinates = options.UnnormalizedCoordinates,
		                                };
		
		fixed (VkSampler* samplerPtr = &Sampler)
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