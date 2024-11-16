shader_type canvas_item;

uniform vec3 particles[1000];
uniform int pLen;

uniform sampler2D tex1: source_color, filter_nearest, repeat_enable;
uniform sampler2D gradient: source_color, filter_nearest;
uniform vec2 pan;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 coord = floor(UV * size);
	vec2 pixelSize = 1.0 / size;
	float e1 = 0.0;
	for(int i = 0; i < pLen; i++) {
		float z0 = step(0.1, particles[i].z);
		float x = (particles[i].z-length(coord - particles[i].xy)) / (particles[i].z + 1.0 - z0) * z0; 
		e1 = max(x, e1);
	}
	float energy = texture(tex1, floor(UV*size)/size + floor(pan * pixelSize * TIME * size)/size).r * e1;
	vec4 color = texture(gradient, vec2(energy, energy));
	//vec4 color = vec4(energy, energy, energy, 1);
	//vec4 color = texture(tex1, floor(UV*size)/size);
	COLOR = color;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
