// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32719,y:32712,varname:node_4013,prsc:2|diff-465-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:31955,y:33190,ptovrint:False,ptlb:Diffuse Tint,ptin:_DiffuseTint,varname:_DiffuseTint,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.4,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6045,x:31955,y:33011,ptovrint:False,ptlb:Diffuse Texture,ptin:_DiffuseTexture,varname:_DiffuseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2679,x:32132,y:33112,varname:node_2679,prsc:2|A-6045-RGB,B-1304-RGB;n:type:ShaderForge.SFN_Tex2d,id:3708,x:31857,y:32126,ptovrint:False,ptlb:Snow Texture,ptin:_SnowTexture,varname:_SnowTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:9029,x:31857,y:32305,ptovrint:False,ptlb:Snow Color,ptin:_SnowColor,varname:_SnowColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:3841,x:32112,y:32195,varname:node_3841,prsc:2|A-3708-RGB,B-9029-RGB;n:type:ShaderForge.SFN_NormalVector,id:7491,x:31472,y:32700,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector3,id:5413,x:31472,y:32846,varname:node_5413,prsc:2,v1:0,v2:1,v3:0;n:type:ShaderForge.SFN_Dot,id:4084,x:31649,y:32772,varname:node_4084,prsc:2,dt:0|A-7491-OUT,B-5413-OUT;n:type:ShaderForge.SFN_Slider,id:8191,x:31421,y:32608,ptovrint:False,ptlb:Snow Coverage,ptin:_SnowCoverage,varname:_SnowCoverage,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Step,id:6042,x:31835,y:32727,varname:node_6042,prsc:2|A-8191-OUT,B-4084-OUT;n:type:ShaderForge.SFN_If,id:465,x:32441,y:32824,varname:node_465,prsc:2|A-2073-OUT,B-4800-OUT,GT-2243-OUT,EQ-2679-OUT,LT-2679-OUT;n:type:ShaderForge.SFN_Vector1,id:4800,x:32441,y:32775,varname:node_4800,prsc:2,v1:0;n:type:ShaderForge.SFN_FragmentPosition,id:7460,x:30622,y:32727,varname:node_7460,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:5542,x:30525,y:32127,ptovrint:False,ptlb:Snow Height,ptin:_SnowHeight,varname:_SnowHeight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-5;n:type:ShaderForge.SFN_Step,id:1303,x:31332,y:32980,varname:node_1303,prsc:2|A-1339-OUT,B-7460-Y;n:type:ShaderForge.SFN_Multiply,id:2073,x:31835,y:32861,varname:node_2073,prsc:2|A-6042-OUT,B-1303-OUT;n:type:ShaderForge.SFN_ValueProperty,id:773,x:30347,y:32439,ptovrint:False,ptlb:Frost Height,ptin:_FrostHeight,varname:_FrostHeight,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:1463,x:30347,y:32520,varname:node_1463,prsc:2,v1:0;n:type:ShaderForge.SFN_If,id:6898,x:30557,y:32415,varname:node_6898,prsc:2|A-773-OUT,B-1463-OUT,GT-773-OUT,EQ-773-OUT,LT-1463-OUT;n:type:ShaderForge.SFN_Subtract,id:1339,x:30744,y:32393,varname:node_1339,prsc:2|A-5542-OUT,B-6898-OUT;n:type:ShaderForge.SFN_Step,id:1414,x:30997,y:32229,varname:node_1414,prsc:2|A-1339-OUT,B-7460-Y;n:type:ShaderForge.SFN_Step,id:5766,x:30997,y:32354,varname:node_5766,prsc:2|A-7460-Y,B-5542-OUT;n:type:ShaderForge.SFN_Multiply,id:3174,x:31193,y:32273,varname:node_3174,prsc:2|A-1414-OUT,B-5766-OUT;n:type:ShaderForge.SFN_Smoothstep,id:5941,x:31104,y:32107,varname:node_5941,prsc:2|A-1339-OUT,B-5542-OUT,V-7460-Y;n:type:ShaderForge.SFN_Multiply,id:4375,x:31366,y:32238,varname:node_4375,prsc:2|A-5941-OUT,B-3174-OUT;n:type:ShaderForge.SFN_Step,id:7050,x:31081,y:32547,varname:node_7050,prsc:2|A-5542-OUT,B-7460-Y;n:type:ShaderForge.SFN_Add,id:502,x:31421,y:32466,varname:node_502,prsc:2|A-4375-OUT,B-7050-OUT;n:type:ShaderForge.SFN_Lerp,id:2243,x:32183,y:32448,varname:node_2243,prsc:2|A-2679-OUT,B-3841-OUT,T-502-OUT;proporder:1304-6045-3708-9029-8191-5542-773;pass:END;sub:END;*/

Shader "Shader Forge/SnowShader" {
    Properties {
        _DiffuseTint ("Diffuse Tint", Color) = (0,0.4,0,1)
        _DiffuseTexture ("Diffuse Texture", 2D) = "white" {}
        _SnowTexture ("Snow Texture", 2D) = "white" {}
        _SnowColor ("Snow Color", Color) = (1,1,1,1)
        _SnowCoverage ("Snow Coverage", Range(0, 1)) = 0.5
        _SnowHeight ("Snow Height", Float ) = -5
        _FrostHeight ("Frost Height", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _DiffuseTint;
            uniform sampler2D _DiffuseTexture; uniform float4 _DiffuseTexture_ST;
            uniform sampler2D _SnowTexture; uniform float4 _SnowTexture_ST;
            uniform float4 _SnowColor;
            uniform float _SnowCoverage;
            uniform float _SnowHeight;
            uniform float _FrostHeight;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_1463 = 0.0;
                float node_6898_if_leA = step(_FrostHeight,node_1463);
                float node_6898_if_leB = step(node_1463,_FrostHeight);
                float node_1339 = (_SnowHeight-lerp((node_6898_if_leA*node_1463)+(node_6898_if_leB*_FrostHeight),_FrostHeight,node_6898_if_leA*node_6898_if_leB));
                float node_465_if_leA = step((step(_SnowCoverage,dot(i.normalDir,float3(0,1,0)))*step(node_1339,i.posWorld.g)),0.0);
                float node_465_if_leB = step(0.0,(step(_SnowCoverage,dot(i.normalDir,float3(0,1,0)))*step(node_1339,i.posWorld.g)));
                float4 _DiffuseTexture_var = tex2D(_DiffuseTexture,TRANSFORM_TEX(i.uv0, _DiffuseTexture));
                float3 node_2679 = (_DiffuseTexture_var.rgb*_DiffuseTint.rgb);
                float4 _SnowTexture_var = tex2D(_SnowTexture,TRANSFORM_TEX(i.uv0, _SnowTexture));
                float3 diffuseColor = lerp((node_465_if_leA*node_2679)+(node_465_if_leB*lerp(node_2679,(_SnowTexture_var.rgb*_SnowColor.rgb),((smoothstep( node_1339, _SnowHeight, i.posWorld.g )*(step(node_1339,i.posWorld.g)*step(i.posWorld.g,_SnowHeight)))+step(_SnowHeight,i.posWorld.g)))),node_2679,node_465_if_leA*node_465_if_leB);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _DiffuseTint;
            uniform sampler2D _DiffuseTexture; uniform float4 _DiffuseTexture_ST;
            uniform sampler2D _SnowTexture; uniform float4 _SnowTexture_ST;
            uniform float4 _SnowColor;
            uniform float _SnowCoverage;
            uniform float _SnowHeight;
            uniform float _FrostHeight;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float node_1463 = 0.0;
                float node_6898_if_leA = step(_FrostHeight,node_1463);
                float node_6898_if_leB = step(node_1463,_FrostHeight);
                float node_1339 = (_SnowHeight-lerp((node_6898_if_leA*node_1463)+(node_6898_if_leB*_FrostHeight),_FrostHeight,node_6898_if_leA*node_6898_if_leB));
                float node_465_if_leA = step((step(_SnowCoverage,dot(i.normalDir,float3(0,1,0)))*step(node_1339,i.posWorld.g)),0.0);
                float node_465_if_leB = step(0.0,(step(_SnowCoverage,dot(i.normalDir,float3(0,1,0)))*step(node_1339,i.posWorld.g)));
                float4 _DiffuseTexture_var = tex2D(_DiffuseTexture,TRANSFORM_TEX(i.uv0, _DiffuseTexture));
                float3 node_2679 = (_DiffuseTexture_var.rgb*_DiffuseTint.rgb);
                float4 _SnowTexture_var = tex2D(_SnowTexture,TRANSFORM_TEX(i.uv0, _SnowTexture));
                float3 diffuseColor = lerp((node_465_if_leA*node_2679)+(node_465_if_leB*lerp(node_2679,(_SnowTexture_var.rgb*_SnowColor.rgb),((smoothstep( node_1339, _SnowHeight, i.posWorld.g )*(step(node_1339,i.posWorld.g)*step(i.posWorld.g,_SnowHeight)))+step(_SnowHeight,i.posWorld.g)))),node_2679,node_465_if_leA*node_465_if_leB);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
