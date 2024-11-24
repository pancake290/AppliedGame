Shader"Custom/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineWidth ("Outline Width", Range(0.0, 0.03)) = 0.005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Name "Outline"
            Cull Front
            ZWrite On
            ZTest LEqual
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
        
struct appdata
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
};

struct v2f
{
    float4 pos : SV_POSITION;
    float4 color : COLOR;
};

fixed4 _OutlineColor;
float _OutlineWidth;

v2f vert(appdata v)
{
                // 扩大顶点位置以形成轮廓
    v2f o;
    float3 norm = UnityObjectToWorldNormal(v.normal);
    o.pos = UnityObjectToClipPos(v.vertex + float4(norm * _OutlineWidth, 0.0));
    o.color = _OutlineColor;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    return i.color;
}
            ENDCG
        }
    }
}