shader_type canvas_item;

uniform float iValue;
uniform sampler2D gradient: source_color, filter_nearest;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pos = round(UV * size)/size;
	float energy = texture(TEXTURE, UV).r;
	COLOR = texture(gradient, vec2(energy));
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
