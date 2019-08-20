Shader "Code Cyber/poleshader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color Tine", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags {"RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" "DisableBatching" = "True"}

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}

            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct a2v {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

            v2f vert (appdata_base v)
            {
                v2f o;
         
				o.pos = UnityObjectToClipPos(v.vertex);

				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);

				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                c.rgb *= _Color.rgb;
                return c;
            }
            ENDCG
        
        }
        pass
        {
            Tags { "LightMode" = "ShadowCaster" }
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            struct v2f 
            {
                float2 uv : TEXCOORD0;
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;

                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)

                return o;
            }

            fixed4 frag(v2f i) : SV_Target 
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                clip (tex.a - 0.3);
			    SHADOW_CASTER_FRAGMENT(i)
			}
            ENDCG
        }
    }
    Fallback "Transparent/VertexLit"
}
