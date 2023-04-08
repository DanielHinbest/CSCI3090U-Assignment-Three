/*
 *  Simple fragment sharder for Example 10
 */

#version 330 core

in vec3 normal;
in vec3 position;
in vec2 tc;

uniform samplerCube tex;
uniform samplerCube irrMap; // the irradiance map
uniform vec4 colour;
uniform vec3 Eye;
uniform vec3 light;
uniform vec4 material;

void main() {
    vec3 N = normalize(normal);
    vec3 L = normalize(light - position);
    vec3 R = reflect(-L, N);
    vec3 irradiance = texture(irrMap, N).rgb; // sample the irradiance map

    float NdotL = max(dot(N, L), 0.0);
    vec3 diffuse = NdotL * irradiance;

    vec3 specular = vec3(0.0);
    if (NdotL > 0.0) {
        specular = pow(max(dot(R, normalize(Eye - position)), 0.0), material.w) * texture(tex, R).rgb;
    }

    gl_FragColor = vec4((diffuse + specular) * colour.rgb, colour.a);
}

