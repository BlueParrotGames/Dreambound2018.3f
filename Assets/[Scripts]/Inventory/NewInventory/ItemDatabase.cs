using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class ItemDatabase : MonoBehaviour
{
    List<Item> database = new List<Item>();
    JsonData itemData;

    public static ItemDatabase instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    void Start()
    {
        // Change this to a non-streamingassets file later.
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();

        //Debug.Log(database[1].title);
    }

    public Item FindItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].id == id)
                return database[i];
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item(
                (int)itemData[i]["id"],
                (string)itemData[i]["title"],
                (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["power"],
                (int)itemData[i]["stats"]["defense"],
                (int)itemData[i]["stats"]["stamina"],
                (string)itemData[i]["description"],
                (bool)itemData[i]["stackable"],
                (int)itemData[i]["rarity"],
                (string)itemData[i]["slug"]
                ));
        }
    }
}

public class Item
{
    public int id;
    public string title;
    public int value;
    public int power;
    public int defense;
    public int stamina;
    public string description;
    public bool stackable;
    public int rarity;
    public string slug;
    public Sprite sprite;

    public Item(int id, string title, int value, int power, int defense, int stamina, string description, bool stackable, int rarity, string slug)
    {
        this.id = id;
        this.title = title;
        this.value = value;
        this.power = power;
        this.defense = defense;
        this.stamina = stamina;
        this.description = description;
        this.stackable = stackable;
        this.rarity = rarity;
        this.slug = slug;
        this.sprite = Resources.Load<Sprite>("Textures/Item sprites/" + slug);
    }
    
    public Item()
    {
        this.id = -1;
    }
}
