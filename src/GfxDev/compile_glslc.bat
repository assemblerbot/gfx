::dxc -spirv -T vs_6_0 -Fo test.vsh.spirv test.vsh
::dxc -spirv -T ps_6_0 -Fo test.psh.spirv test.psh

d:\Tmp\shaderc\install\bin\glslc -fpreserve-bindings -fentry-point=main -fshader-stage=vertex -o test.vsh.spirv test.vsh.hlsl
d:\Tmp\shaderc\install\bin\glslc -fpreserve-bindings -fentry-point=main -fshader-stage=fragment -o test.psh.spirv test.psh.hlsl

::d:\Tmp\shaderc\install\bin\glslc -fpreserve-bindings -fentry-point=main -fshader-stage=vertex -S -o test.vsh.spirvs test.vsh.hlsl

