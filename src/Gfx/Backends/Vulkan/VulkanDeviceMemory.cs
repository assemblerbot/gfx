using Silk.NET.Vulkan;

namespace Gfx;

public unsafe class VulkanDeviceMemory : DeviceMemory
{
	private readonly VulkanApi                    _api;
	private readonly VulkanLogicalDevice          _logicalDevice;
	private          Silk.NET.Vulkan.DeviceMemory _memory;
	
	internal VulkanDeviceMemory(VulkanApi api, VulkanLogicalDevice logicalDevice, DeviceMemoryOptions options)
	{
		_api           = api;
		_logicalDevice = logicalDevice;

		MemoryAllocateInfo allocateInfo = new()
		                                  {
			                                  SType           = StructureType.MemoryAllocateInfo,
			                                  AllocationSize  = options.Size,
			                                  MemoryTypeIndex = options.MemoryTypeIndex,
		                                  };
		
		fixed (Silk.NET.Vulkan.DeviceMemory* bufferMemoryPtr = &_memory)
		{
			if (_api.Vk.AllocateMemory(_logicalDevice.Device, allocateInfo, null, bufferMemoryPtr) != Result.Success)
			{
				throw new GfxException("Failed to allocate vertex buffer memory!");
			}
		}
	}

	public override void Dispose()
	{
		_api.Vk.FreeMemory(_logicalDevice.Device, _memory, null);
	}
}