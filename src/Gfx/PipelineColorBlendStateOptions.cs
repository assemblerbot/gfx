namespace Gfx;

public struct PipelineColorBlendStateOptions
{
	public PipelineColorBlendStateCreateFlags  Flags;
	public bool                                LogicOpEnable;
	public LogicOp                             LogicOp;
	public PipelineColorBlendAttachmentState[] Attachments;
	public float[]                             BlendConstants = new float[4]; // unsafe fixed float[4] in vulkan implementation

	public PipelineColorBlendStateOptions(PipelineColorBlendStateCreateFlags flags, bool logicOpEnable, LogicOp logicOp, PipelineColorBlendAttachmentState[] attachments)
	{
		Flags         = flags;
		LogicOpEnable = logicOpEnable;
		LogicOp       = logicOp;
		Attachments   = attachments;
	}
}