using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType { Weapon, Clothing, Trash}
    public Sprite itemImage;
    public float mass;

    public void Get()
    {
        var a = FindObjectOfType<Inventory>();
        a.itemList.Add(this);
        a.ManualUpdate();
        gameObject.SetActive(false);
    }

    public virtual void Action()
    {

    }
}
