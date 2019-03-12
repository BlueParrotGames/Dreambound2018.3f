using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private Item item;
    [SerializeField] GameObject tooltip;

    [SerializeField] ContentSizeFitter[] contentSizeFitters;
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text stats;


    private void Start()
    {
        tooltip.SetActive(false);
    }

    private void Update()
    {
        if(tooltip.activeInHierarchy)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Active(Item item)
    {
        this.item = item;
        title.text = item.title;
        string formattedText = "";

        #region oh god make it stop
        if (item.strength > 0)
            formattedText += string.Format("+{0} Strength", item.strength);
        if (item.intelligence > 0)
            formattedText += string.Format("\n+{0} Intelligence", item.intelligence);
        if (item.stamina > 0)
            formattedText += string.Format("\n+{0} Stamina", item.stamina);

        if (formattedText == "")
            stats.enabled = false;
        else
        {
            stats.enabled = true;
            stats.text = formattedText;
        }
        #endregion

        description.text = "\"" + item.description + "\"";
        title.color = item.rarityColor;


        tooltip.SetActive(true);

        foreach(Transform c in transform.GetComponentInChildren<Transform>())
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)c);
        }
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }
}
