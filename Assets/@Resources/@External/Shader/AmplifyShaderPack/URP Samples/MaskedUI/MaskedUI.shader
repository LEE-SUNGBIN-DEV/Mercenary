// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AmplifyShaderPack/URP/MaskedUI"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		_WaveWidthB("WaveWidthB", Float) = 2.5
		_WaveAmplitudeB("WaveAmplitudeB", Float) = 0
		_WaveAmplitudeA("WaveAmplitudeA", Float) = 0
		_WaveWidthA("WaveWidthA", Float) = 2.5
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_YDisplacementA("YDisplacementA", Float) = 0
		_ColorWaveB("ColorWaveB", Color) = (1,0.376056,0.05882353,1)
		_YDisplacementB("YDisplacementB", Float) = 5
		_ColorWaveA("ColorWaveA", Color) = (0.4485294,0.313166,0.1517085,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		Stencil
		{
			Ref [_Stencil]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
			CompFront [_StencilComp]
			PassFront [_StencilOp]
			FailFront Keep
			ZFailFront Keep
			CompBack Always
			PassBack Keep
			FailBack Keep
			ZFailBack Keep
		}


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		
		Pass
		{
			Name ""
		CGPROGRAM
			
			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR

			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float4 _ColorWaveA;
			uniform float _WaveAmplitudeA;
			uniform float _YDisplacementA;
			float4 _MainTex_TexelSize;
			uniform float _WaveWidthA;
			uniform float _WaveAmplitudeB;
			uniform float _YDisplacementB;
			uniform float _WaveWidthB;
			uniform float4 _ColorWaveB;
			uniform sampler2D _TextureSample2;
			uniform float4 _TextureSample2_ST;
			uniform float4 _MainTex_ST;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				OUT.worldPosition = IN.vertex;
				
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				float2 temp_cast_0 = (1.0).xx;
				float2 texCoord86 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 newUV99 = ( temp_cast_0 - ( texCoord86 * 2.0 ) );
				float2 temp_output_1_0_g14 = newUV99;
				float mulTime96 = _Time.y * 2.0;
				float scaledTime124 = mulTime96;
				float temp_output_6_0_g14 = scaledTime124;
				float temp_output_16_0_g14 = (temp_output_1_0_g14).y;
				float YVal31_g14 = ( ( _WaveAmplitudeA * cos( ( ( UNITY_PI * (temp_output_1_0_g14).x ) + ( UNITY_PI * temp_output_6_0_g14 ) ) ) * sin( ( ( temp_output_16_0_g14 * UNITY_PI ) + ( _YDisplacementA / 3.0 ) + ( temp_output_6_0_g14 * UNITY_PI ) ) ) ) + temp_output_16_0_g14 );
				float overallHeight125 = _MainTex_TexelSize.w;
				float2 temp_output_1_0_g13 = newUV99;
				float temp_output_6_0_g13 = scaledTime124;
				float temp_output_16_0_g13 = (temp_output_1_0_g13).y;
				float YVal31_g13 = ( ( _WaveAmplitudeB * cos( ( ( UNITY_PI * (temp_output_1_0_g13).x ) + ( UNITY_PI * temp_output_6_0_g13 ) ) ) * sin( ( ( temp_output_16_0_g13 * UNITY_PI ) + ( _YDisplacementB / 3.0 ) + ( temp_output_6_0_g13 * UNITY_PI ) ) ) ) + temp_output_16_0_g13 );
				float2 uv_TextureSample2 = IN.texcoord.xy * _TextureSample2_ST.xy + _TextureSample2_ST.zw;
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 temp_output_8_0 = ( ( tex2D( _MainTex, uv_MainTex ) + _TextureSampleAdd ) * IN.color );
				float4 appendResult137 = (float4(( ( (( ( _ColorWaveA * abs( ( 1.0 / ( ( YVal31_g14 * overallHeight125 ) / _WaveWidthA ) ) ) ) + ( abs( ( 1.0 / ( ( YVal31_g13 * overallHeight125 ) / _WaveWidthB ) ) ) * _ColorWaveB ) )).rgb * tex2D( _TextureSample2, uv_TextureSample2 ).r ) + (temp_output_8_0).rgb ) , (temp_output_8_0).a));
				
				half4 color = appendResult137;
				
				#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}

	
	
}
/*ASEBEGIN
Version=18920
303;172;1497;869;1182.085;3280.216;3.4;True;False
Node;AmplifyShaderEditor.CommentaryNode;146;904.3149,-1925.409;Inherit;False;893.204;465.4991;Comment;6;93;86;104;91;90;99;Offset UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;93;967.1171,-1574.91;Float;False;Constant;_Float3;Float 3;6;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;145;931.9144,-2451.912;Inherit;False;844.3919;332.3784;Comment;3;139;97;125;Set Overall Height;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;141;986.3222,-1363.926;Inherit;False;711.6904;242.309;Comment;3;120;96;124;Time Scale;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;86;956.5161,-1750.31;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;104;954.3149,-1872.208;Float;False;Constant;_Float0;Float 0;7;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;1209.615,-1659.008;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;120;1036.322,-1295.316;Float;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;139;981.9144,-2250.523;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;96;1221.018,-1231.617;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexelSizeNode;97;1247.313,-2401.912;Inherit;False;-1;1;0;SAMPLER2D;;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;90;1361.616,-1875.409;Inherit;False;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;125;1520.306,-2234.534;Float;False;overallHeight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;99;1554.519,-1670.908;Float;False;newUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;142;1984.314,-2287.615;Inherit;False;1141.609;1314.004;Comment;17;128;112;131;129;133;132;130;126;127;95;134;113;110;115;114;109;116;Create Wave Effect;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;124;1455.013,-1313.926;Float;False;scaledTime;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;133;2056.514,-1818.329;Inherit;False;99;newUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;140;2622.898,-483.5008;Inherit;False;1072.104;626.4009;Comment;6;4;2;7;8;5;6;Default UI;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;132;2078.514,-1318.329;Float;False;Property;_WaveWidthB;WaveWidthB;0;0;Create;True;0;0;0;False;0;False;2.5;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;129;2086.212,-1492.031;Inherit;False;125;overallHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;131;2085.512,-1413.629;Inherit;False;124;scaledTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;2100.414,-1088.613;Float;False;Property;_YDisplacementB;YDisplacementB;7;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;128;2079.313,-1566.531;Float;False;Property;_WaveAmplitudeB;WaveAmplitudeB;1;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;130;2069.512,-1989.027;Inherit;False;124;scaledTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;127;2050.61,-2061.425;Inherit;False;125;overallHeight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;134;2078.514,-1213.329;Inherit;False;99;newUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;95;2052.215,-1912.615;Float;False;Property;_WaveWidthA;WaveWidthA;3;0;Create;True;0;0;0;False;0;False;2.5;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;113;2034.314,-1721.013;Float;False;Property;_YDisplacementA;YDisplacementA;5;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;126;2052.711,-2141.431;Float;False;Property;_WaveAmplitudeA;WaveAmplitudeA;2;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;6;2672.898,-367.6007;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;110;2332.813,-2237.615;Float;False;Property;_ColorWaveA;ColorWaveA;8;0;Create;True;0;0;0;False;0;False;0.4485294,0.313166,0.1517085,1;0.4485293,0.313166,0.1517085,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;115;2435.916,-1257.111;Float;False;Property;_ColorWaveB;ColorWaveB;6;0;Create;True;0;0;0;False;0;False;1,0.376056,0.05882353,1;1,0.376056,0.0588235,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;150;2440.218,-1544.512;Inherit;False;CoolWave;-1;;13;b2ee99647053b0745a8b40f9f5921b4d;0;6;35;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;149;2354.311,-2005.012;Inherit;False;CoolWave;-1;;14;b2ee99647053b0745a8b40f9f5921b4d;0;6;35;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;2932.799,-433.501;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;4;2963.498,-188.1996;Inherit;False;0;0;_TextureSampleAdd;Pass;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;2798.72,-1430.415;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;109;2655.218,-2118.014;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;2;3237.599,-59.09987;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;7;3342.499,-369.4988;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;143;3301.113,-1553.931;Inherit;False;531.0039;432.2048;Comment;3;122;135;121;Apply Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;116;2971.924,-1544.211;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;144;3763.41,-464.8258;Inherit;False;680.5081;458.4976;Comment;4;137;138;123;136;Combine Default + Waves;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;3526.002,-290.0993;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;121;3351.113,-1351.726;Inherit;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;0;False;0;False;-1;None;fbda8092d7b84f87be3259ec87d72dd1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwizzleNode;135;3377.213,-1503.931;Inherit;False;FLOAT3;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;3663.116,-1491.124;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;136;3813.41,-404.5289;Inherit;False;FLOAT3;0;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;123;4019.309,-414.8258;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;138;3823.016,-171.3282;Inherit;False;FLOAT;3;1;2;3;1;0;COLOR;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;137;4178.417,-232.4282;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;4470.103,-334.2029;Float;False;True;-1;2;ASEMaterialInspector;0;6;AmplifyShaderPack/URP/MaskedUI;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;;2;False;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;True;True;True;True;True;0;True;-9;False;False;False;False;False;False;False;True;True;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;91;0;86;0
WireConnection;91;1;93;0
WireConnection;96;0;120;0
WireConnection;97;0;139;0
WireConnection;90;0;104;0
WireConnection;90;1;91;0
WireConnection;125;0;97;4
WireConnection;99;0;90;0
WireConnection;124;0;96;0
WireConnection;150;35;128;0
WireConnection;150;4;129;0
WireConnection;150;6;131;0
WireConnection;150;3;132;0
WireConnection;150;1;134;0
WireConnection;150;2;112;0
WireConnection;149;35;126;0
WireConnection;149;4;127;0
WireConnection;149;6;130;0
WireConnection;149;3;95;0
WireConnection;149;1;133;0
WireConnection;149;2;113;0
WireConnection;5;0;6;0
WireConnection;114;0;150;0
WireConnection;114;1;115;0
WireConnection;109;0;110;0
WireConnection;109;1;149;0
WireConnection;7;0;5;0
WireConnection;7;1;4;0
WireConnection;116;0;109;0
WireConnection;116;1;114;0
WireConnection;8;0;7;0
WireConnection;8;1;2;0
WireConnection;135;0;116;0
WireConnection;122;0;135;0
WireConnection;122;1;121;1
WireConnection;136;0;8;0
WireConnection;123;0;122;0
WireConnection;123;1;136;0
WireConnection;138;0;8;0
WireConnection;137;0;123;0
WireConnection;137;3;138;0
WireConnection;0;0;137;0
ASEEND*/
//CHKSM=1C700D4FB8F2D4F9D3292CFE4E518A5DC14515E7