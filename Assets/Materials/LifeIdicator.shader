Shader "Hidden/LifeIdicator"
{
    Properties
    {
        _Intensity ("Intensidad Indicador", Range(0.01, 1.0)) = 0.3


        _RadiusX ("Radio Horizontal", Range(0.01, 1.0)) = 0.3
       
        _Smoothness ("Suavizado de Borde", Range(0.001, 0.1)) = 0.01
        
        // 1. Añadimos la propiedad para la textura de ruido en el inspector
        _NoiseTex ("Textura de Ruido (2D)", 2D) = "white" {}
   

         // 1. Añadimos los dos colores para crear la rampa
        _ColorA ("Color Ramp: Inicio (Ruido 0)", Color) = (1, 0, 0, 1) // Rojo por defecto
        _ColorB ("Color Ramp: Fin (Ruido 1)", Color) = (0, 0, 1, 1)    // Azul por defecto

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

      
            float _RadiusX;
            
            float _Smoothness;
            float _Intensity;
            // 2. Declaramos las variables de la textura para HLSL
            sampler2D _NoiseTex;
            float4 _NoiseTex_ST; // Variable interna de Unity para el Tiling y Offset (Escala y posición)
            float _NoiseStrength;

         
            float4 _ColorA;
            float4 _ColorB;

            float remap(float value, float originalMin, float originalMax, float targetMin, float targetMax)
            {
                return targetMin + (value - originalMin) * (targetMax - targetMin) / (originalMax - originalMin);
            }

            float _RadiusY ;



            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Aplica la escala y desfase definidos en el inspector para la textura
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // 3. Muestrear (acceder) al valor de la textura usando las coordenadas UV del píxel
                // Multiplicamos por _NoiseTex_ST.xy y sumamos _NoiseTex_ST.zw para que respete el Tiling/Offset
                float2 noiseUV = TRANSFORM_TEX(i.uv, _NoiseTex);
                float4 noiseSample = tex2D(_NoiseTex, noiseUV);

                float noiseValue = noiseSample.r;
                
                _RadiusY = remap(_Intensity , 0 , 0.8 , 0.8 , 0.23);
                _NoiseStrength = _Intensity;

                // --- Código base del elipse ---
                float aspectRatio = _ScreenParams.x / _ScreenParams.y;
                float2 pos = i.uv - float2(0.5, 0.5);
                pos.x *= aspectRatio;

                float2 radii = float2(_RadiusX * aspectRatio, _RadiusY);
                float2 normalizedPos = (pos * pos) / (radii * radii);
                float distanceSq = normalizedPos.x + normalizedPos.y;
                
                // 4. EJEMPLO DE USO: Modificamos el borde del elipse sumándole el ruido extraído
                // Esto creará un contorno irregular o distorsionado
                float finalDistance = distanceSq + (noiseValue * _NoiseStrength);
                float4 MixValue = lerp(_ColorA , _ColorB , finalDistance);
                float alpha =  smoothstep(1.0 - _Smoothness, 1.0, finalDistance);

                return float4(MixValue.rgb,  alpha );
            }
            ENDHLSL
        }
    }
}
