namespace Gfx;

public enum SharingMode
{
	Exclusive,	// allow access to the resource by one queue family only
	Concurrent,	// allow access to the resource by multiple queue families
}