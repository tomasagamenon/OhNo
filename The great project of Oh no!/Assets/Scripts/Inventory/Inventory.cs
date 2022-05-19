using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform hand;
    public float timeToVanish;
    public List<Item> itemList;
    public List<Image> inventoryItemsImages;
    private int selectedItem = 0;
    private Item itemInHand;
    private DialogueUI dialogue;
    private bool interact = true;

    private void Start()
    {
        inventoryItemsImages[selectedItem].transform.parent.GetChild(1).gameObject.SetActive(true);
        dialogue = FindObjectOfType<DialogueUI>();
    }

    public void ManualUpdate()
    {
        if (itemList.Count > inventoryItemsImages.Count)
            Drop(itemList[0], true);
        for (int i = itemList.Count; i > 0; i--)
        {
            itemList[i - 1].GetComponent<Collider>().enabled = false;
            itemList[i - 1].GetComponent<Rigidbody>().useGravity = false;
            if (!inventoryItemsImages[i - 1].IsActive())
                inventoryItemsImages[i - 1].gameObject.SetActive(true);
            inventoryItemsImages[i - 1].sprite = itemList[i - 1].itemImage;
        }
        ChangeObjectInHand();
    }

    public void Drop(Item item, bool drop)
    {
        item.GetComponent<Collider>().enabled = true;
        item.GetComponent<Rigidbody>().useGravity = true;
        if (itemInHand == item)
            itemInHand = null;
        itemList.Remove(item);
        for(int i = 0; i < inventoryItemsImages.Count; i++)
        {
            inventoryItemsImages[i].sprite = null;
            if (i < itemList.Count)
                inventoryItemsImages[i].sprite = itemList[i].itemImage;
            else inventoryItemsImages[i].gameObject.SetActive(false);
        }
        if (selectedItem < itemList.Count)
            itemInHand = itemList[selectedItem];
        item.gameObject.SetActive(true);
        if (drop)
            item.gameObject.transform.position = hand.transform.position;
        else
            item.GetComponent<Rigidbody>().AddForce(hand.forward * 1000 / (item.mass + 0.1f));
    }
    private void LateUpdate()
    {
        if (itemInHand != null)
        {
            itemInHand.gameObject.transform.position = hand.position;
            itemInHand.gameObject.transform.rotation = hand.rotation;
        }
        
        if (dialogue.DialogueBox.activeSelf)
        {
            interact = false;
            foreach (Image image in inventoryItemsImages)
            {
                Color color = image.color;
                color.a = 0;
                image.color = color;
                color = image.transform.parent.GetChild(1).GetComponent<Image>().color;
                color.a = 0;
                image.transform.parent.GetChild(1).GetComponent<Image>().color = color;
                color = image.transform.parent.GetComponent<Image>().color;
                color.a = 0;
                image.transform.parent.GetComponent<Image>().color = color;
                image.transform.parent.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if (!interact) interact = true;
    }

    private void FixedUpdate()
    {
        if (interact)
        {
            if (itemInHand != null)
            {
                if (itemInHand.gameObject.activeSelf != true)
                    itemList[selectedItem].gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.G))
                    Drop(itemInHand, true);
                itemInHand.Action();
                if (Input.GetButtonDown("Fire2"))
                {
                    Drop(itemInHand, false);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (selectedItem <= 0)
                    selectedItem = inventoryItemsImages.Count - 1;
                else selectedItem--;
                ChangeObjectInHand();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (selectedItem >= inventoryItemsImages.Count - 1)
                    selectedItem = 0;
                else selectedItem++;
                ChangeObjectInHand();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedItem = 0;
                ChangeObjectInHand();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedItem = 1;
                ChangeObjectInHand();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedItem = 2;
                ChangeObjectInHand();
            }
            else if (inventoryItemsImages[0].color.a > 0)
                StartCoroutine(VanishImages(-1));
        }
    }

    private void ChangeObjectInHand()
    {
        StopAllCoroutines();
        foreach (Image image in inventoryItemsImages)
        {
            Color color = image.color;
            color.a = 1;
            image.color = color;
            color = image.transform.parent.GetChild(1).GetComponent<Image>().color;
            color.a = 1;
            image.transform.parent.GetChild(1).GetComponent<Image>().color = color;
            color = image.transform.parent.GetComponent<Image>().color;
            color.a = 0.5f;
            image.transform.parent.GetComponent<Image>().color = color;
            image.transform.parent.GetChild(1).gameObject.SetActive(false);
        }
        inventoryItemsImages[selectedItem].transform.parent.GetChild(1).gameObject.SetActive(true);
        if (itemInHand != null)
        {
            if (itemList.Contains(itemInHand))
                itemInHand.gameObject.SetActive(false);
            itemInHand = null;
        }
        if (itemList.Count > selectedItem)
            itemInHand = itemList[selectedItem];
    }

    IEnumerator VanishImages(int sign)
    {
        yield return new WaitForSeconds(timeToVanish);

        Vanish(sign);
    }

    public void Vanish(int sign)
    {
        foreach (Image image in inventoryItemsImages)
        {
            Color color = image.color;
            color.a += Time.deltaTime * sign;
            image.color = color;
            var c = image.transform.parent.GetChild(1).GetComponent<Image>();
            Color colorC = c.color;
            colorC.a += Time.deltaTime * sign;
            c.color = colorC;
            var p = image.transform.parent.GetComponent<Image>();
            Color colorP = p.color;
            colorP.a += Time.deltaTime * sign;
            p.color = colorP;
            if (c.color.a <= 0)
            {
                StopAllCoroutines();
            }
        }
    }

    public bool IHaveThis(DialogueObject dialogueObject)
    {
        if (dialogueObject.ObjectThatINeed == "")
            return true;
        if (itemList != null)
        {
            foreach (Item item in itemList)
            {
                if (item.name == dialogueObject.ObjectThatINeed)
                    return true;
            }
            return false;
        }
        else return false;
    }
}
