/*
 *  Fragment shader for Lab 2 with reflections and refractions
 */

#version 330 core

in vec3 normal;
in vec3 position;
uniform samplerCube envMap;
uniform vec3 Eye;

const float refractiveIndex = 1.52; // Refractive index of the material

float schlick(float cosTheta, float refractiveIndex) {
    float r0 = pow((1.0 - refractiveIndex) / (1.0 + refractiveIndex), 2.0);
    return r0 + (1.0 - r0) * pow(1.0 - cosTheta, 5.0);
}

void main() {
    vec3 V = -normalize(Eye - position);
    
    // Compute the reflection vector and lookup the reflected color in the environment map
    vec3 R = reflect(V, normal);
    vec4 reflectedColor = texture(envMap, vec3(-R.x, R.y, -R.z)); // flip y-coordinate and rotate by 180 degrees around the z-axis
    
    // Compute the refraction vector and lookup the refracted color in the environment map
    vec3 refractedVec = refract(V, normal, refractiveIndex);
    vec4 refractedColor = texture(envMap, vec3(-refractedVec.x, refractedVec.y, -refractedVec.z)); // flip y-coordinate and rotate by 180 degrees around the z-axis
    
    // Combine the reflected and refracted colors using the Schlick approximation
    float cosTheta = dot(V, normal);
    float reflectance = schlick(cosTheta, refractiveIndex);
    vec4 finalColor = mix(refractedColor, reflectedColor, reflectance);
    
    gl_FragColor = finalColor;
}
