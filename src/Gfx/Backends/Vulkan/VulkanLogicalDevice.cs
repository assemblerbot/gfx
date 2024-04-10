using System.Runtime.CompilerServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;

namespace Gfx;

// TODO - this is just for graphical device, not for compute
public unsafe class VulkanLogicalDevice : LogicalDevice
{
    private const    float     _queuePriority = 1f; // from every queue family we need just one queue (priority: 0..1)
	private readonly VulkanApi _api;
    private readonly Device    _device;
    private readonly Queue     _graphicsQueue;
    private readonly Queue     _presentQueue;
	
	internal VulkanLogicalDevice(VulkanApi api, LogicalDeviceOptions options)
	{
		_api = api;

        VulkanPhysicalDevice physicalDevice = (VulkanPhysicalDevice)options.PhysicalDevice;
		
        uint[] uniqueQueueFamilies = { physicalDevice.GraphicsQueueFamily!.Value, physicalDevice.PresentQueueFamily!.Value };
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

        if (_api.Vk.CreateDevice(physicalDevice.Device, in createInfo, null, out _device) != Result.Success)
        {
            throw new Exception("failed to create logical device!");
        }

        _api.Vk.GetDeviceQueue(_device, physicalDevice.GraphicsQueueFamily!.Value, 0, out _graphicsQueue);
        _api.Vk.GetDeviceQueue(_device, physicalDevice.PresentQueueFamily!.Value,  0, out _presentQueue);

        if (_api.IsDebugEnabled)
        {
            SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
        }

        SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
	}

    public override void Dispose()
    {
        _api.Vk.DestroyDevice(_device, null);
    }

    public override void WaitIdle()
    {
        _api.Vk.DeviceWaitIdle(_device);
    }
}