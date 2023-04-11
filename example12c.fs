#version 330 core

in vec3 normal;
uniform samplerCube envMap;
uniform vec3 light;

int num = 1235;
int a = 141;
int c = 28411;
int m = 134456;

float rand() {
    float f;
    num = (a*num+c) % m;
    f = (num+0.0)/m;
    return((f-0.5)*2.0);
}

void main() {
    vec3 N = normalize(normal);
    vec3 L = normalize(light);
    vec3 R = reflect(-L, N);

    vec3 diffuse = vec3(0.0);
    float weightSum = 0.0;
    const int numSamples = 500;

    for (int i = 0; i < numSamples; i++) {
        vec3 sampleDir = vec3(rand()*2.0-1.0, rand()*2.0-1.0, rand()*2.0-1.0);
        if (dot(sampleDir, N) < 0.0) {
            sampleDir = -sampleDir;
        }
        diffuse += texture(envMap, sampleDir).rgb * max(dot(sampleDir, N), 0.0);
        weightSum += max(dot(sampleDir, N), 0.0);
    }

    diffuse /= weightSum;

    gl_FragColor = vec4(diffuse, 1.0);
}
