using Silk.NET.Vulkan;

namespace Gfx;

using VkBuffer = Silk.NET.Vulkan.Buffer;

public sealed unsafe class VulkanCommandBuffer : CommandBuffer
{
	private readonly VulkanApi                     _api;
	private readonly VulkanLogicalDevice           _logicalDevice;
	public readonly  Silk.NET.Vulkan.CommandBuffer CommandBuffer;
	
	public VulkanCommandBuffer(VulkanApi api, VulkanLogicalDevice logicalDevice, CommandBufferOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;
		
		CommandBufferAllocateInfo allocateInfo = new()
		                                         {
			                                         SType              = StructureType.CommandBufferAllocateInfo,
			                                         Level              = options.Level.ToVulkan(),
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
			                                   Flags = usage.ToVulkan(),
		                                   };

		_api.Vk.BeginCommandBuffer(CommandBuffer, beginInfo);
	}

	public override void End()
	{
		_api.Vk.EndCommandBuffer(CommandBuffer);
	}

	public override void BeginRenderPass(RenderPass renderPass, SwapChain swapChain, int swapChainBufferIndex, float r, float g, float b, float a, float depth, uint stencil)
	{
		VulkanSwapChain vkSwapChain = (VulkanSwapChain) swapChain;
		
		RenderPassBeginInfo renderPassBeginInfo = new()
		                                          {
			                                          SType       = StructureType.RenderPassBeginInfo,
			                                          RenderPass  = ((VulkanRenderPass)renderPass).RenderPass,
			                                          Framebuffer = vkSwapChain.SwapChainFramebuffers[swapChainBufferIndex],
			                                          RenderArea =
			                                          {
				                                          Offset = { X = 0, Y = 0 },
				                                          Extent = new Extent2D(vkSwapChain.Width, vkSwapChain.Height),
			                                          }
		                                          };

		ClearValue[] clearValues = new ClearValue[]
		                           {
			                           new()
			                           {
				                           Color = new() {Float32_0 = r, Float32_1 = g, Float32_2 = b, Float32_3 = a} 
			                           },
			                           new()
			                           {
				                           DepthStencil = new() {Depth = depth, Stencil = stencil}
			                           }
		                           };

		fixed (ClearValue* clearValuesPtr = clearValues)
		{
			renderPassBeginInfo.ClearValueCount = (uint)clearValues.Length;
			renderPassBeginInfo.PClearValues    = clearValuesPtr;

			_api.Vk.CmdBeginRenderPass(CommandBuffer, &renderPassBeginInfo, SubpassContents.Inline); // TODO - enum customization?
		}
	}

	public override void EndRenderPass()
	{
		_api.Vk.CmdEndRenderPass(CommandBuffer);
	}

	public override void BindPipeline(GraphicsPipeline pipeline)
	{
		_api.Vk.CmdBindPipeline(CommandBuffer, PipelineBindPoint.Graphics, ((VulkanGraphicsPipeline) pipeline).Pipeline);
	}

	public override void BindVertexBuffer(DeviceBuffer buffer, uint binding, ulong offset)
	{
		var vertexBuffers = new VkBuffer[] { ((VulkanDeviceBuffer)buffer).Buffer };
		var offsets       = new ulong[] { 0 };

		fixed (ulong* offsetsPtr = offsets)
		fixed (VkBuffer* vertexBuffersPtr = vertexBuffers)
		{
			_api.Vk.CmdBindVertexBuffers(CommandBuffer, binding, 1, vertexBuffersPtr, offsetsPtr);
		}
	}

	public override void CopyBuffer(DeviceBuffer src, DeviceBuffer dst, ulong srcOffset, ulong dstOffset, ulong size)
	{
		BufferCopy copyRegion = new()
		                        {
			                        SrcOffset = srcOffset,
			                        DstOffset = dstOffset,
			                        Size      = size,
		                        };
		
		_api.Vk.CmdCopyBuffer(CommandBuffer, ((VulkanDeviceBuffer) src).Buffer, ((VulkanDeviceBuffer) dst).Buffer, 1, copyRegion);
	}

	public override void Draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance)
	{
		_api.Vk.CmdDraw(CommandBuffer, vertexCount, instanceCount, firstVertex, firstInstance);
	}
}