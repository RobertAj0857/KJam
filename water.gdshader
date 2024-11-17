shader_type canvas_item;

uniform sampler2D gradient: source_color, filter_nearest;
uniform vec2 pan1;
uniform vec2 pan2;
uniform sampler2D tex1: source_color, filter_nearest, repeat_enable;
uniform sampler2D tex2: source_color, filter_nearest, repeat_enable;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec2 size = vec2(256, 176);
	vec2 pixelSize = 1.0 / size;
	float energy = pow(texture(tex1, floor(UV*size)/size+pan1*TIME*pixelSize).r*texture(tex2,floor(UV*size)/size+pan2*TIME*pixelSize).r+0.1,2);
	COLOR = texture(gradient, vec2(energy));
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
