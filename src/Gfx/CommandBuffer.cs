namespace Gfx;

public abstract class CommandBuffer : IDisposable
{
	public abstract void Dispose();

	public abstract void Begin(CommandBufferUsage usage = CommandBufferUsage.None);
	public abstract void End();

	public abstract void BeginRenderPass(RenderPass renderPass, SwapChain swapChain, int swapChainBufferIndex, float r, float g, float b, float a, float depth, uint stencil);
	public abstract void EndRenderPass();

	public abstract void BindPipeline(GraphicsPipeline pipeline);
	public abstract void BindVertexBuffer(DeviceBuffer buffer, uint binding, ulong offset);
	
	public abstract void CopyBuffer(DeviceBuffer src, DeviceBuffer dst, ulong srcOffset, ulong dstOffset, ulong size);

	public abstract void Draw(uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
}