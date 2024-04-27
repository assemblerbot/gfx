namespace Gfx;

public struct SamplerOptions
{
	public SamplerFlags       SamplerFlags            = SamplerFlags.None;
	public Filter             MagFilter               = Filter.Nearest;
	public Filter             MinFilter               = Filter.Nearest;
	public SamplerMipmapMode  MipmapMode              = SamplerMipmapMode.Nearest;
	public SamplerAddressMode AddressModeU            = SamplerAddressMode.ClampToEdge;
	public SamplerAddressMode AddressModeV            = SamplerAddressMode.ClampToEdge;
	public SamplerAddressMode AddressModeW            = SamplerAddressMode.ClampToEdge;
	public float              MipLodBias              = 0;
	public bool               AnisotropyEnable        = false;
	public float              MaxAnisotropy           = 0;
	public bool               CompareEnable           = false;
	public CompareOp          CompareOp               = CompareOp.Never;
	public float              MinLod                  = 0;
	public float              MaxLod                  = 0;
	public BorderColor        BorderColor             = BorderColor.FloatTransparentBlack;
	public bool               UnnormalizedCoordinates = false;

	public SamplerOptions()
	{
	}
}