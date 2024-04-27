namespace Gfx;

public abstract class LogicalDevice : IDisposable
{
	public abstract void Dispose();

	public abstract SwapChain     CreateSwapChain(SwapChainOptions         options);
	public abstract DeviceMemory  AllocateMemory(DeviceMemoryOptions       options);
	public abstract DeviceBuffer  CreateBuffer(DeviceBufferOptions         options);
	public abstract Shader        CreateShader(ShaderOptions               options);
	public abstract CommandBuffer CreateCommandBuffer(CommandBufferOptions options);

	// TODO - Vulkan supports bulk command buffer operations (create, free, submit), but it should be packed in one class probably, not one-by-one command buffer
	//public abstract CommandBuffer[] CreateCommandBuffers(CommandBufferOptions options, int count);

	public abstract Sampler CreateSampler(SamplerOptions options);
	
	public abstract void QueueSubmit(DeviceQueue   queue, CommandBuffer commandBuffer);
	public abstract void QueueSubmit(DeviceQueue   queue, CommandBuffer commandBuffer, SwapChain swapChain, int fenceIndex);
	public abstract void QueueWaitIdle(DeviceQueue queue);
	
	public abstract void          WaitIdle();
	
	
}