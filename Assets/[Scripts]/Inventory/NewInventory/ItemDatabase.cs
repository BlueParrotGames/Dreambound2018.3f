using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //database.Add(new Item(
            //    (int)itemData[i]["id"],
            //    (string)itemData[i]["title"],
            //    (int)itemData[i]["value"],
            //    (int)itemData[i]["stats"]["strength"],
            //    (int)itemData[i]["stats"]["defense"],
            //    (int)itemData[i]["stats"]["stamina"],
            //    (string)itemData[i]["description"],
            //    (bool)itemData[i]["stackable"],
            //    (int)itemData[i]["rarity"],
            //    (string)itemData[i]["slug"]
            //    ));

            Item item = new Item(
                (int)itemData[i]["id"],
                (string)itemData[i]["title"],
                (int)itemData[i]["value"],
                -1,
                -1,
                -1,
                (string)itemData[i]["description"],
                (bool)itemData[i]["stackable"],
                (int)itemData[i]["rarity"],
                (string)itemData[i]["slug"]
                );

            try
            { item.intelligence = (int)itemData[i]["stats"]["intelligence"]; }
            catch
            { item.intelligence = -1; }

            try
            { item.strength = (int)itemData[i]["stats"]["strength"]; }
            catch
            { item.strength = -1; }

            try
            { item.stamina = (int)itemData[i]["stats"]["stamina"]; }
            catch
            { item.stamina = -1; }

            database.Add(item);
        }
    }
}

public class Item
{
    public int id;
    public string title;
    public int value;
    public int strength;
    public int intelligence;
    public int stamina;
    public string description;
    public bool stackable;
    public int rarity;
    public string slug;
    public Sprite sprite;
    public Color rarityColor;

    public Item(int id, string title, int value, int strength, int intelligence, int stamina, string description, bool stackable, int rarity, string slug)
    {
        this.id = id;
        this.title = title;
        this.value = value;
        this.strength = strength;
        this.intelligence = intelligence;
        this.stamina = stamina;
        this.description = description;
        this.stackable = stackable;
        this.rarity = rarity;
        this.slug = slug;
        this.sprite = Resources.Load<Sprite>("Textures/Item sprites/" + slug);

        switch(rarity)
        {
            case 0:
                {
                    ColorUtility.TryParseHtmlString("#747474", out rarityColor);
                    break;
                }
            case 1:
                {
                    ColorUtility.TryParseHtmlString("#FFFFFF", out rarityColor);
                    break;
                }
            case 2:
                {
                    ColorUtility.TryParseHtmlString("#1CB047", out rarityColor);
                    break;
                }
            case 3:
                {
                    ColorUtility.TryParseHtmlString("#2C44B7", out rarityColor);
                    break;
                }
            case 4:
                {
                    ColorUtility.TryParseHtmlString("#9342B5", out rarityColor);
                    break;
                }
            case 5:
                {
                    ColorUtility.TryParseHtmlString("#FFD600", out rarityColor);
                    break;
                }
        }
    }
    
    public Item()
    {
        this.id = -1;
    }
}
