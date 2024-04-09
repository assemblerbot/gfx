using System.Runtime.InteropServices;
using Silk.NET.Core.Native;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;

namespace Gfx;

public unsafe class VulkanPhysicalDevice : PhysicalDevice
{
	private readonly VulkanApi                      _api;
	private readonly Silk.NET.Vulkan.PhysicalDevice _physicalDevice;

	private         PhysicalDeviceKind _kind = PhysicalDeviceKind.Other;
	public override PhysicalDeviceKind Kind => _kind;

	private         string _name = "";
	public override string Name => _name;

	internal uint? GraphicsQueueFamily { get; private set; }
	internal uint? PresentQueueFamily  { get; private set; }
	internal uint? ComputeQueueFamily  { get; private set; }

	internal VulkanPhysicalDevice(
		VulkanApi api,
		Silk.NET.Vulkan.PhysicalDevice physicalDevice
	)
	{
		_api            = api;
		_physicalDevice = physicalDevice;
		
		InitKind();
		InitName();
		InitQueueFamilies();
	}

	private void InitKind()
	{
		_kind = _api.Vk.GetPhysicalDeviceProperty(_physicalDevice).DeviceType switch
		{
			PhysicalDeviceType.Other         => PhysicalDeviceKind.Other,
			PhysicalDeviceType.IntegratedGpu => PhysicalDeviceKind.IntegratedGpu,
			PhysicalDeviceType.DiscreteGpu   => PhysicalDeviceKind.DiscreteGpu,
			PhysicalDeviceType.VirtualGpu    => PhysicalDeviceKind.VirtualGpu,
			PhysicalDeviceType.Cpu           => PhysicalDeviceKind.Cpu,
			_                                => throw new ArgumentOutOfRangeException()
		};
	}

	private void InitName()
	{
		PhysicalDeviceProperties properties = _api.Vk.GetPhysicalDeviceProperty(_physicalDevice);
		_name = Marshal.PtrToStringAnsi((IntPtr)properties.DeviceName) ?? "unknown";
	}

	private void InitQueueFamilies()
	{
		GraphicsQueueFamily = null;
		PresentQueueFamily  = null;
		ComputeQueueFamily  = null;
		
		{
			uint queueFamilyCount = 0;
			_api.Vk.GetPhysicalDeviceQueueFamilyProperties(_physicalDevice, ref queueFamilyCount, null);

			var queueFamilies = new QueueFamilyProperties[queueFamilyCount];
			fixed (QueueFamilyProperties* queueFamiliesPtr = queueFamilies)
			{
				_api.Vk.GetPhysicalDeviceQueueFamilyProperties(_physicalDevice, ref queueFamilyCount, queueFamiliesPtr);
			}

			for(uint i=0;i <queueFamilies.Length;++i)
			{
				if (queueFamilies[i].QueueFlags.HasFlag(QueueFlags.GraphicsBit))
				{
					GraphicsQueueFamily = i;
				}

				if (queueFamilies[i].QueueFlags.HasFlag(QueueFlags.ComputeBit))
				{
					ComputeQueueFamily = i;
				}
				
				_api.KhrSurface!.GetPhysicalDeviceSurfaceSupport(_physicalDevice, i, _api.Surface, out var presentSupport);
				if (presentSupport)
				{
					PresentQueueFamily = i;
				}
			}
		}
	}
}