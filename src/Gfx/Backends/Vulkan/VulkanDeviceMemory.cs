using Silk.NET.Vulkan;
using VkDevideMemory = Silk.NET.Vulkan.DeviceMemory;

namespace Gfx;

public sealed unsafe class VulkanDeviceMemory : DeviceMemory
{
	private readonly  VulkanApi                    _api;
	private readonly  VulkanLogicalDevice          _logicalDevice;
	private readonly  ulong                        _size;
	internal readonly VkDevideMemory Memory;
	
	internal VulkanDeviceMemory(VulkanApi api, VulkanLogicalDevice logicalDevice, DeviceMemoryOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;
		_size          = options.Size;

		MemoryAllocateInfo allocateInfo = new()
		                                  {
			                                  SType           = StructureType.MemoryAllocateInfo,
			                                  AllocationSize  = options.Size,
			                                  MemoryTypeIndex = options.MemoryTypeIndex,
		                                  };
		
		fixed (VkDevideMemory* bufferMemoryPtr = &Memory)
		{
			if (_api.Vk.AllocateMemory(_logicalDevice.Device, allocateInfo, null, bufferMemoryPtr) != Result.Success)
			{
				throw new GfxException("Failed to allocate vertex buffer memory!");
			}
		}
	}

	public override void Dispose()
	{
		_api.Vk.FreeMemory(_logicalDevice.Device, Memory, null);
	}

	public override void Write<T>(ulong offset, Span<T> data) where T : struct
	{
		void* memoryData;
		_api.Vk.MapMemory(_logicalDevice.Device, Memory, offset, _size - offset, 0, &memoryData); // hmm not sure about that size
		data.CopyTo(new Span<T>(memoryData, data.Length));
		_api.Vk.UnmapMemory(_logicalDevice.Device, Memory);
	}
}