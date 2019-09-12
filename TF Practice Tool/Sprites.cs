using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sprites : MonoBehaviour
{
    //public GameObject gameController;
    private List<ChampionObject> cardsRevealed = new List<ChampionObject>();
    private List<ChampionObject> inventory = new List<ChampionObject>();
    private List<ChampionObject> champsOnField = new List<ChampionObject>();
    private List<GameObject> options = new List<GameObject>();
    private List<GameObject> inventoryObjects = new List<GameObject>();
    private List<GameObject> onfield = new List<GameObject>();
    //this string will put into the sprite load.
    private string tempName;
    private string tempLevel;

    void Start()
    {
        options = GetComponent<GameController>().options;
        inventoryObjects = GetComponent<GameController>().inventoryObjects;
        onfield = GetComponent<GameController>().onfield;
        cardsRevealed = GetComponent<GameController>().cardsRevealed;
        inventory = GetComponent<GameController>().inventory;
        champsOnField = GetComponent<GameController>().champsOnField;
    }

    public void UpdatePics()
    {
        for(int i = 0; i < 5; i++) 
        {
            tempName = "Pictures/" + cardsRevealed[i].championName;
            if(cardsRevealed[i].championName != "Empty")
               options[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(tempName);
            else
               options[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/ItemSold");
        }
        for (int i = 0; i < 9; i++)
        {
            tempLevel = "Pictures/" + "Level" + inventory[i].level;
            tempName = "Pictures/" + inventory[i].championName + "Icon";
            if (inventory[i].championName != "Empty")
            {
                inventoryObjects[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(tempName);
                inventoryObjects[i].GetComponentsInChildren<Image>()[1].enabled = true;
                inventoryObjects[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(tempLevel);
            }
            else
            {
                inventoryObjects[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/OpenSlot");
                inventoryObjects[i].GetComponentsInChildren<Image>()[1].enabled = false;
                
            }
        }
        for (int i = 0; i < 9; i++)
        {
            tempLevel = "Pictures/" + "Level" + champsOnField[i].level;
            tempName = "Pictures/" + champsOnField[i].championName + "Icon";
            if (champsOnField[i].championName != "Empty")
            {
                onfield[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(tempName);
                onfield[i].GetComponentsInChildren<Image>()[1].enabled = true;
                onfield[i].GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(tempLevel);
            }
            else
            {
                onfield[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Pictures/OpenSlot");
                onfield[i].GetComponentsInChildren<Image>()[1].enabled = false;
            }
        }

    }
}
