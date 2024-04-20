using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;

namespace Gfx;

public sealed unsafe class VulkanLogicalDevice : LogicalDevice
{
	private const float _queuePriority = 1f; // from every queue family we need just one queue (priority: 0..1)
	
	private readonly VulkanApi            _api;
	internal readonly VulkanPhysicalDevice PhysicalDevice;
    
	internal readonly Device Device;

	private readonly  Queue  _graphicsQueue;
	private readonly  Queue  _presentQueue;
	
	internal VulkanLogicalDevice(VulkanApi api, LogicalDeviceOptions options)
	{
		_api            = api;
		PhysicalDevice = (VulkanPhysicalDevice)options.PhysicalDevice;

		InitDeviceAndQueues(out Device, out _graphicsQueue, out _presentQueue);
	}

	public override void Dispose()
	{
		_api.Vk.DestroyDevice(Device, null);
	}

	public override void WaitIdle()
	{
		_api.Vk.DeviceWaitIdle(Device);
	}

	public override SwapChain CreateSwapChain(SwapChainOptions options)
	{
		return new VulkanSwapChain(_api, PhysicalDevice, this, options);
	}

	public override DeviceMemory AllocateMemory(DeviceMemoryOptions options)
	{
		return new VulkanDeviceMemory(_api, this, options);
	}

	public override DeviceBuffer AllocateBuffer(DeviceBufferOptions options)
	{
		return new VulkanDeviceBuffer(_api, this, options);
	}

	#region Initialization
	private void InitDeviceAndQueues(out Device device, out Queue graphicsQueue, out Queue presentQueue)
	{
		uint[] uniqueQueueFamilies = { PhysicalDevice.GraphicsQueueFamily!.Value, PhysicalDevice.PresentQueueFamily!.Value };
		uniqueQueueFamilies = uniqueQueueFamilies.Distinct().ToArray();

		using var mem              = GlobalMemory.Allocate(uniqueQueueFamilies.Length * sizeof(DeviceQueueCreateInfo));
		var       queueCreateInfos = (DeviceQueueCreateInfo*)Unsafe.AsPointer(ref mem.GetPinnableReference());

		float queuePriority = _queuePriority;
		for (int i = 0; i < uniqueQueueFamilies.Length; i++)
		{
			queueCreateInfos[i] = new()
			                      {
				                      SType            = StructureType.DeviceQueueCreateInfo,
				                      QueueFamilyIndex = uniqueQueueFamilies[i],
				                      QueueCount       = 1,
				                      PQueuePriorities = &queuePriority
			                      };
		}

		PhysicalDeviceFeatures deviceFeatures = new()
		                                        {
			                                        SamplerAnisotropy = true,
		                                        };

		DeviceCreateInfo createInfo = new()
		                              {
			                              SType                = StructureType.DeviceCreateInfo,
			                              QueueCreateInfoCount = (uint)uniqueQueueFamilies.Length,
			                              PQueueCreateInfos    = queueCreateInfos,

			                              PEnabledFeatures = &deviceFeatures,

			                              EnabledExtensionCount   = (uint)VulkanExtensions.GraphicsExtensions.Length,
			                              PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanExtensions.GraphicsExtensions)
		                              };

		if (_api.IsDebugEnabled)
		{
			createInfo.EnabledLayerCount   = (uint)VulkanValidationLayers.DebugValidationLayers.Length;
			createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanValidationLayers.DebugValidationLayers);
		}
		else
		{
			createInfo.EnabledLayerCount = 0;
		}

		if (_api.Vk.CreateDevice(PhysicalDevice.Device, in createInfo, null, out device) != Result.Success)
		{
			throw new GfxException("Failed to create logical device!");
		}

		_api.Vk.GetDeviceQueue(Device, PhysicalDevice.GraphicsQueueFamily!.Value, 0, out graphicsQueue);
		_api.Vk.GetDeviceQueue(Device, PhysicalDevice.PresentQueueFamily!.Value,  0, out presentQueue);

		if (_api.IsDebugEnabled)
		{
			SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
		}

		SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
	}

	#endregion
	
}