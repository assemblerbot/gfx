namespace Gfx;

[Flags]
public enum DeviceHeapKind
{
	None = 0,
	Local = 1 << 0,			// device local heap
	MultiInstance = 1 << 1,	// every physical device instance have separate instance of heap
}