using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private GameController gameController;
    public GameObject itemBeingDragged;
    public int posBeingDragged;
    public string nameBeingDragged;
    private Vector3 savedPosition;
    public void OnDrag(PointerEventData eventData)
    {
        if (nameBeingDragged == "Inventory")
        {
            if (gameController.inventory[posBeingDragged].championName != "Empty")
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                pos.z = 0;
                transform.position = pos;
            }
        }
        if (nameBeingDragged == "OnField")
        {
            if (gameController.champsOnField[posBeingDragged].championName != "Empty")
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                pos.z = 0;
                transform.position = pos;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(transform.position);
        transform.position = savedPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        posBeingDragged = int.Parse(this.name.Remove(0, this.name.Length - 1)) - 1;
        nameBeingDragged = this.name.Remove(this.name.Length - 1);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        savedPosition = transform.position;
        itemBeingDragged = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
