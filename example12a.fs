/*
 *  Simple fragment sharder for Example 10
 */

#version 330 core

in vec3 normal;
in vec3 position;
in vec2 tc;
uniform samplerCube tex;
uniform vec4 colour;
uniform vec3 Eye;
uniform vec3 light;
uniform vec4 material;

void main() {
    vec3 viewDir = normalize(Eye - position);
    vec3 reflectDir = reflect(viewDir, normalize(normal));
    vec3 refractDir = refract(viewDir, normalize(normal), material.w);

    float reflectFactor = pow(max(dot(reflectDir, viewDir), 0.0), material.z);
    float refractFactor = 1.0 - reflectFactor;

    vec4 reflectColor = texture(tex, reflectDir);
    vec4 refractColor = texture(tex, refractDir);

    vec4 finalColor = reflectColor * reflectFactor + refractColor * refractFactor;
    gl_FragColor = mix(colour, finalColor, material.y);
}
