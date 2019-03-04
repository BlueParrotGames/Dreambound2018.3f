using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(LineRenderer))]
public class Enemy : MonoBehaviour
{
    [Header("Global settings")]
    [Range(0, 50)] public int segments = 50;
    public float combatRadius;
    public bool showRange;

    LineRenderer line;
    SphereCollider triggerColl;


    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        triggerColl = GetComponent<SphereCollider>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        line.loop = true;

        triggerColl.isTrigger = true;
        triggerColl.radius = combatRadius;
    }

    public void SetPoints(float r)
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * r;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * r;
            y = 1;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / segments);
        }
    }

    void Start()
    {
        if(showRange)
            SetPoints(combatRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.TriggerCombat();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.AbandonCombat();
        }
    }
}
