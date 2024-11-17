shader_type canvas_item;

uniform float iValue = 0;
uniform sampler2D skull: source_color, filter_nearest, repeat_enable;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec4 color1 = texture(TEXTURE,UV);
	vec4 color2 = texture(skull,UV);
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pixelSize = 1.0 / size;
	vec2 pos = round(UV * size)/size;
	float i = floor(iValue*size.y)/size.y;
	COLOR = step(i, UV.y)*color1 + step(UV.y, i)*color2;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
