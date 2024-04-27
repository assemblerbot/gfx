namespace Gfx;

public readonly struct VertexInputAttributeDescription
{
	public readonly uint         Location;
	public readonly uint         Binding;
	public readonly DeviceFormat DeviceFormat;
	public readonly uint         Offset;

	public VertexInputAttributeDescription(uint location, uint binding, DeviceFormat deviceFormat, uint offset)
	{
		Location     = location;
		Binding      = binding;
		DeviceFormat = deviceFormat;
		Offset       = offset;
	}
}