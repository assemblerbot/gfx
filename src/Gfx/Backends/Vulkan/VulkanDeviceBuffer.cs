using Silk.NET.Vulkan;
using Buffer = Silk.NET.Vulkan.Buffer;

namespace Gfx;

public sealed unsafe class VulkanDeviceBuffer : DeviceBuffer
{
	private readonly VulkanApi           _api;
	private readonly VulkanLogicalDevice _logicalDevice;
	public readonly Buffer              Buffer;
	
	internal VulkanDeviceBuffer(VulkanApi api, VulkanLogicalDevice logicalDevice, DeviceBufferOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;
		
		BufferCreateInfo bufferInfo = new()
		                              {
			                              SType       = StructureType.BufferCreateInfo,
			                              Size        = options.Size,
			                              Usage       = options.Usage.ToVulkan(),
			                              SharingMode = options.SharingMode.ToVulkan(),
		                              };

		fixed (Buffer* bufferPtr = &Buffer)
		{
			if (_api.Vk.CreateBuffer(_logicalDevice.Device, bufferInfo, null, bufferPtr) != Result.Success)
			{
				throw new GfxException("Failed to create vertex buffer!");
			}
		}
	}

	public override void Dispose()
	{
		_api.Vk.DestroyBuffer(_logicalDevice.Device, Buffer, null);
	}

	public override bool GetMemoryRequirements(DeviceMemoryProperties requiredProperties, out uint memoryIndex, out ulong alignment, out ulong size)
	{
		MemoryRequirements memRequirements = new();
		_api.Vk.GetBufferMemoryRequirements(_logicalDevice.Device, Buffer, out memRequirements);

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

	public override bool GetMemoryRequirements(uint suggestedMemoryIndex, out ulong alignment, out ulong size)
	{
		MemoryRequirements memRequirements = new();
		_api.Vk.GetBufferMemoryRequirements(_logicalDevice.Device, Buffer, out memRequirements);

		if ((memRequirements.MemoryTypeBits & (1 << (int)suggestedMemoryIndex)) == 0)
		{
			alignment = 0;
			size      = 0;
			return false;
		}

		alignment = memRequirements.Alignment;
		size      = memRequirements.Size;
		return true;
	}

	public override void BindToMemory(DeviceMemory memory, ulong memoryOffset)
	{
		_api.Vk.BindBufferMemory(_logicalDevice.Device, Buffer, ((VulkanDeviceMemory)memory).Memory, memoryOffset);
	}
}