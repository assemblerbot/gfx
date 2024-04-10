using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;

namespace Gfx;

// TODO - this is just for graphical device, not for compute
public unsafe class VulkanLogicalDevice : LogicalDevice
{
    private const    float                _queuePriority = 1f; // from every queue family we need just one queue (priority: 0..1)
	private readonly VulkanApi            _api;
    private readonly VulkanPhysicalDevice _physicalDevice;
    private readonly Device               _device;
    private readonly Queue                _graphicsQueue;
    private readonly Queue                _presentQueue;
	
	internal VulkanLogicalDevice(VulkanApi api, LogicalDeviceOptions options)
	{
		_api            = api;
        _physicalDevice = (VulkanPhysicalDevice)options.PhysicalDevice;

        InitDeviceAndQueues(out _device, out _graphicsQueue, out _presentQueue);
	}

	public override void Dispose()
    {
        _api.Vk.DestroyDevice(_device, null);
    }

    public override void WaitIdle()
    {
        _api.Vk.DeviceWaitIdle(_device);
    }
    
    #region Initialization
	private void InitDeviceAndQueues(out Device device, out Queue graphicsQueue, out Queue presentQueue)
	{
        uint[] uniqueQueueFamilies = { _physicalDevice.GraphicsQueueFamily!.Value, _physicalDevice.PresentQueueFamily!.Value };
        uniqueQueueFamilies = uniqueQueueFamilies.Distinct().ToArray();

        using var mem = GlobalMemory.Allocate(uniqueQueueFamilies.Length * sizeof(DeviceQueueCreateInfo));
        var queueCreateInfos = (DeviceQueueCreateInfo*)Unsafe.AsPointer(ref mem.GetPinnableReference());

        float queuePriority = _queuePriority;
        for (int i = 0; i < uniqueQueueFamilies.Length; i++)
        {
            queueCreateInfos[i] = new()
            {
                SType = StructureType.DeviceQueueCreateInfo,
                QueueFamilyIndex = uniqueQueueFamilies[i],
                QueueCount = 1,
                PQueuePriorities = &queuePriority
            };
        }

        PhysicalDeviceFeatures deviceFeatures = new()
        {
            SamplerAnisotropy = true,
        };

        DeviceCreateInfo createInfo = new()
        {
            SType = StructureType.DeviceCreateInfo,
            QueueCreateInfoCount = (uint)uniqueQueueFamilies.Length,
            PQueueCreateInfos = queueCreateInfos,

            PEnabledFeatures = &deviceFeatures,

            EnabledExtensionCount = (uint)VulkanExtensions.GraphicsExtensions.Length,
            PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanExtensions.GraphicsExtensions)
        };

        if (_api.IsDebugEnabled)
        {
            createInfo.EnabledLayerCount = (uint)VulkanValidationLayers.DebugValidationLayers.Length;
            createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanValidationLayers.DebugValidationLayers);
        }
        else
        {
            createInfo.EnabledLayerCount = 0;
        }

        if (_api.Vk.CreateDevice(_physicalDevice.Device, in createInfo, null, out device) != Result.Success)
        {
            throw new GfxException("Failed to create logical device!");
        }

        _api.Vk.GetDeviceQueue(_device, _physicalDevice.GraphicsQueueFamily!.Value, 0, out graphicsQueue);
        _api.Vk.GetDeviceQueue(_device, _physicalDevice.PresentQueueFamily!.Value,  0, out presentQueue);

        if (_api.IsDebugEnabled)
        {
            SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
        }

        SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
	}
    #endregion
}