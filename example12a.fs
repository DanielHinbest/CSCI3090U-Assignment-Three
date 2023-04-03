/*
 *  Simple fragment sharder for Example 12
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
uniform float refractiveIndex;

void main() {
	vec3 tc;
	vec3 V = normalize(Eye - position);

	tc = refract(V, normal, refractiveIndex);
	gl_FragColor = texture(tex, tc);

}