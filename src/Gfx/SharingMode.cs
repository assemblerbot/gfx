namespace Gfx;

public enum SharingMode
{
	Excludive,	// allow access to the resource by one queue family only
	Concurent,	// allow access to the readouse by multiple queue families
}