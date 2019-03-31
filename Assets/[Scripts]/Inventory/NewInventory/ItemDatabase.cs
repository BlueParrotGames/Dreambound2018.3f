using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;
using System.Reflection;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> database = new List<Item>();
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
                (string)itemData[i]["slug"],
                (string)itemData[i]["type"]
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

[Serializable]
public class Item
{
    public int id;
    public string title;
    public string slug;
    public string description;

    public int value;
    public int strength;
    public int intelligence;
    public int stamina;

    public bool stackable;
    public string itemType;

    public int rarity;
    public Color rarityColor;

    public Sprite sprite;
    public GameObject itemPrefab;
    public string hitPower;

    // Only assigned for armor
    public SkinnedMeshRenderer mesh;

    public Item(int id, string title, int value, int strength, int intelligence, int stamina, string description, bool stackable, int rarity, string slug, string itemType)
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
        this.itemType = itemType;
        this.sprite = Resources.Load<Sprite>("Textures/Item sprites/" + slug);
        string[] seperatedSlug = slug.Split('_');

        //for (int i = 0; i < seperatedSlug.Length; i++)
        //    Debug.Log(seperatedSlug[i]);

        if(this.itemType != "weapon" && this.itemType != "consumable")
        {
            Debug.Log("I'm armor :D");
            itemPrefab = Resources.Load<GameObject>("Items/" + seperatedSlug[0]);
            mesh = itemPrefab.transform.Find(seperatedSlug[1]).GetComponent<SkinnedMeshRenderer>();
        }
        else
        {
            Debug.Log("I'm not armor :D");
            this.itemPrefab = Resources.Load<GameObject>("Items/" + slug);
        }

        switch (rarity)
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

[CustomPropertyDrawer(typeof(Item))]
public class ItemEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        
        EditorGUI.PropertyField(position, property, label, true);
        if (property.isExpanded)
        {
            //EditorGUI.PropertyField(position, property, label, true);
            Rect buttonRect = new Rect(position.xMin + 30f, position.yMax - 20f, position.width - 30f, 20f);
            if(property.serializedObject.targetObject.name == "Game Manager")
            {
                if (GUI.Button(buttonRect, "Add me"))
                {
                    Item item = GetTargetObjectOfProperty(property) as Item;
                    Debug.Log(item.id);
                    try
                    {
                        Player.instance.inventory.AddItem(item.id);
                    }
                    catch(Exception e)
                    {
                        Debug.LogError("There's probably no player in the scene!");
                    }
                }
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
            return EditorGUI.GetPropertyHeight(property) + 20f;

        return EditorGUI.GetPropertyHeight(property);
    }

    public static object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        if (prop == null) return null;

        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements)
        {
            if (element.Contains("["))
            {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue_Imp(obj, elementName, index);
            }
            else
            {
                obj = GetValue_Imp(obj, element);
            }
        }
        return obj;
    }

    private static object GetValue_Imp(object source, string name)
    {
        if (source == null)
            return null;
        var type = source.GetType();

        while (type != null)
        {
            var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f != null)
                return f.GetValue(source);

            var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p != null)
                return p.GetValue(source, null);

            type = type.BaseType;
        }
        return null;
    }

    private static object GetValue_Imp(object source, string name, int index)
    {
        var enumerable = GetValue_Imp(source, name) as System.Collections.IEnumerable;
        if (enumerable == null) return null;
        var enm = enumerable.GetEnumerator();
        //while (index-- >= 0)
        //    enm.MoveNext();
        //return enm.Current;

        for (int i = 0; i <= index; i++)
        {
            if (!enm.MoveNext()) return null;
        }
        return enm.Current;
    }
}