using Silk.NET.Vulkan;

namespace Gfx;

public sealed class VulkanCommandBuffer : CommandBuffer
{
	private readonly VulkanApi                     _api;
	private readonly VulkanLogicalDevice           _logicalDevice;
	public readonly Silk.NET.Vulkan.CommandBuffer CommandBuffer;
	
	public VulkanCommandBuffer(VulkanApi api, VulkanLogicalDevice logicalDevice, CommandBufferOptions options)
	{
		CommandBufferAllocateInfo allocateInfo = new()
		                                         {
			                                         SType              = StructureType.CommandBufferAllocateInfo,
			                                         Level              = options.Level.ToVulkanCommandBufferLevel(),
			                                         CommandPool        = logicalDevice.CommandPool,
			                                         CommandBufferCount = 1,
		                                         };

		_api.Vk.AllocateCommandBuffers(_logicalDevice.Device, allocateInfo, out CommandBuffer);
	}

	public override void Dispose()
	{
		_api.Vk.FreeCommandBuffers(_logicalDevice.Device, _logicalDevice.CommandPool, 1, CommandBuffer);
	}

	public override void Begin(CommandBufferUsage usage)
	{
		CommandBufferBeginInfo beginInfo = new()
		                                   {
			                                   SType = StructureType.CommandBufferBeginInfo,
			                                   Flags = usage.ToVulkanCommandBufferUsageFlags(),
		                                   };

		_api.Vk.BeginCommandBuffer(CommandBuffer, beginInfo);
	}

	public override void End()
	{
		_api.Vk.EndCommandBuffer(CommandBuffer);
	}

	public override void CopyBuffer(DeviceBuffer src, DeviceBuffer dst, ulong srcOffset, ulong dstOffset, ulong size)
	{
		BufferCopy copyRegion = new()
		                        {
			                        SrcOffset = srcOffset,
			                        DstOffset = dstOffset,
			                        Size = size,
		                        };
		
		_api.Vk.CmdCopyBuffer(CommandBuffer, ((VulkanDeviceBuffer) src).Buffer, ((VulkanDeviceBuffer) dst).Buffer, 1, copyRegion);
	}
}