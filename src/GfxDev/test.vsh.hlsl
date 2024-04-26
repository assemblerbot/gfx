struct VSInput
{
	layout(location=0) float2 Position : POSITION0;
	layout(location=1) half4  Color    : COLOR0;
};

struct VStoPS
{
	layout(location=0) float4 Position : SV_POSITION;
	layout(location=1) half4 Color : COLOR0;
};

VStoPS main(VSInput input)
{
	VStoPS output;
	output.Position = float4(input.Position, 0 , 1);
	output.Color = input.Color;
	return output;
}
