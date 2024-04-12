using Silk.NET.Vulkan;

namespace Gfx;

public unsafe class VulkanRenderPass : RenderPass
{
	private readonly VulkanApi                  _api;
	private readonly VulkanLogicalDevice        _device;
	
	private          Silk.NET.Vulkan.RenderPass _renderPass;
	
	public VulkanRenderPass(VulkanApi api, VulkanLogicalDevice logicalDevice, RenderPassOptions options)
	{
		_api    = api;
		_device = logicalDevice;

		if (_device.HasDepthStencil)
		{
			InitColorAndDepthStencil(out _renderPass);
		}
		else
		{
			InitColor(out _renderPass);
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyRenderPass(_device.Device, _renderPass, null);
	}

	#region Initialization

	private void InitColor(out Silk.NET.Vulkan.RenderPass renderPass)
	{
		AttachmentDescription colorAttachment = new()
		                                        {
			                                        Format        = _device.SwapChainImageFormat,
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

		if (_api.Vk.CreateRenderPass(_device.Device, renderPassInfo, null, out renderPass) != Result.Success)
		{
			throw new GfxException("Failed to create render pass!");
		}
	}
	
	private void InitColorAndDepthStencil(out Silk.NET.Vulkan.RenderPass renderPass)
	{
		AttachmentDescription colorAttachment = new()
		                                        {
			                                        Format        = _device.SwapChainImageFormat,
			                                        Samples       = _device.MsaaSampleCount,
			                                        LoadOp        = AttachmentLoadOp.Clear,
			                                        StoreOp       = AttachmentStoreOp.Store,
			                                        StencilLoadOp = AttachmentLoadOp.DontCare,
			                                        InitialLayout = ImageLayout.Undefined,
			                                        FinalLayout   = ImageLayout.ColorAttachmentOptimal,
		                                        };

		AttachmentDescription depthAttachment = new()
		                                        {
			                                        Format         = _device.SwapChainDepthStencilFormat,
			                                        Samples        = _device.MsaaSampleCount,
			                                        LoadOp         = AttachmentLoadOp.Clear,
			                                        StoreOp        = AttachmentStoreOp.DontCare,
			                                        StencilLoadOp  = AttachmentLoadOp.DontCare,
			                                        StencilStoreOp = AttachmentStoreOp.DontCare,
			                                        InitialLayout  = ImageLayout.Undefined,
			                                        FinalLayout    = ImageLayout.DepthStencilAttachmentOptimal,
		                                        };

		AttachmentDescription colorAttachmentResolve = new()
		                                               {
			                                               Format         = _device.SwapChainImageFormat,
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

			if (_api.Vk.CreateRenderPass(_device.Device, renderPassInfo, null, out renderPass) != Result.Success)
			{
				throw new GfxException("Failed to create render pass!");
			}
		}
	}

	#endregion
}