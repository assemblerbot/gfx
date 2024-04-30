using System.Runtime.InteropServices;
using Silk.NET.Core;
using Silk.NET.Core.Native;
using Silk.NET.Windowing;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;

namespace Gfx;

//using GfxPhysicalDevice = Gfx.PhysicalDevice;

public sealed unsafe class VulkanApi : Api
{
	internal readonly IWindow  Window;
	internal readonly Vk       Vk;
	internal readonly Instance Instance;

	private          ExtDebugUtils?         _debugUtils;
	private          DebugUtilsMessengerEXT _debugMessenger;
	private readonly LogMessage?            _debugMessageLog;
	internal         bool                   IsDebugEnabled => _debugMessageLog != null;

	internal KhrSurface? KhrSurface;
	internal SurfaceKHR  Surface;
	
	#region Lifecycle
	internal VulkanApi(
		IWindow                                                 window,
		LogMessage? debugMessageLog
	)
	{
		Window          = window;
		_debugMessageLog = debugMessageLog;
		Vk              = Vk.GetApi();

		CreateInstance(out Instance);
		SetupDebugMessenger();
		CreateSurface();
	}

	public override void Dispose()
	{
		if (IsDebugEnabled)
		{
			_debugUtils!.DestroyDebugUtilsMessenger(Instance, _debugMessenger, null);
		}

		KhrSurface?.DestroySurface(Instance, Surface, null);
		Vk.DestroyInstance(Instance, null);
		Vk.Dispose();
	}
	#endregion Lifecycle

	#region Base overrides
	public override IReadOnlyList<PhysicalDevice> EnumeratePhysicalDevices()
	{
		IReadOnlyCollection<Silk.NET.Vulkan.PhysicalDevice> devices = Vk.GetPhysicalDevices(Instance);
		return devices.Select<Silk.NET.Vulkan.PhysicalDevice, VulkanPhysicalDevice>(device => new VulkanPhysicalDevice(this, device)).ToList();
	}

	public override LogicalDevice CreateLogicalDevice(LogicalDeviceOptions options)
	{
		return new VulkanLogicalDevice(this, options);
	}

	#endregion Base overrides

	#region Private
	private void CreateInstance(out Instance instance)
	{
		if (IsDebugEnabled && !ValidationLayersSupported(VulkanValidationLayers.DebugValidationLayers))
		{
			throw new GfxException("Validation layers are not supported!");
		}

		ApplicationInfo appInfo = new()
		                          {
			                          SType              = StructureType.ApplicationInfo,
			                          PApplicationName   = (byte*)Marshal.StringToHGlobalAnsi(Window.Title),
			                          ApplicationVersion = new Version32(1, 0, 0),
			                          PEngineName        = (byte*)Marshal.StringToHGlobalAnsi("Gfx"),
			                          EngineVersion      = new Version32(1,                                      0,                                      0),
			                          ApiVersion         = new Version32((uint)Window.API.Version.MajorVersion, (uint)Window.API.Version.MinorVersion, 0U)
		                          };

		InstanceCreateInfo createInfo = new()
		                                {
			                                SType            = StructureType.InstanceCreateInfo,
			                                PApplicationInfo = &appInfo,
			                                Flags = RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
				                                ? InstanceCreateFlags.EnumeratePortabilityBitKhr
				                                : InstanceCreateFlags.None,
		                                };

		string[] extensions = GetRequiredExtensions();
		createInfo.EnabledExtensionCount   = (uint)extensions.Length;
		createInfo.PpEnabledExtensionNames = (byte**)SilkMarshal.StringArrayToPtr(extensions); ;
		
		if (IsDebugEnabled)
		{
			createInfo.EnabledLayerCount   = (uint)VulkanValidationLayers.DebugValidationLayers.Length;
			createInfo.PpEnabledLayerNames = (byte**)SilkMarshal.StringArrayToPtr(VulkanValidationLayers.DebugValidationLayers);

			DebugUtilsMessengerCreateInfoEXT debugCreateInfo = new();
			PopulateDebugMessengerCreateInfo(ref debugCreateInfo);
			createInfo.PNext = &debugCreateInfo;
		}
		else
		{
			createInfo.EnabledLayerCount = 0;
			createInfo.PNext             = null;
		}

		if (Vk.CreateInstance(createInfo, null, out instance) != Result.Success)
		{
			throw new GfxException("Failed to create Vulkan instance!");
		}

		Marshal.FreeHGlobal((IntPtr)appInfo.PApplicationName);
		Marshal.FreeHGlobal((IntPtr)appInfo.PEngineName);
		SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);

		if (IsDebugEnabled)
		{
			SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
		}
	}

	private void SetupDebugMessenger()
	{
		if (!IsDebugEnabled)
		{
			return;
		}

		if (!Vk.TryGetInstanceExtension(Instance, out _debugUtils))
		{
			return;
		}

		DebugUtilsMessengerCreateInfoEXT createInfo = new();
		PopulateDebugMessengerCreateInfo(ref createInfo);

		if (_debugUtils!.CreateDebugUtilsMessenger(Instance, in createInfo, null, out _debugMessenger) != Result.Success)
		{
			throw new GfxException("Failed to set up debug messenger!");
		}
	}

	private void CreateSurface()
	{
		if (!Vk.TryGetInstanceExtension<KhrSurface>(Instance, out KhrSurface))
		{
			throw new GfxException("KHR_surface extension not found.");
		}

		Surface = Window.VkSurface!.Create<AllocationCallbacks>(Instance.ToHandle(), null).ToSurface();
	}

	private bool ValidationLayersSupported(string[] validationLayers)
	{
		uint layerCount = 0;
		Vk.EnumerateInstanceLayerProperties(ref layerCount, null);
		var availableLayers = new LayerProperties[layerCount];
		fixed (LayerProperties* availableLayersPtr = availableLayers)
		{
			Vk.EnumerateInstanceLayerProperties(ref layerCount, availableLayersPtr);
		}

		HashSet<string?> availableLayerNames = availableLayers.Select(layer => { return Marshal.PtrToStringAnsi((IntPtr) layer.LayerName); }).ToHashSet();

		return validationLayers.All(availableLayerNames.Contains);
	}

	private string[] GetRequiredExtensions()
	{
		byte**    windowExtensions = Window!.VkSurface!.GetRequiredExtensions(out uint windowExtensionCount);
		string[]? extensions       = SilkMarshal.PtrToStringArray((nint)windowExtensions, (int)windowExtensionCount);

		if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
		{
			extensions = extensions.Append("VK_KHR_portability_enumeration").ToArray();
		}

		if (IsDebugEnabled)
		{
			extensions = extensions.Append(ExtDebugUtils.ExtensionName).ToArray();
		}
		
		return extensions;
	}
	
	private void PopulateDebugMessengerCreateInfo(ref DebugUtilsMessengerCreateInfoEXT createInfo)
	{
		createInfo.SType = StructureType.DebugUtilsMessengerCreateInfoExt;
		createInfo.MessageSeverity = DebugUtilsMessageSeverityFlagsEXT.VerboseBitExt |
		                             DebugUtilsMessageSeverityFlagsEXT.WarningBitExt |
		                             DebugUtilsMessageSeverityFlagsEXT.ErrorBitExt;
		createInfo.MessageType = DebugUtilsMessageTypeFlagsEXT.GeneralBitExt     |
		                         DebugUtilsMessageTypeFlagsEXT.PerformanceBitExt |
		                         DebugUtilsMessageTypeFlagsEXT.ValidationBitExt;
		createInfo.PfnUserCallback = (DebugUtilsMessengerCallbackFunctionEXT)DebugCallback;
	}
	
	private uint DebugCallback(DebugUtilsMessageSeverityFlagsEXT messageSeverity, DebugUtilsMessageTypeFlagsEXT messageTypes, DebugUtilsMessengerCallbackDataEXT* pCallbackData, void* pUserData)
	{
		_debugMessageLog!.Invoke(messageSeverity.ToGfx(), messageTypes.ToGfx(), Marshal.PtrToStringAnsi((nint) pCallbackData->PMessage) ?? ""); 
		return Vk.False;
	}

	#endregion
}