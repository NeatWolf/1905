Shader "Code Cyber/bontanyshader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color Tine", Color) = (1, 1, 1, 1)
        _VerticalBillboarding ("Vertical Restraints - 倾斜限制", range(0, 1)) = 1
        _Magnitude ("Distortion Magnitude - 震级", float) = 1
        _Frequecy ("Distortion Frequecy - 振幅", float) = 1
        _InvWaveLength ("Distortion InvWaveLength - 波长", float) = 10
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
            float _VerticalBillboarding;
            float _Magnitude;
            float _Frequecy;
            float _InvWaveLength;

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
				
                float4 offset;
                offset.yzw = float3(0, 0, 0);
                offset.x = sin(_Frequecy * _Time.y + v.vertex.x * _InvWaveLength + v.vertex.y * _InvWaveLength + v.vertex.z * _InvWaveLength) * _Magnitude;

				float3 center = float3(0, 0, 0);
				float3 viewer = mul(unity_WorldToObject,float4(_WorldSpaceCameraPos, 1));
				
				float3 normalDir = viewer - center;
				normalDir.y =normalDir.y * _VerticalBillboarding;
				normalDir = normalize(normalDir);
                
				float3 upDir = abs(normalDir.y) > 0.999 ? float3(0, 0, 1) : float3(0, 1, 0);
				float3 rightDir = normalize(cross(upDir, normalDir));
				upDir = normalize(cross(normalDir, rightDir));
				
				float3 centerOffs = v.vertex.xyz - center;
				float3 localPos = center + rightDir * centerOffs.x + upDir * centerOffs.y + normalDir * centerOffs.z;
              
				o.pos = UnityObjectToClipPos(float4(localPos, 1) + offset);

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
            float _Magnitude;
            float _InvWaveLength;
            float _Frequecy;

            struct v2f 
            {
                float2 uv : TEXCOORD0;
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;

                float4 offset;

                offset.yzw = float3(0, 0, 0);
                offset.x = sin(_Frequecy * _Time.y + v.vertex.x * _InvWaveLength + v.vertex.y * _InvWaveLength + v.vertex.z * _InvWaveLength) * _Magnitude;
                v.vertex = v.vertex + offset;

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
    Fallback "Transparent/Cutout/VertexLit"
}
