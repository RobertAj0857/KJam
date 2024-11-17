shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

uniform vec4 color1 : source_color = vec4(1.0);
uniform vec4 color2 : source_color = vec4(1.0);
uniform vec4 color3 : source_color = vec4(1.0);
uniform sampler2D tex1: source_color, filter_nearest, repeat_enable;

void fragment() {
	vec4 color = texture(TEXTURE, UV);
	float t = TIME*3.0;
	float e = texture(tex1, SCREEN_UV).r;
	if(color == vec4(0,0,0,1)) {
		float val1 = 1.0;//max(0.0,floor(mod(t+1.0, 4)-2.0))
		COLOR = color1*val1;
	} else if(color == vec4(1,0,0,1)) {
		float val2 = max(0.0,floor(mod(t+1.0, 4)-2.0));//+ max(0.0,floor(mod(t+3.0, 4)-2.0));
		COLOR = color2*val2;
	} else if(color == vec4(0,1,0,1)) {
		float val1 = max(0.0,floor(mod(t+0.0, 4)-2.0));
		float val2 = max(0.0,floor(mod(t+3.0, 4)-2.0));
		COLOR = color3*val2+color2*val1;
	} else {
		COLOR = color;
	}
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
