using Silk.NET.Windowing;

namespace Gfx;

public struct ApiOptions
{
	public GraphicsBackend Backend;
	public IWindow         Window;
	
	public LogMessage? DebugMessageLog; // if null, no validation layers will be enabled

	public ApiOptions(
		GraphicsBackend backend,
		IWindow         window,
		
		LogMessage? debugMessageLog = null
	)
	{
		Backend         = backend;
		Window          = window;
		DebugMessageLog = debugMessageLog;
	}
}