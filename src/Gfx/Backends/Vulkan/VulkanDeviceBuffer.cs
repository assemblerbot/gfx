using Silk.NET.Vulkan;
using Buffer = Silk.NET.Vulkan.Buffer;

namespace Gfx;

public sealed unsafe class VulkanDeviceBuffer : DeviceBuffer
{
	private readonly VulkanApi              _api;
	private readonly VulkanLogicalDevice    _logicalDevice;
	private          Buffer                 _buffer;
	
	internal VulkanDeviceBuffer(VulkanApi api, VulkanLogicalDevice logicalDevice, DeviceBufferOptions options)
	{
		_api                      = api;
		_logicalDevice            = logicalDevice;
		
		BufferCreateInfo bufferInfo = new()
		                              {
			                              SType       = StructureType.BufferCreateInfo,
			                              Size        = options.Size,
			                              Usage       = options.Usage.ToVulkanBufferUsageFlags(),
			                              SharingMode = options.SharingMode.ToVulkanSharingMode(),
		                              };

		fixed (Buffer* bufferPtr = &_buffer)
		{
			if (_api.Vk.CreateBuffer(_logicalDevice.Device, bufferInfo, null, bufferPtr) != Result.Success)
			{
				throw new GfxException("Failed to create vertex buffer!");
			}
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyBuffer(_logicalDevice.Device, _buffer, null);
	}

	public override bool GetMemoryRequirements(DeviceMemoryProperties requiredProperties, out uint memoryIndex, out ulong alignment, out ulong size)
	{
		MemoryRequirements memRequirements = new();
		_api.Vk.GetBufferMemoryRequirements(_logicalDevice.Device, _buffer, out memRequirements);

		uint index = _logicalDevice.PhysicalDevice.FindMemoryIndex(memRequirements.MemoryTypeBits, requiredProperties.ToMemoryPropertyFlags());
		if (index == uint.MaxValue)
		{
			memoryIndex = 0;
			alignment   = 0;
			size        = 0;
			return false;
		}

		memoryIndex = index;
		alignment   = memRequirements.Alignment;
		size        = memRequirements.Size;
		return true;
	}
}