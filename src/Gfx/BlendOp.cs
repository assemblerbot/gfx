namespace Gfx;

public enum BlendOp
{
	Add                 = 0,
	Subtract            = 1,
	ReverseSubtract     = 2,
	Min                 = 3,
	Max                 = 4,
	ZeroExt             = 1000148000, // 0x3B9D0C20
	SrcExt              = 1000148001, // 0x3B9D0C21
	DstExt              = 1000148002, // 0x3B9D0C22
	SrcOverExt          = 1000148003, // 0x3B9D0C23
	DstOverExt          = 1000148004, // 0x3B9D0C24
	SrcInExt            = 1000148005, // 0x3B9D0C25
	DstInExt            = 1000148006, // 0x3B9D0C26
	SrcOutExt           = 1000148007, // 0x3B9D0C27
	DstOutExt           = 1000148008, // 0x3B9D0C28
	SrcAtopExt          = 1000148009, // 0x3B9D0C29
	DstAtopExt          = 1000148010, // 0x3B9D0C2A
	XorExt              = 1000148011, // 0x3B9D0C2B
	MultiplyExt         = 1000148012, // 0x3B9D0C2C
	ScreenExt           = 1000148013, // 0x3B9D0C2D
	OverlayExt          = 1000148014, // 0x3B9D0C2E
	DarkenExt           = 1000148015, // 0x3B9D0C2F
	LightenExt          = 1000148016, // 0x3B9D0C30
	ColordodgeExt       = 1000148017, // 0x3B9D0C31
	ColorburnExt        = 1000148018, // 0x3B9D0C32
	HardlightExt        = 1000148019, // 0x3B9D0C33
	SoftlightExt        = 1000148020, // 0x3B9D0C34
	DifferenceExt       = 1000148021, // 0x3B9D0C35
	ExclusionExt        = 1000148022, // 0x3B9D0C36
	InvertExt           = 1000148023, // 0x3B9D0C37
	InvertRgbExt        = 1000148024, // 0x3B9D0C38
	LineardodgeExt      = 1000148025, // 0x3B9D0C39
	LinearburnExt       = 1000148026, // 0x3B9D0C3A
	VividlightExt       = 1000148027, // 0x3B9D0C3B
	LinearlightExt      = 1000148028, // 0x3B9D0C3C
	PinlightExt         = 1000148029, // 0x3B9D0C3D
	HardmixExt          = 1000148030, // 0x3B9D0C3E
	HslHueExt           = 1000148031, // 0x3B9D0C3F
	HslSaturationExt    = 1000148032, // 0x3B9D0C40
	HslColorExt         = 1000148033, // 0x3B9D0C41
	HslLuminosityExt    = 1000148034, // 0x3B9D0C42
	PlusExt             = 1000148035, // 0x3B9D0C43
	PlusClampedExt      = 1000148036, // 0x3B9D0C44
	PlusClampedAlphaExt = 1000148037, // 0x3B9D0C45
	PlusDarkerExt       = 1000148038, // 0x3B9D0C46
	MinusExt            = 1000148039, // 0x3B9D0C47
	MinusClampedExt     = 1000148040, // 0x3B9D0C48
	ContrastExt         = 1000148041, // 0x3B9D0C49
	InvertOvgExt        = 1000148042, // 0x3B9D0C4A
	RedExt              = 1000148043, // 0x3B9D0C4B
	GreenExt            = 1000148044, // 0x3B9D0C4C
	BlueExt             = 1000148045, // 0x3B9D0C4D
}