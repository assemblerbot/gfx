using Silk.NET.Vulkan;

namespace Gfx;

public static class VulkanDeviceBufferUsageExtensions
{
	public static BufferUsageFlags ToVulkan(this DeviceBufferUsage usage)
	{
		BufferUsageFlags flags = BufferUsageFlags.None;

		if((usage & DeviceBufferUsage.TransferSrc)                                != 0){flags |= BufferUsageFlags.TransferSrcBit;}
		if((usage & DeviceBufferUsage.TransferDst)                                != 0){flags |= BufferUsageFlags.TransferDstBit;}
		if((usage & DeviceBufferUsage.UniformTexelBuffer)                         != 0){flags |= BufferUsageFlags.UniformTexelBufferBit;}
		if((usage & DeviceBufferUsage.StorageTexelBuffer)                         != 0){flags |= BufferUsageFlags.StorageTexelBufferBit;}
		if((usage & DeviceBufferUsage.UniformBuffer)                              != 0){flags |= BufferUsageFlags.UniformBufferBit;}
		if((usage & DeviceBufferUsage.StorageBuffer)                              != 0){flags |= BufferUsageFlags.StorageBufferBit;}
		if((usage & DeviceBufferUsage.IndexBuffer)                                != 0){flags |= BufferUsageFlags.IndexBufferBit;}
		if((usage & DeviceBufferUsage.VertexBuffer)                               != 0){flags |= BufferUsageFlags.VertexBufferBit;}
		if((usage & DeviceBufferUsage.IndirectBuffer)                             != 0){flags |= BufferUsageFlags.IndirectBufferBit;}
		if((usage & DeviceBufferUsage.VideoDecodeSrcKhr)                          != 0){flags |= BufferUsageFlags.VideoDecodeSrcBitKhr;}
		if((usage & DeviceBufferUsage.VideoDecodeDstKhr)                          != 0){flags |= BufferUsageFlags.VideoDecodeDstBitKhr;}
		if((usage & DeviceBufferUsage.TransformFeedbackBufferExt)                 != 0){flags |= BufferUsageFlags.TransformFeedbackBufferBitExt;}
		if((usage & DeviceBufferUsage.TransformFeedbackCounterBufferExt)          != 0){flags |= BufferUsageFlags.TransformFeedbackCounterBufferBitExt;}
		if((usage & DeviceBufferUsage.ConditionalRenderingExt)                    != 0){flags |= BufferUsageFlags.ConditionalRenderingBitExt;}
		if((usage & DeviceBufferUsage.ExecutionGraphScratchAmdx)                  != 0){flags |= BufferUsageFlags.ExecutionGraphScratchBitAmdx;}
		if((usage & DeviceBufferUsage.AccelerationStructureBuildInputReadOnlyKhr) != 0){flags |= BufferUsageFlags.AccelerationStructureBuildInputReadOnlyBitKhr;}
		if((usage & DeviceBufferUsage.AccelerationStructureStorageKhr)            != 0){flags |= BufferUsageFlags.AccelerationStructureStorageBitKhr;}
		if((usage & DeviceBufferUsage.ShaderBindingTableKhr)                      != 0){flags |= BufferUsageFlags.ShaderBindingTableBitKhr;}
		if((usage & DeviceBufferUsage.RayTracingNV)                               != 0){flags |= BufferUsageFlags.RayTracingBitNV;}
		if((usage & DeviceBufferUsage.ShaderDeviceAddressExt)                     != 0){flags |= BufferUsageFlags.ShaderDeviceAddressBitExt;}
		if((usage & DeviceBufferUsage.ShaderDeviceAddressKhr)                     != 0){flags |= BufferUsageFlags.ShaderDeviceAddressBitKhr;}
		if((usage & DeviceBufferUsage.VideoEncodeDstKhr)                          != 0){flags |= BufferUsageFlags.VideoEncodeDstBitKhr;}
		if((usage & DeviceBufferUsage.VideoEncodeSrcKhr)                          != 0){flags |= BufferUsageFlags.VideoEncodeSrcBitKhr;}
		if((usage & DeviceBufferUsage.SamplerDescriptorBufferExt)                 != 0){flags |= BufferUsageFlags.SamplerDescriptorBufferBitExt;}
		if((usage & DeviceBufferUsage.ResourceDescriptorBufferExt)                != 0){flags |= BufferUsageFlags.ResourceDescriptorBufferBitExt;}
		if((usage & DeviceBufferUsage.PushDescriptorsDescriptorBufferExt)         != 0){flags |= BufferUsageFlags.PushDescriptorsDescriptorBufferBitExt;}
		if((usage & DeviceBufferUsage.MicromapBuildInputReadOnlyExt)              != 0){flags |= BufferUsageFlags.MicromapBuildInputReadOnlyBitExt;}
		if((usage & DeviceBufferUsage.MicromapStorageExt)                         != 0){flags |= BufferUsageFlags.MicromapStorageBitExt;}
		if((usage & DeviceBufferUsage.ShaderDeviceAddress)                        != 0){flags |= BufferUsageFlags.ShaderDeviceAddressBit;}
		
		return flags;
	}
}