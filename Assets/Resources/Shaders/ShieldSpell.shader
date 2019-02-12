// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31566,y:32568,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:66142d696a878ec479cc334af46a7549,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-220-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT,E-6074-A;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33073,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Lerp,id:9685,x:31566,y:32414,varname:node_9685,prsc:2|A-9119-RGB,B-1433-U,T-9922-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9922,x:31335,y:32591,ptovrint:False,ptlb:Lerp Time,ptin:_LerpTime,varname:node_9922,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Tex2d,id:9119,x:31345,y:32264,ptovrint:False,ptlb:Noise Texture,ptin:_NoiseTexture,varname:node_9119,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:207a0c91ec8434cd3a6e2e6ec7262d41,ntxv:0,isnm:False|UVIN-5466-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5214,x:30584,y:32401,ptovrint:False,ptlb:U Speed,ptin:_USpeed,varname:node_5214,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:8972,x:30584,y:32478,ptovrint:False,ptlb:V Speed,ptin:_VSpeed,varname:node_8972,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:4531,x:30762,y:32428,varname:node_4531,prsc:2|A-5214-OUT,B-8972-OUT;n:type:ShaderForge.SFN_Multiply,id:2433,x:30959,y:32337,varname:node_2433,prsc:2|A-3187-T,B-4531-OUT;n:type:ShaderForge.SFN_Time,id:3187,x:30762,y:32300,varname:node_3187,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:7371,x:30959,y:32190,varname:node_7371,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:5466,x:31155,y:32297,varname:node_5466,prsc:2|A-7371-UVOUT,B-2433-OUT;n:type:ShaderForge.SFN_TexCoord,id:1433,x:31335,y:32433,varname:node_1433,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Abs,id:112,x:31737,y:32424,varname:node_112,prsc:2|IN-9685-OUT;n:type:ShaderForge.SFN_Color,id:1476,x:31752,y:32619,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:5,c2:0.5,c3:5,c4:1;n:type:ShaderForge.SFN_Multiply,id:220,x:31947,y:32525,varname:node_220,prsc:2|A-112-OUT,B-1476-RGB,C-6074-RGB;proporder:6074-797-9922-9119-5214-8972-1476;pass:END;sub:END;*/

Shader "Shader Forge/ShieldSpell" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _LerpTime ("Lerp Time", Float ) = 3
        _NoiseTexture ("Noise Texture", 2D) = "white" {}
        _USpeed ("U Speed", Float ) = 0.1
        _VSpeed ("V Speed", Float ) = 0.1
        _Color ("Color", Color) = (5,0.5,5,1)
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
            uniform float _LerpTime;
            uniform sampler2D _NoiseTexture; uniform float4 _NoiseTexture_ST;
            uniform float _USpeed;
            uniform float _VSpeed;
            uniform float4 _Color;
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
                float4 node_3187 = _Time;
                float2 node_5466 = (i.uv0+(node_3187.g*float2(_USpeed,_VSpeed)));
                float4 _NoiseTexture_var = tex2D(_NoiseTexture,TRANSFORM_TEX(node_5466, _NoiseTexture));
                float3 node_9685 = lerp(_NoiseTexture_var.rgb,float3(i.uv0.r,i.uv0.r,i.uv0.r),_LerpTime);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 emissive = ((abs(node_9685)*_Color.rgb*_MainTex_var.rgb)*i.vertexColor.rgb*_TintColor.rgb*2.0*_MainTex_var.a);
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
