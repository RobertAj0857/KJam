shader_type canvas_item;

uniform float speed;
uniform sampler2D tex1: source_color, filter_nearest, repeat_enable;
uniform sampler2D gradient: source_color, filter_nearest;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pos = round(UV * size)/size;
	float energy = texture(TEXTURE, pos).r * 2.0 * pow(texture(tex1, pos+floor((vec2(1,0)*TIME*speed)*size)/size).r, 2.4) * (0.3+min(pos.x, 22.0));
	COLOR = texture(gradient, vec2(energy, energy));
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
