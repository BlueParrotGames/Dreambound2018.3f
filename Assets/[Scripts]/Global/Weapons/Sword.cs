using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider))]
public class Sword : MonoBehaviour
{
    float damage;
    [SerializeField] GameObject floatiePrefab;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            DealDamage(coll.gameObject);
        }
    }

    void DealDamage(GameObject enemy)
    {
        damage = Random.Range(5, 8);
        Debug.Log(gameObject.name + " dealt damage to " + enemy.name + " dealing " + damage + " damage " );

        GameObject floatie = Instantiate(floatiePrefab, enemy.transform.position + new Vector3(Random.Range(-1f, 1), 1, 0), Quaternion.identity);
        TextMeshPro t = floatie.GetComponentInChildren<TextMeshPro>();
        t.text = damage.ToString();
    }
}
