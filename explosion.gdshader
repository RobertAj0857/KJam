shader_type canvas_item;

uniform float iValue;
uniform sampler2D grayGradient: source_color, filter_nearest;
uniform sampler2D gradient: source_color, filter_nearest;
uniform sampler2D tex1: source_color, filter_nearest, repeat_enable;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pos = round(UV * size)/size;
	float energy = texture(TEXTURE, UV).r;
	float i = iValue;//(TIME, 1.0);
	float outer = i*texture(tex1, UV).r;
	float inner = pow(3.0*i, 3.0)*texture(tex1, -UV).r;
	float interpolation = texture(grayGradient, vec2(0.9-energy*outer-energy*inner)).r;
	COLOR = texture(gradient, vec2(1.0-interpolation));
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
