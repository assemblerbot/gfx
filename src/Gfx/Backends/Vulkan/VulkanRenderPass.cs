using Silk.NET.Vulkan;
using VkRenderPass = Silk.NET.Vulkan.RenderPass;

namespace Gfx;

public sealed unsafe class VulkanRenderPass : RenderPass
{
	private readonly VulkanApi           _api;
	private readonly VulkanLogicalDevice _logicalDevice;
	private readonly VulkanSwapChain     _swapChain;
	
	internal VkRenderPass RenderPass;
	
	public VulkanRenderPass(VulkanApi api, VulkanLogicalDevice logicalDevice, VulkanSwapChain swapChain, RenderPassOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;
		_swapChain     = swapChain;

		if (_swapChain.HasDepthStencil)
		{
			InitColorAndDepthStencil(out RenderPass);
		}
		else
		{
			InitColor(out RenderPass);
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyRenderPass(_logicalDevice.Device, RenderPass, null);
	}

	#region Initialization

	private void InitColor(out VkRenderPass renderPass)
	{
		AttachmentDescription colorAttachment = new()
		                                        {
			                                        Format        = _swapChain.SwapChainImageFormat,
			                                        Samples       = SampleCountFlags.Count1Bit,
			                                        LoadOp        = AttachmentLoadOp.Clear,
			                                        StoreOp       = AttachmentStoreOp.Store,
			                                        StencilLoadOp = AttachmentLoadOp.DontCare,
			                                        InitialLayout = ImageLayout.Undefined,
			                                        FinalLayout   = ImageLayout.PresentSrcKhr,
		                                        };

		AttachmentReference colorAttachmentRef = new()
		                                         {
			                                         Attachment = 0,
			                                         Layout     = ImageLayout.ColorAttachmentOptimal,
		                                         };

		SubpassDescription subpass = new()
		                             {
			                             PipelineBindPoint    = PipelineBindPoint.Graphics,
			                             ColorAttachmentCount = 1,
			                             PColorAttachments    = &colorAttachmentRef,
		                             };

		SubpassDependency dependency = new()
		                               {
			                               SrcSubpass    = Vk.SubpassExternal,
			                               DstSubpass    = 0,
			                               SrcStageMask  = PipelineStageFlags.ColorAttachmentOutputBit,
			                               SrcAccessMask = 0,
			                               DstStageMask  = PipelineStageFlags.ColorAttachmentOutputBit,
			                               DstAccessMask = AccessFlags.ColorAttachmentWriteBit
		                               };

		RenderPassCreateInfo renderPassInfo = new()
		                                      {
			                                      SType           = StructureType.RenderPassCreateInfo,
			                                      AttachmentCount = 1,
			                                      PAttachments    = &colorAttachment,
			                                      SubpassCount    = 1,
			                                      PSubpasses      = &subpass,
			                                      DependencyCount = 1,
			                                      PDependencies   = &dependency,
		                                      };

		if (_api.Vk.CreateRenderPass(_logicalDevice.Device, renderPassInfo, null, out renderPass) != Result.Success)
		{
			throw new GfxException("Failed to create render pass!");
		}
	}
	
	private void InitColorAndDepthStencil(out VkRenderPass renderPass)
	{
		AttachmentDescription colorAttachment = new()
		                                        {
			                                        Format        = _swapChain.SwapChainImageFormat,
			                                        Samples       = _swapChain.MsaaSampleCount,
			                                        LoadOp        = AttachmentLoadOp.Clear,
			                                        StoreOp       = AttachmentStoreOp.Store,
			                                        StencilLoadOp = AttachmentLoadOp.DontCare,
			                                        InitialLayout = ImageLayout.Undefined,
			                                        FinalLayout   = ImageLayout.ColorAttachmentOptimal,
		                                        };

		AttachmentDescription depthAttachment = new()
		                                        {
			                                        Format         = _swapChain.SwapChainDepthStencilFormat,
			                                        Samples        = _swapChain.MsaaSampleCount,
			                                        LoadOp         = AttachmentLoadOp.Clear,
			                                        StoreOp        = AttachmentStoreOp.DontCare,
			                                        StencilLoadOp  = AttachmentLoadOp.DontCare,
			                                        StencilStoreOp = AttachmentStoreOp.DontCare,
			                                        InitialLayout  = ImageLayout.Undefined,
			                                        FinalLayout    = ImageLayout.DepthStencilAttachmentOptimal,
		                                        };

		AttachmentDescription colorAttachmentResolve = new()
		                                               {
			                                               Format         = _swapChain.SwapChainImageFormat,
			                                               Samples        = SampleCountFlags.Count1Bit,
			                                               LoadOp         = AttachmentLoadOp.DontCare,
			                                               StoreOp        = AttachmentStoreOp.Store,
			                                               StencilLoadOp  = AttachmentLoadOp.DontCare,
			                                               StencilStoreOp = AttachmentStoreOp.DontCare,
			                                               InitialLayout  = ImageLayout.Undefined,
			                                               FinalLayout    = ImageLayout.PresentSrcKhr,
		                                               };

		AttachmentReference colorAttachmentRef = new()
		                                         {
			                                         Attachment = 0,
			                                         Layout     = ImageLayout.ColorAttachmentOptimal,
		                                         };

		AttachmentReference depthAttachmentRef = new()
		                                         {
			                                         Attachment = 1,
			                                         Layout     = ImageLayout.DepthStencilAttachmentOptimal,
		                                         };

		AttachmentReference colorAttachmentResolveRef = new()
		                                                {
			                                                Attachment = 2,
			                                                Layout     = ImageLayout.ColorAttachmentOptimal,
		                                                };

		SubpassDescription subpass = new()
		                             {
			                             PipelineBindPoint       = PipelineBindPoint.Graphics,
			                             ColorAttachmentCount    = 1,
			                             PColorAttachments       = &colorAttachmentRef,
			                             PDepthStencilAttachment = &depthAttachmentRef,
			                             PResolveAttachments     = &colorAttachmentResolveRef,
		                             };

		SubpassDependency dependency = new()
		                               {
			                               SrcSubpass    = Vk.SubpassExternal,
			                               DstSubpass    = 0,
			                               SrcStageMask  = PipelineStageFlags.ColorAttachmentOutputBit | PipelineStageFlags.EarlyFragmentTestsBit,
			                               SrcAccessMask = 0,
			                               DstStageMask  = PipelineStageFlags.ColorAttachmentOutputBit | PipelineStageFlags.EarlyFragmentTestsBit,
			                               DstAccessMask = AccessFlags.ColorAttachmentWriteBit         | AccessFlags.DepthStencilAttachmentWriteBit
		                               };

		var attachments = new[] { colorAttachment, depthAttachment, colorAttachmentResolve };

		fixed (AttachmentDescription* attachmentsPtr = attachments)
		{
			RenderPassCreateInfo renderPassInfo = new()
			                                      {
				                                      SType           = StructureType.RenderPassCreateInfo,
				                                      AttachmentCount = (uint)attachments.Length,
				                                      PAttachments    = attachmentsPtr,
				                                      SubpassCount    = 1,
				                                      PSubpasses      = &subpass,
				                                      DependencyCount = 1,
				                                      PDependencies   = &dependency,
			                                      };

			if (_api.Vk.CreateRenderPass(_logicalDevice.Device, renderPassInfo, null, out renderPass) != Result.Success)
			{
				throw new GfxException("Failed to create render pass!");
			}
		}
	}

	#endregion
}