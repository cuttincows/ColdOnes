// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Pulse"
{
	// Jessica Thomson, 2017
	// Cubemap from https://opengameart.org/content/mountain-skyboxes
	Properties 
	{
		_Glossiness ("Smoothness", Range(0,1)) = 0.5

		_Frequency("Frequency", float) = 0.5
		_HalfWidth ("Half Width", float) = 0.05

		_ObjWidth ("Object Width", float) = 6.5

		_Intensity ("Transpanency Intensity", Range(0.5, 10)) = 5

		_Cube("Reflection Map", Cube) = "" {}

		_GradientFrom("Gradient From", Color) = (1, 0, 0, 1)
		_GradientTo("Gradient To", Color)     = (0, 1, 0, 1)

		_GradientIntensity("Gradient Intensity", Range(0, 1)) = 0.2

	}
	SubShader 
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// PBR shader, with alpha and shadows
		#pragma surface surf Standard fullforwardshadows alpha:fade
		#pragma target 3.0

		#include "UnityCG.cginc" // for WorldSpaceViewDir

		// UNIFORMS
		uniform samplerCUBE _Cube;
		uniform sampler2D _MainTex, _StripeGradient;

		uniform half _Glossiness;

		uniform float _Frequency, _HalfWidth, _ObjWidth, _Intensity, _GradientIntensity;

		uniform fixed4 _GradientFrom, _GradientTo;


		struct Input 
		{
			// Used for the UV of the gradient
			float2 uv_MainTex;

			// Used for calculating the stripe
			float3 worldPos;

			// Used for reflections
			float3 viewDir;
			float3 norm;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			// Copies the normal to make it usable by the surface shader
			o.norm = v.normal;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// Finds the location of the fragment in the world
			float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

			// Calculate sreflections from cubemap
			float refractiveIndex = 1.5;
			float3 refractedDir = refract(normalize(IN.viewDir),
				   normalize(IN.norm), 1.0 / refractiveIndex);
			fixed4 col = texCUBE(_Cube, refractedDir);

			// Set color directly to reflection, blended with gradient
			o.Albedo = lerp(col.rgb, (lerp(_GradientFrom, _GradientTo, localPos.y * 0.5 + 0.5)), _GradientIntensity);


			// Scales time to be usable, and modifies by frequency
			float normTime = fmod(_Time * 60.0 * _Frequency, _ObjWidth) - (_ObjWidth * 0.5);

			// Finds how far away the current surface fragment is from the center of the ring
			float min = normTime - _HalfWidth, max = normTime + _HalfWidth;
			float between = localPos.x > min && localPos.x < max;
			float diff = clamp(abs(normTime - localPos.x), 0, _HalfWidth);

			// Clamps to usuable levels
			o.Alpha = 1 - clamp(diff * _Intensity, 0, 1);

			o.Smoothness = _Glossiness;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
