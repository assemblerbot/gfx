using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using VkCommandBuffer = Silk.NET.Vulkan.CommandBuffer;

namespace Gfx;

public sealed unsafe class VulkanLogicalDevice : LogicalDevice
{
	private const float _queuePriority = 1f; // from every queue family we need just one queue (priority: 0..1)
	
	private readonly VulkanApi            _api;
	internal readonly VulkanPhysicalDevice PhysicalDevice;
    
	internal readonly Device Device;

	private readonly  Queue  _graphicsQueue;
	private readonly  Queue  _presentQueue;
	
	public readonly CommandPool CommandPool;
    
	internal VulkanLogicalDevice(VulkanApi api, LogicalDeviceOptions options)
	{
		_api            = api;
		PhysicalDevice = (VulkanPhysicalDevice)options.PhysicalDevice;

		InitDeviceAndQueues(out Device, out _graphicsQueue, out _presentQueue);
		InitCommandPool(out CommandPool);
	}

	public override void Dispose()
	{
		_api.Vk.DestroyCommandPool(Device, CommandPool, null);
		_api.Vk.DestroyDevice(Device, null);
	}

	public override void WaitIdle()
	{
		_api.Vk.DeviceWaitIdle(Device);
	}

	public override SwapChain CreateSwapChain(SwapChainOptions options)
	{
		return new VulkanSwapChain(_api, PhysicalDevice, this, options);
	}

	public override DeviceMemory AllocateMemory(DeviceMemoryOptions options)
	{
		return new VulkanDeviceMemory(_api, this, options);
	}

	public override DeviceBuffer CreateBuffer(DeviceBufferOptions options)
	{
		return new VulkanDeviceBuffer(_api, this, options);
	}

	public override Shader CreateShader(ShaderOptions options)
	{
		return new VulkanShader(_api, this, options);
	}

	public override CommandBuffer CreateCommandBuffer(CommandBufferOptions options)
	{
		return new VulkanCommandBuffer(_api, this, options);
	}

	public override RenderPass CreateRenderPass(SwapChain swapChain, RenderPassOptions options)
	{
		return new VulkanRenderPass(_api, this, (VulkanSwapChain) swapChain, options);
	}

	public override Sampler CreateSampler(SamplerOptions options)
	{
		return new VulkanSampler(_api, this, options);
	}

	public override DescriptorSetLayout CreateDescriptorSetLayout(DescriptorSetLayoutOptions options)
	{
		return new VulkanDescriptorSetLayout(_api, this, options);
	}

	public override PipelineLayout CreatePipelineLayout(DescriptorSetLayout layout)
	{
		return new VulkanPipelineLayout(_api, this, layout);
	}

	public override GraphicsPipeline CreateGraphicsPipeline(GraphicsPipelineOptions options)
	{
		return new VulkanGraphicsPipeline(_api, this, options);
	}

	public override void QueueSubmit(DeviceQueue queue, CommandBuffer commandBuffer)
	{
		fixed (VkCommandBuffer* buffer = &((VulkanCommandBuffer) commandBuffer).CommandBuffer)
		{

			SubmitInfo submitInfo = new()
			                        {
				                        SType              = StructureType.SubmitInfo,
				                        CommandBufferCount = 1,
				                        PCommandBuffers    = buffer,
			                        };

			_api.Vk.QueueSubmit(GetQueue(queue), 1, submitInfo, default);
		}
	}

	public override void QueueSubmit(DeviceQueue queue, CommandBuffer commandBuffer, SwapChain swapChain, int fenceIndex)
	{
		throw new NotImplementedException(); // todo - get fence by index from swap chain
	}

	public override void QueueWaitIdle(DeviceQueue queue)
	{
		_api.Vk.QueueWaitIdle(GetQueue(queue));
	}

	#region Initialization
	private void InitDeviceAndQueues(out Device device, out Queue graphicsQueue, out Queue presentQueue)
	{
		uint[] uniqueQueueFamilies = { PhysicalDevice.GraphicsQueueFamily!.Value, PhysicalDevice.PresentQueueFamily!.Value };
		uniqueQueueFamilies = uniqueQueueFamilies.Distinct().ToArray();

		using var mem              = GlobalMemory.Allocate(uniqueQueueFamilies.Length * sizeof(DeviceQueueCreateInfo));
		var       queueCreateInfos = (DeviceQueueCreateInfo*)Unsafe.AsPointer(ref mem.GetPinnableReference());

		float queuePriority = _queuePriority;
		for (int i = 0; i < uniqueQueueFamilies.Length; i++)
		{
			queueCreateInfos[i] = new()
			                      {
				                      SType            = StructureType.DeviceQueueCreateInfo,
				                      QueueFamilyIndex = uniqueQueueFamilies[i],
				                      QueueCount       = 1,
				                      PQueuePriorities = &queuePriority
			                      };
		}

		PhysicalDeviceFeatures deviceFeatures = new()
		                                        {
			                                        SamplerAnisotropy = true,
		                                        };

		DeviceCreateInfo createInfo = new()
		                              {
			                              SType                = StructureType.DeviceCreateInfo,
			                              QueueCreateInfoCount = (uint)uniqueQueueFamilies.Length,
			                              PQueueCreateInfos    = queueCreateInfos,

			                              PEnabledFeatures = &deviceFeatures,

			                              EnabledExtensionCount   = (uint)VulkanExtensions.GraphicsExtensions.Length,
			                              PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanExtensions.GraphicsExtensions)
		                              };

		if (_api.IsDebugEnabled)
		{
			createInfo.EnabledLayerCount   = (uint)VulkanValidationLayers.DebugValidationLayers.Length;
			createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanValidationLayers.DebugValidationLayers);
		}
		else
		{
			createInfo.EnabledLayerCount = 0;
		}

		if (_api.Vk.CreateDevice(PhysicalDevice.Device, in createInfo, null, out device) != Result.Success)
		{
			throw new GfxException("Failed to create logical device!");
		}

		_api.Vk.GetDeviceQueue(Device, PhysicalDevice.GraphicsQueueFamily!.Value, 0, out graphicsQueue);
		_api.Vk.GetDeviceQueue(Device, PhysicalDevice.PresentQueueFamily!.Value,  0, out presentQueue);

		if (_api.IsDebugEnabled)
		{
			SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
		}

		SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
	}

	private void InitCommandPool(out CommandPool commandPool)
	{
		CommandPoolCreateInfo poolInfo = new()
		                                 {
			                                 SType            = StructureType.CommandPoolCreateInfo,
			                                 QueueFamilyIndex = PhysicalDevice.GraphicsQueueFamily!.Value,
		                                 };

		if (_api.Vk.CreateCommandPool(Device, poolInfo, null, out commandPool) != Result.Success)
		{
			throw new GfxException("Failed to create command pool!");
		}
	}

	private Queue GetQueue(DeviceQueue queue)
	{
		return queue switch
		{
			DeviceQueue.Graphics => _graphicsQueue,
			DeviceQueue.Present => _presentQueue,
		};
	}

	#endregion
	
}