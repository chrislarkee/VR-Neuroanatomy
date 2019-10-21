// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "VertexInputSimple" {
  Properties {
    _SoundWavePosition ("Sound Wave Position", Vector) = (0,0,0,0)
    _SoundWaveRange ("Sound Wave Range", Float) = 2.0
    _SoundWaveWidth ("Sound Wave Width", Float) = 0.1

    _MainTex ("Texture", 2D) = "white" {}
  }

  
  SubShader {
  
    Pass {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      float4 _SoundWavePosition;
      float _SoundWaveRange;
      float _SoundWaveWidth;
      sampler2D _MainTex;

      
      struct v2f {
          float4 pos : SV_POSITION;
          fixed4 color : COLOR;
          float2  uv : TEXCOORD0;          
      };

      float4 _MainTex_ST;
      
      v2f vert (appdata_base v)
      {
          v2f o;
          o.pos = UnityObjectToClipPos(v.vertex);
          float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
          
          float vertexDistance = distance(worldPos, _SoundWavePosition);

          float alpha;

          if(abs(vertexDistance - _SoundWaveRange) < _SoundWaveWidth) {
            alpha = 1;
          } else {
            alpha = 0;
          }

          o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
          o.color.rgb = (1,1,1,alpha);
          
          return o;
      }

      fixed4 frag (v2f i) : COLOR0 {
        half4 texcol = tex2D (_MainTex, i.uv);
        return texcol * i.color;
      }
      ENDCG
    }
  } 
}
