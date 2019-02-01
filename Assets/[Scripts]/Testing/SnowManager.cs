using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManager : MonoBehaviour
{
    public bool setSnow = false;
    bool _setSnow;

    public float snowLevel = 0f;
    float _snowLevel;

    public float frostLevel = 1f;
    float _frostLevel;

    [SerializeField] List<SnowMaterial> snowList;
    [SerializeField] List<ParticleSystem> snowFallParticles;

    [SerializeField] float timer;

    void Update()
    {
        if(setSnow != _setSnow)
        {
            ToggleSnow(setSnow);
            _setSnow = setSnow;
        }

        if (snowLevel != _snowLevel)
        {
            UpdateSnowLevel(snowLevel);
            _snowLevel = snowLevel;
        }

        if(frostLevel != _frostLevel)
        {
            UpdateFrostLevel(frostLevel);
            _frostLevel = frostLevel;
        }

        if (setSnow)
        {
            timer += 1 * Time.deltaTime;
            for (int i = 0; i < snowFallParticles.Count; i++)
            {
                var e = snowFallParticles[i].emission;
                if (e.rateOverTimeMultiplier >= 200f)
                    return;
                e.rateOverTimeMultiplier = timer * 2;
            }
        }
        else
        {
            if(timer >= 0)
            {
                timer -= 1 * Time.deltaTime;
                for (int i = 0; i < snowFallParticles.Count; i++)
                {
                    var e = snowFallParticles[i].emission;
                    e.rateOverTimeMultiplier = timer * 2;
                }
            }
        }
    }

    void ToggleSnow(bool b)
    {
        foreach(SnowMaterial m in snowList)
            m.ToggleShader(b);
    }

    void UpdateSnowLevel(float h)
    {
        foreach(SnowMaterial m in snowList)
            m.SetSnowLevel(h);
    }

    void UpdateFrostLevel(float f)
    {
        foreach (SnowMaterial m in snowList)
            m.SetFrostLevel(f);
    }
}

[System.Serializable]
public class SnowMaterial
{
    public Shader normalShader;
    public Shader snowShader;
    public List<Material> materials = new List<Material>();

    public void ToggleShader(bool b)
    {
        foreach(Material mat in materials)
        {
            if(mat != null)
            {
                if (b)
                    mat.shader = snowShader;
                else
                    mat.shader = normalShader;
            }
        }
    }

    public void SetSnowLevel(float h)
    {
        foreach(Material m in materials)
            m.SetFloat("_SnowHeight", h);
    }

    public void SetFrostLevel(float f)
    {
        foreach (Material m in materials)
            m.SetFloat("_FrostHeight", f);
    }
}
