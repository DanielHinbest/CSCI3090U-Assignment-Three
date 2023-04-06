/*
 *  Simple fragment sharder for Lab 2
 */

#version 330 core

in vec3 position;
in vec3 normal;

uniform samplerCube reflectionMap;
uniform samplerCube refractionMap;
uniform float refractionIndex;

out vec4 outColor;

void main() {
    vec3 V = normalize(position);
    vec3 R = reflect(V, normal);
    vec3 T = refract(V, normal, refractionIndex);

    // Calculate a factor based on the angle between the view vector and the surface normal
    float factor = pow(1.0 - abs(dot(normalize(position), normalize(normal))), 5.0);

    // Calculate the reflected and refracted colors
    vec4 reflectionColor = texture(reflectionMap, R);
    vec4 refractionColor = texture(refractionMap, T);

    // Mix the colors based on the angle factor
    vec4 color = mix(reflectionColor, refractionColor, factor);

    // Set the alpha value of the color based on the angle factor
    color.a = factor;

    outColor = color;
}
