	
// X-ray effect
Shader "Custom/Xray"
{

	Properties
	{
		// main sticker
	   _MainTex("Base (RGB)", 2D) = "white" {}

	// main color
   _GhostColor("Ghost Color", Color) = (0, 1, 0, 1)

	   // x-ray intensity adjustment parameters
	  _Pow("Pow Factor", float) = 1
	}

		SubShader
   {
	   // Set to transparent rendering and queue
	  Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
	  LOD 200

	  Zwrite Off
	  Ztest Always

	   // mixed mode
	  Blend SrcAlpha One


	  CGPROGRAM
	  #pragma surface surf Unlit keepalpha

	   // Parameter name
	  sampler2D _MainTex;
	  half4 _GhostColor;
	  float _Pow;

	  struct Input
	  {
		   float3 viewDir; // view direction
		  float2 uv_MainTex;
	  };

	  fixed4 LightingUnlit(SurfaceOutput s, fixed3 lightDir, fixed atten)
	   {
		   fixed4 c;
		   c.rgb = s.Albedo;
		   c.a = s.Alpha;
		   return c;
	   }

	  void surf(Input IN, inout SurfaceOutput o)
	  {
		  half4 c = tex2D(_MainTex, IN.uv_MainTex);

		  // Normal
		 float3 worldNormal = WorldNormalVector(IN, o.Normal);

		 // set color
		o.Albedo = _GhostColor.rgb;

		// set the edge to 1
	   half alpha_XRay = 1.0f - saturate(dot(normalize(IN.viewDir), worldNormal));
	   // Adjust the effect of X-Ray
	  alpha_XRay = pow(alpha_XRay, _Pow);
	  
	  // The first two alphas are basically 1, the key is alpha_XRay
	 o.Alpha = c.a * _GhostColor.a * alpha_XRay;
 }
 ENDCG
   }
}
