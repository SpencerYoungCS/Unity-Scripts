using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    //public GameObject gameControllerObj;

    private GameController gameController;
    private List<ChampionObject> inventory;
    private TextMeshProUGUI textMesh;
    List<Vector3> onFieldPos = new List<Vector3>();
    List<Vector3> inventoryPos = new List<Vector3>();
    float distanceFromField;
    float distanceFromInventory;
    int posBeingDragged;
    string nameBeingDragged;

    public void OnDrop(PointerEventData eventData)
    {
        posBeingDragged = int.Parse(eventData.pointerDrag.name.Remove(0, this.name.Length - 1)) - 1;
        nameBeingDragged = eventData.pointerDrag.name.Remove(this.name.Length - 1);

        //as long as im not dropping an empty slot...
        for (int i = 0; i < 9; i++)
        {
            onFieldPos.Add(gameController.onfield[i].transform.position);
            inventoryPos.Add(gameController.inventoryObjects[i].transform.position);
        }

        //If dragged to bottom, sell it!
        if (this.transform.position.y < -1.6)
        {
            //Debug.Log("Sell me");
            if (nameBeingDragged == "Inventory")
            {
                gameController.ReturnToDeck(inventory[posBeingDragged]);
                inventory[posBeingDragged] = new Champion().placeHolder;
            }
            else
            {
                gameController.ReturnToDeck(gameController.champsOnField[posBeingDragged]);
                gameController.champsOnField[posBeingDragged] = new Champion().placeHolder;
            }
            gameController.sprites.UpdatePics();

        }

        for (int i = 0; i < 9; i++)
        {
            distanceFromField = Vector3.Distance(this.transform.position, onFieldPos[i]);
            distanceFromInventory = Vector3.Distance(this.transform.position, inventoryPos[i]);

            //skip checking itself!
            if (distanceFromField < 0.45)
            {
                //moving to field i
                if (nameBeingDragged == "Inventory")
                {
                    if (gameController.champsOnField[i].championName == "Empty")
                    {
                        //   Debug.Log("Swapping inventory with Field");
                        gameController.champsOnField[i] = new ChampionObject(inventory[posBeingDragged]);
                        inventory[posBeingDragged] = new Champion().placeHolder;
                        gameController.sprites.UpdatePics();
                        break;
                    }
                }
            }
            //dropping into inventory
            else if (distanceFromInventory < 0.45)
            {
                if (nameBeingDragged == "Inventory")
                {
                    if (i != posBeingDragged)
                    {
                        if (inventory[i].championName == "Empty")
                        {
                            inventory[i] = new ChampionObject(inventory[posBeingDragged]);
                            inventory[posBeingDragged] = new Champion().placeHolder;
                            gameController.sprites.UpdatePics();
                            break;
                        }
                        else
                        {
                            //swap objects
                            ChampionObject tempChamp = new ChampionObject(inventory[i]);
                            inventory[i] = new ChampionObject(inventory[posBeingDragged]);
                            inventory[posBeingDragged] = new ChampionObject(tempChamp);
                            gameController.sprites.UpdatePics();
                        }
                    }
                }
                //field to inventory
                else
                {
                    if (inventory[i].championName == "Empty")
                    {
                        inventory[i] = new ChampionObject(gameController.champsOnField[posBeingDragged]);
                        gameController.champsOnField[posBeingDragged] = new Champion().placeHolder;
                        gameController.sprites.UpdatePics();
                        break;
                    }
                    else
                    {
                        //swap objects
                        ChampionObject tempChamp = new ChampionObject(gameController.champsOnField[i]);
                        inventory[i] = new ChampionObject(gameController.champsOnField[posBeingDragged]);
                        gameController.champsOnField[posBeingDragged] = new ChampionObject(tempChamp);
                        gameController.sprites.UpdatePics();
                    }


                }
            }
        }
        textMesh.SetText(gameController.Traits());
        textMesh.ForceMeshUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GameObject.Find("Traits").GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        inventory = gameController.inventory;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
