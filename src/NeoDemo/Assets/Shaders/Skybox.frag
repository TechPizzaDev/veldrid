#version 450
#extension GL_EXT_nonuniform_qualifier : require

layout(set = 0, binding = 2) uniform sampler CubeSampler;
layout(set = 0, binding = 3) uniform A{
    int a;
}b;
layout(set = 0, binding = 4) uniform textureCube[] CubeTexture;

layout(location = 0) in vec3 fsin_0;
layout(location = 0) out vec4 OutputColor;

void main()
{
    
    OutputColor = texture(samplerCube(CubeTexture[nonuniformEXT(b.a)], CubeSampler), fsin_0);
}
