using System.Runtime.InteropServices;
using Silk.NET.Vulkan;
using VkDescriptorSetLayout = Silk.NET.Vulkan.DescriptorSetLayout;
using VkDescriptorSetLayoutBinding = Silk.NET.Vulkan.DescriptorSetLayoutBinding;
using VkSampler = Silk.NET.Vulkan.Sampler;

namespace Gfx;

public sealed unsafe class VulkanDescriptorSetLayout : DescriptorSetLayout
{
	private readonly VulkanApi                           _api;
	private readonly VulkanLogicalDevice                 _logicalDevice;
	public readonly VkDescriptorSetLayout Layout;
	
	public VulkanDescriptorSetLayout(VulkanApi api, VulkanLogicalDevice logicalDevice, DescriptorSetLayoutOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;
		
		VkDescriptorSetLayoutBinding* bindings = stackalloc VkDescriptorSetLayoutBinding[options.Bindings?.Length ?? 0];
		if (options.Bindings is not null)
		{
			for (int i = 0; i < options.Bindings.Length; ++i)
			{
				int        samplersCount = options.Bindings[i].Samplers.Length;
				VkSampler* samplers      = stackalloc VkSampler[samplersCount];

				for (int j = 0; j < samplersCount; ++j)
				{
					samplers[j] = ((VulkanSampler) options.Bindings[i].Samplers[j]).Sampler;
				}

				bindings[i] = new VkDescriptorSetLayoutBinding(
					options.Bindings[i].Binding,
					options.Bindings[i].DescriptorKind.ToVulkan(),
					options.Bindings[i].DescriptorCount,
					options.Bindings[i].ShaderStage.ToVulkan(),
					samplers
				);
			}
		}

		Result result = Result.Success;
		fixed (VkDescriptorSetLayout* descriptorSetLayoutPtr = &Layout)
		{
			Silk.NET.Vulkan.DescriptorSetLayoutCreateInfo info = new()
			                                                     {
				                                                     SType        = StructureType.DescriptorSetLayoutCreateInfo,
				                                                     BindingCount = (uint)(options.Bindings?.Length ?? 0),
				                                                     PBindings    = bindings,
			                                                     };

			result = _api.Vk.CreateDescriptorSetLayout(_logicalDevice.Device, info, null, descriptorSetLayoutPtr);
		}

		if (result != Result.Success)
		{
			throw new GfxException($"Failed to create descriptor set layout! Result:{result}");
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyDescriptorSetLayout(_logicalDevice.Device, Layout, null);
	}
}