using Silk.NET.Vulkan;

namespace Gfx;

public sealed class VulkanCommandBuffer : CommandBuffer
{
	private readonly VulkanApi                     _api;
	private readonly VulkanLogicalDevice           _logicalDevice;
	private readonly Silk.NET.Vulkan.CommandBuffer _commandBuffer;
	
	public VulkanCommandBuffer(VulkanApi api, VulkanLogicalDevice logicalDevice, CommandBufferOptions options)
	{
		CommandBufferAllocateInfo allocateInfo = new()
		                                         {
			                                         SType              = StructureType.CommandBufferAllocateInfo,
			                                         Level              = options.Level.ToVulkanCommandBufferLevel(),
			                                         CommandPool        = logicalDevice.CommandPool,
			                                         CommandBufferCount = 1,
		                                         };

		_api.Vk.AllocateCommandBuffers(_logicalDevice.Device, allocateInfo, out _commandBuffer);
	}

	public override void Dispose()
	{
		_api.Vk.FreeCommandBuffers(_logicalDevice.Device, _logicalDevice.CommandPool, 1, _commandBuffer);
	}

	public override void Begin(CommandBufferUsage usage)
	{
		CommandBufferBeginInfo beginInfo = new()
		                                   {
			                                   SType = StructureType.CommandBufferBeginInfo,
			                                   Flags = usage.ToVulkanCommandBufferUsageFlags(),
		                                   };

		_api.Vk.BeginCommandBuffer(_commandBuffer, beginInfo);
	}

	public override void End()
	{
		_api.Vk.EndCommandBuffer(_commandBuffer);
	}
}