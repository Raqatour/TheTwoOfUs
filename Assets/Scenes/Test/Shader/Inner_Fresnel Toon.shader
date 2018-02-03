// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|emission-734-OUT;n:type:ShaderForge.SFN_Tex2d,id:1213,x:31746,y:32441,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1213,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:efee36f974d458d449e18a923fb5383a,ntxv:0,isnm:False|UVIN-2186-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:2963,x:31547,y:32724,varname:node_2963,prsc:2|EXP-9517-OUT;n:type:ShaderForge.SFN_Multiply,id:9970,x:32009,y:32738,varname:node_9970,prsc:2|A-1213-RGB,B-2963-OUT,C-7566-OUT;n:type:ShaderForge.SFN_Clamp01,id:3140,x:32199,y:32834,varname:node_3140,prsc:2|IN-9970-OUT;n:type:ShaderForge.SFN_Color,id:5497,x:32201,y:32647,ptovrint:False,ptlb:Tint,ptin:_Tint,varname:node_5497,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9023386,c2:0.6149979,c3:0.9191176,c4:1;n:type:ShaderForge.SFN_SceneColor,id:3113,x:32006,y:32429,varname:node_3113,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:6669,x:32201,y:32429,varname:node_6669,prsc:2|IN-3113-RGB;n:type:ShaderForge.SFN_Lerp,id:734,x:32489,y:32723,varname:node_734,prsc:2|A-6669-OUT,B-5497-RGB,T-3140-OUT;n:type:ShaderForge.SFN_Slider,id:9517,x:31189,y:32763,ptovrint:False,ptlb:_FresnelAmount,ptin:__FresnelAmount,varname:node_9517,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:7566,x:31210,y:33003,ptovrint:False,ptlb:_Transparency,ptin:__Transparency,varname:node_7566,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_TexCoord,id:34,x:30739,y:32267,varname:node_34,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:492,x:31299,y:32328,varname:node_492,prsc:2,spu:0,spv:1|UVIN-34-UVOUT,DIST-2221-OUT;n:type:ShaderForge.SFN_Panner,id:2186,x:31502,y:32471,varname:node_2186,prsc:2,spu:1,spv:0|UVIN-492-UVOUT,DIST-2805-OUT;n:type:ShaderForge.SFN_Multiply,id:2805,x:31086,y:32581,varname:node_2805,prsc:2|A-3671-T,B-9214-OUT;n:type:ShaderForge.SFN_Time,id:3671,x:30507,y:32509,varname:node_3671,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2221,x:31086,y:32460,varname:node_2221,prsc:2|A-2675-OUT,B-3671-T;n:type:ShaderForge.SFN_Slider,id:2675,x:30411,y:32411,ptovrint:False,ptlb:VerticalSpeed,ptin:_VerticalSpeed,varname:node_2675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0.4812982,max:2;n:type:ShaderForge.SFN_Slider,id:9214,x:30719,y:32812,ptovrint:False,ptlb:HorizontalSpeed,ptin:_HorizontalSpeed,varname:node_9214,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:1.052955,max:2;proporder:1213-5497-9517-7566-2675-9214;pass:END;sub:END;*/

Shader "Shader Forge/Inner_Fresnel_Toon" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Tint ("Tint", Color) = (0.9023386,0.6149979,0.9191176,1)
        __FresnelAmount ("_FresnelAmount", Range(0, 1)) = 1
        __Transparency ("_Transparency", Range(0, 1)) = 1
        _VerticalSpeed ("VerticalSpeed", Range(-2, 2)) = 0.4812982
        _HorizontalSpeed ("HorizontalSpeed", Range(-2, 2)) = 1.052955
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Tint;
            uniform float __FresnelAmount;
            uniform float __Transparency;
            uniform float _VerticalSpeed;
            uniform float _HorizontalSpeed;
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
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 node_3671 = _Time;
                float2 node_2186 = ((i.uv0+(_VerticalSpeed*node_3671.g)*float2(0,1))+(node_3671.g*_HorizontalSpeed)*float2(1,0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2186, _MainTex));
                float3 emissive = lerp(saturate(sceneColor.rgb),_Tint.rgb,saturate((_MainTex_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),__FresnelAmount)*__Transparency)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Tint;
            uniform float __FresnelAmount;
            uniform float __Transparency;
            uniform float _VerticalSpeed;
            uniform float _HorizontalSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_3671 = _Time;
                float2 node_2186 = ((i.uv0+(_VerticalSpeed*node_3671.g)*float2(0,1))+(node_3671.g*_HorizontalSpeed)*float2(1,0));
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_2186, _MainTex));
                o.Emission = lerp(saturate(sceneColor.rgb),_Tint.rgb,saturate((_MainTex_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),__FresnelAmount)*__Transparency)));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
