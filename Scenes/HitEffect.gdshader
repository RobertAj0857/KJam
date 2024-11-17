shader_type canvas_item;
uniform float inWater;
uniform sampler2D waterTex: source_color, filter_nearest, repeat_enable;
uniform float quantity : hint_range(0.0, 1.0);

void fragment() {
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pixelSize = 1.0 / size;
	vec4 base = texture(TEXTURE,UV);
	vec4 new_color = vec4(1.0,1.0,1.0, base.a);
	float m = base.a*max(1.0-inWater, texture(waterTex, UV).a);
	vec4 c =  mix(base, new_color, quantity);
	if(inWater != 0.0 && m>0.0 && texture(waterTex, UV)==vec4(1)) {
		COLOR = vec4(1);
	} else {
		COLOR = vec4(c.x,c.y,c.z,m);
	}
	// Called for every pixel the material is visible on.
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
