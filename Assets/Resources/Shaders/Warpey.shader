// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32210,y:32595,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:35cb46ea398fd824fb212d8b029259c8,ntxv:0,isnm:False|UVIN-1341-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-9248-OUT,E-3995-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32210,y:32770,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32210,y:32928,ptovrint:True,ptlb:MainColor,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8454853,c3:0.990566,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32210,y:33075,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:3995,x:32495,y:32959,varname:node_3995,prsc:2|A-6074-A,B-9211-R;n:type:ShaderForge.SFN_Tex2d,id:9211,x:32210,y:33154,ptovrint:False,ptlb:Alpha Mask,ptin:_AlphaMask,varname:_node_9211,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:58dcc4d19129a924ba084ce1b71b17a3,ntxv:1,isnm:False;n:type:ShaderForge.SFN_Add,id:1341,x:32002,y:32562,varname:node_1341,prsc:2|A-7633-OUT,B-2327-OUT;n:type:ShaderForge.SFN_Lerp,id:7633,x:31821,y:32516,varname:node_7633,prsc:2|A-335-UVOUT,B-7371-OUT,T-1546-R;n:type:ShaderForge.SFN_Multiply,id:2327,x:31821,y:32634,varname:node_2327,prsc:2|A-3273-T,B-9776-OUT;n:type:ShaderForge.SFN_TexCoord,id:335,x:31589,y:32185,varname:node_335,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:7371,x:31589,y:32354,varname:node_7371,prsc:2|A-5660-UVOUT,B-7639-R,T-3515-OUT;n:type:ShaderForge.SFN_Tex2d,id:7639,x:31366,y:32413,ptovrint:False,ptlb:Noise Texture,ptin:_NoiseTexture,varname:_NoiseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2533-OUT;n:type:ShaderForge.SFN_Time,id:3273,x:31589,y:32731,varname:node_3273,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:5660,x:31366,y:32246,varname:node_5660,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:2533,x:31062,y:32527,varname:node_2533,prsc:2|A-7491-UVOUT,B-6124-OUT;n:type:ShaderForge.SFN_TexCoord,id:7491,x:30815,y:32467,varname:node_7491,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6124,x:30815,y:32602,varname:node_6124,prsc:2|A-4027-T,B-2482-OUT;n:type:ShaderForge.SFN_Append,id:2482,x:30606,y:32667,varname:node_2482,prsc:2|A-9-OUT,B-3006-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3006,x:30394,y:32727,ptovrint:False,ptlb:V Speed,ptin:_VSpeed,varname:_VSpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:9,x:30394,y:32634,ptovrint:False,ptlb:U Speed,ptin:_USpeed,varname:_USpeed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Time,id:4027,x:30606,y:32467,varname:node_4027,prsc:2;n:type:ShaderForge.SFN_Slider,id:3515,x:31209,y:32588,ptovrint:False,ptlb:Noise Amount,ptin:_NoiseAmount,varname:_NoiseAmount,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:1546,x:31589,y:32544,ptovrint:False,ptlb:Noise Mask,ptin:_NoiseMask,varname:_NoiseMask,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:11abb57ced1fa454eb23a54ab052efc4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:9776,x:31589,y:32848,varname:node_9776,prsc:2|A-983-OUT,B-9923-OUT;n:type:ShaderForge.SFN_ValueProperty,id:983,x:31342,y:32909,ptovrint:False,ptlb:U SpeedMain,ptin:_USpeedMain,varname:_USpeedMain,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:9923,x:31342,y:32979,ptovrint:False,ptlb:V SpeedMain,ptin:_VSpeedMain,varname:_VSpeedMain,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;proporder:797-6074-9211-7639-9-3006-3515-1546-983-9923;pass:END;sub:END;*/

Shader "Shader Forge/Warpey" {
    Properties {
        _TintColor ("MainColor", Color) = (0,0.8454853,0.990566,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _AlphaMask ("Alpha Mask", 2D) = "gray" {}
        _NoiseTexture ("Noise Texture", 2D) = "white" {}
        _USpeed ("U Speed", Float ) = 0.1
        _VSpeed ("V Speed", Float ) = 0
        _NoiseAmount ("Noise Amount", Range(0, 1)) = 0
        _NoiseMask ("Noise Mask", 2D) = "white" {}
        _USpeedMain ("U SpeedMain", Float ) = 0.1
        _VSpeedMain ("V SpeedMain", Float ) = 0.1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform sampler2D _AlphaMask; uniform float4 _AlphaMask_ST;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
            uniform float _VSpeed;
            uniform float _USpeed;
            uniform float _NoiseAmount;
            uniform sampler2D _NoiseMask; uniform float4 _NoiseMask_ST;
            uniform float _USpeedMain;
            uniform float _VSpeedMain;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_4027 = _Time;
                float2 node_2533 = (i.uv0+(node_4027.g*float2(_USpeed,_VSpeed)));
                float4 _NoiseTexture_var = tex2D(_NoiseTexture,TRANSFORM_TEX(node_2533, _NoiseTexture));
                float4 _NoiseMask_var = tex2D(_NoiseMask,TRANSFORM_TEX(i.uv0, _NoiseMask));
                float4 node_3273 = _Time;
                float2 node_1341 = (lerp(i.uv0,lerp(i.uv0,float2(_NoiseTexture_var.r,_NoiseTexture_var.r),_NoiseAmount),_NoiseMask_var.r)+(node_3273.g*float2(_USpeedMain,_VSpeedMain)));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1341, _MainTex));
                float4 _AlphaMask_var = tex2D(_AlphaMask,TRANSFORM_TEX(i.uv0, _AlphaMask));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*2.0*(_MainTex_var.a*_AlphaMask_var.r));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
