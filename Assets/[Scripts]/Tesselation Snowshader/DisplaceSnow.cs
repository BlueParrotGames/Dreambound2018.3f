using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaceSnow : MonoBehaviour
{
    RenderTexture splatmap;
    [SerializeField] Shader drawShader;
    Material drawMaterial;
    Material baseMaterial;

    [SerializeField] GameObject terrain;
    [SerializeField] Transform[] transforms;

    RaycastHit hit;
    RaycastHit renderHit;
    int layerMask;

    [Range(0, 2)]
    [SerializeField] float brushSize;

    [Range(0, 1)]
    [SerializeField] float brushStrength;

    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);
    }

    void SetMaterialRenderer()
    {
        //Debug.Log("Calling Set Material Renderer");
        baseMaterial = terrain.GetComponent<MeshRenderer>().material;

        if (!baseMaterial.HasProperty("_Splat"))
        {
            this.enabled = false;
            return;
        }

        if (baseMaterial.HasProperty("_Splat") && baseMaterial.GetTexture("_Splat") == null)
            baseMaterial.SetTexture("_Splat", splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
        else if(baseMaterial.HasProperty("_Splat") && baseMaterial.GetTexture("_Splat") != null)
            splatmap = (RenderTexture)baseMaterial.GetTexture("_Splat");


        //GameObject[] terrains = GameObject.FindGameObjectsWithTag("Ground");

        // replace this with a list and set all the splatmaps at the start of the game.
        // Above statement won't work, since this leads to repetitive calling of lists, everytime an object that can edit the snow touches snow.
        // Instead, added simple if-statement to check if a splatmap is present, if so, create new one, otherwise, skip.
    }

    void Update()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, out renderHit, 5f, layerMask))
        {
            if (renderHit.transform.gameObject == terrain)
            {
                //Debug.Log("Current Flooring and terrain match.");
            }
            else
            {
                terrain = renderHit.transform.gameObject;
                SetMaterialRenderer();
            }
        }

        if (terrain == null || !baseMaterial.HasProperty("_Splat"))
            return;

        for (int i = 0; i < transforms.Length; i++)
        {
            if (Physics.Raycast(transforms[i].position, -Vector3.up, out hit, 1f, layerMask))
            {
                drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength", brushStrength);
                drawMaterial.SetFloat("_Size", brushSize);
                RenderTexture t = RenderTexture.GetTemporary(splatmap.width, splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatmap, t);
                Graphics.Blit(t, splatmap, drawMaterial);

                RenderTexture.ReleaseTemporary(t);
            }
        }
    }
}
