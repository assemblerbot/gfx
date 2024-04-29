namespace Gfx;

public enum DescriptorKind
{
	Sampler                  = 0,
	CombinedImageSampler     = 1,
	SampledImage             = 2,
	StorageImage             = 3,
	UniformTexelBuffer       = 4,
	StorageTexelBuffer       = 5,
	UniformBuffer            = 6,
	StorageBuffer            = 7,
	UniformBufferDynamic     = 8,
	StorageBufferDynamic     = 9,
	InputAttachment          = 10,         // 0x0000000A
	InlineUniformBlock       = 1000138000, // 0x3B9CE510
	InlineUniformBlockExt    = 1000138000, // 0x3B9CE510
	AccelerationStructureKhr = 1000150000, // 0x3B9D13F0
	AccelerationStructureNV  = 1000165000, // 0x3B9D4E88
	MutableExt               = 1000351000, // 0x3BA02518
	MutableValve             = 1000351000, // 0x3BA02518
	SampleWeightImageQCom    = 1000440000, // 0x3BA180C0
	BlockMatchImageQCom      = 1000440001, // 0x3BA180C1
}