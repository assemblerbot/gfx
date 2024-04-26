struct VStoPS
{
	layout(location=0) float4 Position : SV_POSITION;
	layout(location=1) half4 Color : COLOR0;
};

half4 main(VStoPS input) : SV_TARGET
{
	return input.Color;
}
