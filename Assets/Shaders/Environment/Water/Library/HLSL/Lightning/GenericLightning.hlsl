/*
 
  Generic Lightning Template
 
  Non-PBR lightning with Blinn-Phong specular model
  
  Supports:
  1. Main light, additional lights
  2. Realtime shadows, baked shadows, shadow cascades, soft shadows
  3. Baked GI
  4. Fog

  Gives you complete control over lightning (which is not possible with URP Lit shaders)
  You can customize this template to create different custom lightning effects (ex. cel shading)
  
*/

#ifndef GENERIC_LIGHTNING_INCLUDED
#define GENERIC_LIGHTNING_INCLUDED

#include "Assets/Shaders/Library/HLSL/Math.hlsl"

struct LightningData
{
    // Position and orientation
    half3 positionWS;
    half3 normalWS;
    half3 viewDirectionWS;
    half4 shadowCoord;

    // Surface attributes
    half3 albedo;
    half smoothness;
    half ambientOcclusion;

    // Baked lightning
    half3 bakedGI;
    half4 shadowMask;

    // Fog
    half fogFactor;
};

#ifndef SHADERGRAPH_PREVIEW

// Unnecessary include, used only for autocompletion (Rider)
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

half3 GetGlobalIllumination(LightningData data)
{
    // Baked GI isn't just lightmaps, it's also environmental lightning color from lightning settings 
    half3 indirectDiffuse = data.albedo * data.bakedGI * data.ambientOcclusion;

    half3 reflectVector = reflect(-data.viewDirectionWS, data.normalWS);
    half fresnel = Pow4(1 - saturate(dot(data.viewDirectionWS, data.normalWS)));

    // Samples the actual color of the skybox
    half3 indirectSpecular = GlossyEnvironmentReflection(reflectVector,
        RoughnessToPerceptualRoughness(1 - data.smoothness), data.ambientOcclusion) * fresnel;
    
    return indirectDiffuse + indirectSpecular;
}

half3 CalculateLight(LightningData data, Light light)
{
    half3 radiance = light.color * light.distanceAttenuation * light.shadowAttenuation;
    
    // Diffuse
    half diffuse = dot(data.normalWS, light.direction);
    diffuse = saturate(diffuse);

    // Blinn-Phong specular model
    half3 halfAngleVector = normalize(light.direction + data.viewDirectionWS);
    
    half specularDot = dot(data.normalWS, halfAngleVector);
    specularDot = saturate(specularDot);
    
    half specular = pow(specularDot, GetSmoothnessPower(data.smoothness)) * diffuse;
    
    half3 color = data.albedo * radiance * (diffuse + specular);
    
    return color;
}
#endif

half3 CalculateLightning(LightningData data)
{
    // Estimation in shader graph preview
    #ifdef SHADERGRAPH_PREVIEW
    
        half3 lightDir = float3(0.5, 0.5, 0);

        // Diffuse
        half diffuse = dot(data.normalWS, lightDir);
        diffuse = saturate(diffuse);

        // Specular
        half specularDot = dot(data.normalWS, normalize(data.viewDirectionWS + lightDir));
        specularDot = saturate(specularDot);
        half specular = pow(specularDot, GetSmoothnessPower(data.smoothness));
        
        half intensity = (specular * diffuse) + diffuse;
        
        return data.albedo * intensity;
    
    #else
    
        Light mainLight = GetMainLight(data.shadowCoord, data.positionWS, data.shadowMask);
        MixRealtimeAndBakedGI(mainLight, data.normalWS, data.bakedGI);
    
        half3 color = GetGlobalIllumination(data);
        color += CalculateLight(data, mainLight);
    
        #ifdef _ADDITIONAL_LIGHTS
        
            uint amountOfAdditionalLights = GetAdditionalLightsCount();

            for(uint lightId = 0; lightId < amountOfAdditionalLights; lightId++)
            {
                Light light = GetAdditionalLight(lightId, data.positionWS, data.shadowMask);
                color += CalculateLight(data, light);
            }
        
        #endif

        color = MixFog(color, data.fogFactor);
        return color;
    
    #endif
}

void CalculateLightning_half(in half3 Albedo, in half3 Tint, in half Smoothness, in half3 Position, in half3 Normal,
    in half3 ViewDirection, in half AmbientOcclusion, half2 LightmapUV, out half3 Color)
{
    LightningData data;

    data.positionWS = Position;
    data.normalWS = Normal;
    data.viewDirectionWS = normalize(ViewDirection);

    data.albedo = Albedo * Tint;
    data.smoothness = Smoothness;
    data.ambientOcclusion = AmbientOcclusion;

    #ifdef SHADERGRAPH_PREVIEW
    
        data.shadowCoord = 0;
        data.bakedGI = 0;
        data.shadowMask = 0;
        data.fogFactor = 0;
    
    #else

        half4 positionCS = TransformWorldToHClip(Position);

        #if SHADOWS_SCREEN
            data.shadowCoord = ComputeScreenPos(positionCS);
        #else
            data.shadowCoord = TransformWorldToShadowCoord(Position);
        #endif

        
        half3 processedLightmapUV;
        // Calculates the final lightmap UV
        OUTPUT_LIGHTMAP_UV(LightmapUV, unity_LightmapST, processedLightmapUV);

        // Samples spherical harmonics
        // Honestly, idk what this is, but it somehow used to sample the environment
        half3 vertexSH;
        OUTPUT_SH(Normal, vertexSH);

        // Calculates the final baked lightning from lightmaps and probes + environment color
        data.bakedGI = SAMPLE_GI(processedLightmapUV, vertexSH, Normal);
        data.shadowMask = SAMPLE_SHADOWMASK(processedLightmapUV);
        data.fogFactor = ComputeFogFactor(positionCS.z);
    
    #endif
    
    Color = CalculateLightning(data);
}

#endif