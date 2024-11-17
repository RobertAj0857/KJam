shader_type canvas_item;
uniform int inWater;
uniform float quantity : hint_range(0.0, 1.0);

void fragment() {
	vec2 size = vec2(textureSize(TEXTURE, 0));
	vec2 pixelSize = 1.0 / size;
	vec4 base = texture(TEXTURE,UV);
	vec4 new_color = vec4(1.0,1.0,1.0, base.a);
	//float m = (step(UV.y,0.5));
	COLOR = mix(base, new_color, quantity);//*m;
	// Called for every pixel the material is visible on.
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}
