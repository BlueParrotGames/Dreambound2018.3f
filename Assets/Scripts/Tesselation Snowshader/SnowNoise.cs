using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNoise : MonoBehaviour
{
    public Shader snowFallShader;
    private Material snowFallMaterial;
    MeshRenderer meshRenderer;

    [Range(0.001f, 0.1f)]
    [SerializeField] float flakeAmount = 0.01f;

    [Range(0f, 1f)]
    [SerializeField] float flakeOpacity = 0.08f;

    void Start()
    {
        if(snowFallShader == null)
        {
            snowFallShader = Shader.Find("Hidden/Snowfall");
        }
        meshRenderer = GetComponent<MeshRenderer>();
        snowFallMaterial = new Material(snowFallShader);
    }

    // Update is called once per frame
    void Update()
    {
        snowFallMaterial.SetFloat("_FlakeAmount", flakeAmount);
        snowFallMaterial.SetFloat("_FlakeOpacity", flakeOpacity);

        RenderTexture snow = (RenderTexture)meshRenderer.material.GetTexture("_Splat");
        RenderTexture t = RenderTexture.GetTemporary(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(snow, t, snowFallMaterial);
        Graphics.Blit(t, snow);

        meshRenderer.material.SetTexture("_Splat", snow);

        RenderTexture.ReleaseTemporary(t);
    }
}
