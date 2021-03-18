using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour, I_Interactable
{
    [SerializeField] string itemText;
    public void OnInteract()
    {
        if (itemText != "")
        {
            TextTyper.instance.TypeText(itemText);
        }
        else
            Debug.Log(this.gameObject.name + " has no Interaction text set");
    }
}
