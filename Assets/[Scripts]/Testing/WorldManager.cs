using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public List<Cloth> cloths = new List<Cloth>();
    public List<CapsuleCollider> clothColliders = new List<CapsuleCollider>();
    public List<Player> players = new List<Player>();

    public static WorldManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += (Scene arg0, Scene arg1) =>
        {
            //Debug.Log("WorldManager caught a scene change!");
            #region cloth reset
            cloths = new List<Cloth>();

            GameObject[] clothGameObjects = GameObject.FindGameObjectsWithTag("InteractiveCloth");
            foreach(GameObject g in clothGameObjects)
            {
                Cloth c = g.GetComponent<Cloth>();
                cloths.Add(c);
            }
            #endregion

            foreach(Player p in players)
            {
                p.ResetPosition();
            }
        };
    }

    private void Start()
    {
        InvokeRepeating("UpdateCloth", 0, 1f);    
    }

    void UpdateCloth()
    {
        for (int i = 0; i < cloths.Count; i++)
        {
            for (int j = 0; j < players.Count; j++)
            {
                if(!clothColliders.Contains(players[j].clothCollider))
                    clothColliders.Add(players[j].clothCollider);
                cloths[i].capsuleColliders = clothColliders.ToArray();
            }
        }
    }
}
