using Gfx;
using Silk.NET.Maths;
using Silk.NET.Windowing;

GfxTestApplication app = new ();
app.Run();

internal class GfxTestApplication
{
	private const GraphicsBackend _graphicsBackend = GraphicsBackend.Vulkan;
	private       IWindow         _window;
	public        IWindow         NativeWindow => _window;
	public        IView           View         => _window;

	private Api?           _api;
	private LogicalDevice? _logicalDevice;
	
	public void Run()
	{
		InitWindow();
		_window.Run();

		// this cannot be called from OnClose .. for some reason
		_logicalDevice?.WaitIdle();
		CleanUp();
	}

	private void InitWindow()
	{
		//GraphicsBackend graphicsBackend = GfxApi.GetDefaultBackend();
		
		WindowOptions opts = new()
		                     {
			                     Title                   = "GfxTest",
			                     Position                = new Vector2D<int>(100,  100),
			                     Size                    = new Vector2D<int>(1024, 720),
			                     API                     = _graphicsBackend.ToGraphicsAPI(),
			                     VSync                   = true,
			                     ShouldSwapAutomatically = false,
			                     WindowState             = WindowState.Normal,
			                     WindowBorder            = WindowBorder.Resizable,
			                     IsVisible               = true,
		                     };
        
		_window = Window.Create(opts);
		
		_window.Load    += OnLoad;
		_window.Update  += OnUpdate;
		_window.Render  += OnRender;
		_window.Closing += OnClose;
		_window.Resize  += OnResize;
	}

	private void InitGfx()
	{
		ApiOptions options = new ApiOptions(_graphicsBackend, _window, DebugMessageLog);
		_api = Api.Create(options);
	}

	private void InitGraphicsDevice()
	{
		// physical device
		IReadOnlyList<PhysicalDevice> physicalDevices = _api!.EnumeratePhysicalDevices();

		foreach (PhysicalDevice device in physicalDevices)
		{
			Console.WriteLine($"{device.Name} kind={device.Kind}");
		}
		
		PhysicalDevice? bestPhysicalDevice = physicalDevices.FirstOrDefault(device => device.Kind == PhysicalDeviceKind.DiscreteGpu && device.SupportsGraphics);
		bestPhysicalDevice ??= physicalDevices.FirstOrDefault(device => device.Kind == PhysicalDeviceKind.IntegratedGpu && device.SupportsGraphics);
		bestPhysicalDevice ??= physicalDevices.FirstOrDefault(device => device.Kind == PhysicalDeviceKind.Cpu           && device.SupportsGraphics);

		Console.WriteLine($"Selected physical device: {bestPhysicalDevice?.Name}");
		if (bestPhysicalDevice == null)
		{
			return;
		}

		// logical device
		_logicalDevice = _api!.CreateLogicalDevice(new LogicalDeviceOptions(bestPhysicalDevice));

		//_api.CreateGraphicsDevice(_window, options);
	}

	private void CleanUp()
	{
		_logicalDevice?.Dispose();
		_api?.Dispose();
		_window.Dispose();
	}

	private void DebugMessageLog(DebugMessageSeverity severity, DebugMessageKind kind, string message)
	{
	}

	#region Main callbacks
	private void OnLoad()
	{
		InitGfx();
		InitGraphicsDevice();
	}

	private void OnUpdate(double obj)
	{
	}

	private void OnRender(double obj)
	{
	}

	private void OnClose()
	{
	}

	private void OnResize(Vector2D<int> obj)
	{
	}
	#endregion Main callbacks
}