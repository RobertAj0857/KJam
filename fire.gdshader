shader_type canvas_item;

uniform vec2 offset;
uniform vec4 particles[1000];
uniform int pLen;
uniform sampler2D sdfGradient: source_color, filter_nearest;
uniform sampler2D color: source_color, filter_nearest;
uniform sampler2D distortion: source_color, filter_nearest;
uniform sampler2D tex1: source_color, filter_nearest, repeat_enable;
uniform vec2 pan1;
uniform sampler2D tex2: source_color, filter_nearest, repeat_enable; 
uniform vec2 pan2;
uniform float energyMax = 2;

void fragment() {
	vec4 pixelColor = texture(TEXTURE, UV);
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pixelSize = 1.0 / size;
	float e = 0.0;
	vec2 coord = floor(UV * size);
	float e1 = 0.0;
	for(int i = 0; i < pLen; i++) {
		float z0 = step(0.1, particles[i].z);
		float x = (particles[i].z-length(coord - particles[i].xy)) / (particles[i].z + 1.0 - z0) * z0; 
		x = step(0.0, x) * texture(sdfGradient, vec2(x,x)).r * particles[i].a;
		e1 = max(x, e1);
		e += x;
	}
	e = e1 + min((e - e1) / 2.0, energyMax);
	pixelColor.a += e;
	vec4 n1 = texture(tex1, UV + floor(pan1*TIME*size)/size + offset * pixelSize);
	vec4 n2 = texture(tex2, UV + floor(pan2*TIME*size)/size + offset * pixelSize);
	float a1 = step(0.1, pixelColor.a);
	float a2 = step(0.6, pixelColor.a);
	vec4 energy = n2*n1;//*n1*2.0;
	float a = (energy.r + energy.g + energy.b) / 2.0 * pixelColor.a;// + pixelColor.a/3.0;
	float a3 = step(0.6, a);
	vec4 gColor = texture(color, vec2(a,a));
	//float distort = texture(distortion, vec2(a, a)).r - 0.5;
	COLOR = vec4(gColor.r, gColor.g, gColor.b, gColor.a);// + */texture(screenTexture, SCREEN_UV+floor((distort * 0.1)*size)/size);// * (1.0-step(0.001, gColor.a));
	//COLOR = vec4(1, 1, 1, e);
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
