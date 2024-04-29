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
		
		List<IntPtr> allocatedPointers = new();
		
		VkDescriptorSetLayoutBinding[] bindings = new VkDescriptorSetLayoutBinding[options.Bindings.Length];
		for (int i = 0; i < options.Bindings.Length; ++i)
		{
			IntPtr samplersPtr = default;

			int samplersCount = options.Bindings[i].Samplers.Length;
			if (samplersCount > 0)
			{
				samplersPtr = Marshal.AllocHGlobal(samplersCount * sizeof(VkSampler));
				allocatedPointers.Add(samplersPtr);
				
				for (int j = 0; j < samplersCount; ++j)
				{
					((VkSampler*) samplersPtr.ToPointer())[j] = ((VulkanSampler) options.Bindings[i].Samplers[j]).Sampler;
				}
			}

			bindings[i] = new VkDescriptorSetLayoutBinding(
				options.Bindings[i].Binding,
				options.Bindings[i].DescriptorKind.ToVulkanDescriptorType(),
				options.Bindings[i].DescriptorCount,
				options.Bindings[i].ShaderStage.ToVulkanShaderStageFlags(),
				(VkSampler*) samplersPtr.ToPointer()
			);
		}

		Result result = Result.Success;
		fixed (VkDescriptorSetLayoutBinding* bindingsPtr = bindings)
		fixed (VkDescriptorSetLayout* descriptorSetLayoutPtr = &Layout)
		{
			Silk.NET.Vulkan.DescriptorSetLayoutCreateInfo info = new()
			                                                     {
				                                                     SType        = StructureType.DescriptorSetLayoutCreateInfo,
				                                                     BindingCount = (uint)bindings.Length,
				                                                     PBindings    = bindingsPtr,
			                                                     };

			result = _api.Vk.CreateDescriptorSetLayout(_logicalDevice.Device, info, null, descriptorSetLayoutPtr);
		}

		foreach (IntPtr ptr in allocatedPointers)
		{
			Marshal.FreeHGlobal(ptr);
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