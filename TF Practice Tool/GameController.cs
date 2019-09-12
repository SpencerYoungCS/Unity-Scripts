using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class GameController : MonoBehaviour
{
    //initializations
    private ChampDeck deck = new ChampDeck();
    public Sprites sprites;
    public List<ChampionObject> inventory = new List<ChampionObject>(); //champions in hand
    public List<ChampionObject> cardsRevealed = new List<ChampionObject>(); //cards offered
    public List<ChampionObject> champsOnField = new List<ChampionObject>(); //champions on the field
    private ChampionObject overflowPlaceholder = new ChampionObject(null, null, null, null, 0, 1);
    public List<GameObject> options = new List<GameObject>();
    public List<GameObject> inventoryObjects = new List<GameObject>();
    public List<GameObject> onfield = new List<GameObject>();
    private int numOfSameUnit;
    private int[] sameUnitPosField = new int[5] { 0, 0, 0, 0, 0 };
    private int[] sameUnitPosInventory = new int[5] { 0, 0, 0, 0, 0 };
    public bool pairOnField;
    public string btnClicked;
    public int level;
    public int exp;
    public int goldUsed;
    private int rand;

    void Start()
    {
        level = 1;
        exp = 0;
        //Create deck and shuffle
        deck.Shuffle();
        //-------------------------

        //i know its not efficient to search for it, but i understand that the overall goal of the game will be on a small scale. otherwise, i will serialize it in the editor.
        sprites = GetComponent<Sprites>();
        options.Add(GameObject.Find("Option1"));
        options.Add(GameObject.Find("Option2"));
        options.Add(GameObject.Find("Option3"));
        options.Add(GameObject.Find("Option4"));
        options.Add(GameObject.Find("Option5"));

        //these are the direct gameobject (slots) on the field
        inventoryObjects.Add(GameObject.Find("Inventory1"));
        inventoryObjects.Add(GameObject.Find("Inventory2"));
        inventoryObjects.Add(GameObject.Find("Inventory3"));
        inventoryObjects.Add(GameObject.Find("Inventory4"));
        inventoryObjects.Add(GameObject.Find("Inventory5"));
        inventoryObjects.Add(GameObject.Find("Inventory6"));
        inventoryObjects.Add(GameObject.Find("Inventory7"));
        inventoryObjects.Add(GameObject.Find("Inventory8"));
        inventoryObjects.Add(GameObject.Find("Inventory9"));

        onfield.Add(GameObject.Find("OnField1"));
        onfield.Add(GameObject.Find("OnField2"));
        onfield.Add(GameObject.Find("OnField3"));
        onfield.Add(GameObject.Find("OnField4"));
        onfield.Add(GameObject.Find("OnField5"));
        onfield.Add(GameObject.Find("OnField6"));
        onfield.Add(GameObject.Find("OnField7"));
        onfield.Add(GameObject.Find("OnField8"));
        onfield.Add(GameObject.Find("OnField9"));

        //Initial Hand
        for (int i = 0; i < 5; i++)
        {
            cardsRevealed.Add(deck.tier1Deck[0]);
            deck.tier1Deck.RemoveAt(0);
        }

        for (int i = 0; i < 9; i++)
        {
            champsOnField.Add(new Champion().placeHolder);
            inventory.Add(new Champion().placeHolder);
        }

        StartCoroutine(StartLater());
    }

    void Update()
    {
    }

    public void ShuffleDeck()
    {
        deck.Shuffle();
    }

    public void Refresh()
    {
        goldUsed += 2;
        //first return champions into card pool
        for (int i = 0; i < 5; i++)
        {
            if (cardsRevealed[i].championName != "Empty")
            {
                //return
                switch (cardsRevealed[i].cost)
                {
                    case 1:
                        deck.tier1Deck.Add(cardsRevealed[i]);
                        break;
                    case 2:
                        deck.tier2Deck.Add(cardsRevealed[i]);
                        break;
                    case 3:
                        deck.tier3Deck.Add(cardsRevealed[i]);
                        break;
                    case 4:
                        deck.tier4Deck.Add(cardsRevealed[i]);
                        break;
                    case 5:
                        deck.tier5Deck.Add(cardsRevealed[i]);
                        break;
                }
            }
        }

        //clear out the hand
        for (int i = 0; i < 5; i++)
        {
            cardsRevealed.RemoveAt(0);
        }

        //shuffle 
        deck.Shuffle();

        //draw new hand

        //if level 1 or 2

        for (int i = 0; i < 5; i++)
        {
            rand = UnityEngine.Random.Range(1, 101);
            switch (level)
            {
                case 1:
                    cardsRevealed.Add(deck.tier1Deck[0]);
                    deck.tier1Deck.RemoveAt(0);
                    break;
                case 2:
                    cardsRevealed.Add(deck.tier1Deck[0]);
                    deck.tier1Deck.RemoveAt(0);
                    break;
                case 3:
                    if (rand > 35)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 5)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }

                    break;
                case 4:
                    if (rand > 50)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 15)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }
                    break;
                case 5:
                    if (rand > 63)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 28)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);

                    }
                    else if (rand > 3)
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier4Deck[0]);
                        deck.tier4Deck.RemoveAt(0);
                    }
                    break;
                case 6:
                    if (rand > 76)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 41)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);

                    }
                    else if (rand > 11)
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }
                    else if (rand > 1)
                    {
                        cardsRevealed.Add(deck.tier4Deck[0]);
                        deck.tier4Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier5Deck[0]);
                        deck.tier5Deck.RemoveAt(0);
                    }
                    break;
                case 7:
                    if (rand > 80)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 50)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);

                    }
                    else if (rand > 17)
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }
                    else if (rand > 2)
                    {
                        cardsRevealed.Add(deck.tier4Deck[0]);
                        deck.tier4Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier5Deck[0]);
                        deck.tier5Deck.RemoveAt(0);
                    }
                    break;
                case 8:
                    if (rand > 85)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 60)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);

                    }
                    else if (rand > 25)
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }
                    else if (rand > 5)
                    {
                        cardsRevealed.Add(deck.tier4Deck[0]);
                        deck.tier4Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier5Deck[0]);
                        deck.tier5Deck.RemoveAt(0);
                    }

                    break;
                case 9:
                    if (rand > 90)
                    {
                        cardsRevealed.Add(deck.tier1Deck[0]);
                        deck.tier1Deck.RemoveAt(0);
                    }
                    else if (rand > 75)
                    {
                        cardsRevealed.Add(deck.tier2Deck[0]);
                        deck.tier2Deck.RemoveAt(0);

                    }
                    else if (rand > 40)
                    {
                        cardsRevealed.Add(deck.tier3Deck[0]);
                        deck.tier3Deck.RemoveAt(0);
                    }
                    else if (rand > 10)
                    {
                        cardsRevealed.Add(deck.tier4Deck[0]);
                        deck.tier4Deck.RemoveAt(0);
                    }
                    else
                    {
                        cardsRevealed.Add(deck.tier5Deck[0]);
                        deck.tier5Deck.RemoveAt(0);
                    }
                    break;
            }
        }
        sprites.UpdatePics();


    }

    public void BuyEXP()
    {
        exp += 4;
        goldUsed += 4;
        if (exp < 176)
        {
            if (exp < 112)
            {
                if (exp < 66)
                {
                    if (exp < 36)
                    {
                        if (exp < 18)
                        {
                            if (exp < 8)
                            {
                                if (exp < 2)
                                    level = 1;
                                else
                                    level = 2;
                            }
                            else
                                level = 3;
                        }
                        else
                            level = 4;
                    }
                    else
                        level = 5;
                }
                else
                    level = 6;
            }
            else
                level = 7;
        }
        else
            level = 9;
    }

    public void Buy(string buttonName)
    {
        switch (buttonName)
        {
            case "Option1":
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (inventory[i].championName == "Empty")
                        {
                            //here i create a new champion object because if it make 
                            //the inventory spot equal the cardsrevealed, any modification will point to
                            //the main deck
                            inventory[i] = new ChampionObject(cardsRevealed[0].championName,
                                cardsRevealed[0].classType1, cardsRevealed[0].classType2, cardsRevealed[0].classType3, cardsRevealed[0].cost
                                , cardsRevealed[0].level);
                            goldUsed += cardsRevealed[0].cost;
                            cardsRevealed[0] = new Champion().placeHolder;
                            //option1.GetComponent<Image>().sprite = "Graves";
                            Combine();
                            break;
                        }
                        else
                        {
                            if (i == 8)
                            {
                                overflowPlaceholder.championName = cardsRevealed[0].championName;
                                if (CombineOverflow(overflowPlaceholder))
                                {
                                    goldUsed += cardsRevealed[0].cost;
                                    cardsRevealed[0] = new Champion().placeHolder;

                                }
                                else
                                    Debug.Log("No more space!");

                            }
                        }
                    }
                    break;
                }
            case "Option2":
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (inventory[i].championName == "Empty")
                        {
                            inventory[i] = new ChampionObject(cardsRevealed[1].championName,
                                cardsRevealed[1].classType1, cardsRevealed[1].classType2, cardsRevealed[1].classType3, cardsRevealed[1].cost
                                , cardsRevealed[1].level);

                            goldUsed += cardsRevealed[1].cost;
                            cardsRevealed[1] = new Champion().placeHolder;
                            Combine();
                            break;
                        }
                        else
                        {
                            if (i == 8)
                            {
                                overflowPlaceholder.championName = cardsRevealed[1].championName;
                                if (CombineOverflow(overflowPlaceholder))
                                {
                                    goldUsed += cardsRevealed[1].cost;
                                    cardsRevealed[1] = new Champion().placeHolder;

                                }
                                else
                                    Debug.Log("No more space!");
                            }
                        }
                    }
                    break;
                }
            case "Option3":
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (inventory[i].championName == "Empty")
                        {
                            inventory[i] = new ChampionObject(cardsRevealed[2].championName,
                                cardsRevealed[2].classType1, cardsRevealed[2].classType2, cardsRevealed[2].classType3, cardsRevealed[2].cost
                                , cardsRevealed[2].level);
                            goldUsed += cardsRevealed[2].cost;
                            cardsRevealed[2] = new Champion().placeHolder;
                            Combine();
                            break;
                        }
                        else
                        {
                            if (i == 8)
                            {
                                overflowPlaceholder.championName = cardsRevealed[2].championName;
                                if (CombineOverflow(overflowPlaceholder))
                                {
                                    goldUsed += cardsRevealed[2].cost;
                                    cardsRevealed[2] = new Champion().placeHolder;

                                }
                                else
                                    Debug.Log("No more space!");
                            }
                        }
                    }
                    break;
                }
            case "Option4":
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (inventory[i].championName == "Empty")
                        {
                            inventory[i] = new ChampionObject(cardsRevealed[3].championName,
                            cardsRevealed[3].classType1, cardsRevealed[3].classType2, cardsRevealed[3].classType3, cardsRevealed[3].cost
                            , cardsRevealed[3].level);

                            goldUsed += cardsRevealed[3].cost;
                            cardsRevealed[3] = new Champion().placeHolder;
                            Combine();
                            break;
                        }
                        else
                        {
                            if (i == 8)
                            {
                                overflowPlaceholder.championName = cardsRevealed[3].championName;
                                if (CombineOverflow(overflowPlaceholder))
                                {
                                    goldUsed += cardsRevealed[3].cost;
                                    cardsRevealed[3] = new Champion().placeHolder;

                                }
                                else
                                    Debug.Log("No more space!");
                            }
                        }
                    }
                    break;
                }
            case "Option5":
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (inventory[i].championName == "Empty")
                        {
                            inventory[i] = new ChampionObject(cardsRevealed[4].championName,
                            cardsRevealed[4].classType1, cardsRevealed[4].classType2, cardsRevealed[4].classType3, cardsRevealed[4].cost
                            , cardsRevealed[4].level);

                            goldUsed += cardsRevealed[4].cost;
                            cardsRevealed[4] = new Champion().placeHolder;
                            Combine();
                            break;
                        }
                        else
                        {
                            if (i == 8)
                            {
                                overflowPlaceholder.championName = cardsRevealed[4].championName;
                                if (CombineOverflow(overflowPlaceholder))
                                {
                                    goldUsed += cardsRevealed[4].cost;
                                    cardsRevealed[4] = new Champion().placeHolder;

                                }
                                else
                                    Debug.Log("No more space!");
                            }
                        }
                    }
                    break;
                }
            case "Empty":
                {
                    Debug.Log("Champion is sold");
                    break;
                }
        }
        //here ill write one more combine (this mostly checks if anything has hit level 3.
        //i can also add another exception in the combine function if i have time.
        Combine();
    }

    public void Combine()
    {
        bool combinedOnField = false;
        numOfSameUnit = 0;
        //check for any pair on the field first
        for (int i = 0; i < 8; i++)
        {
            pairOnField = false;
            if (champsOnField[i].championName != "Empty")
            {
                for (int k = i + 1; k < 9; k++)
                {
                    if (champsOnField[i].championName == champsOnField[k].championName
                        && champsOnField[i].level == champsOnField[k].level)
                    {
                        pairOnField = true;
                        //save positions of the pair on the field
                        //Debug.Log("Pair on field detected!");
                        sameUnitPosField[0] = i;
                        sameUnitPosField[1] = k;
                        break;
                    }
                }
                if (pairOnField)
                    break;
            }
        }

        //combine if there is a pair of field
        if (pairOnField)
        {
            for (int i = 0; i < 9; i++)
            {
                if (inventory[i].championName != "Empty")
                {
                    if (inventory[i].championName == champsOnField[sameUnitPosField[0]].championName
                    && inventory[i].level == champsOnField[sameUnitPosField[0]].level)
                    {
                        champsOnField[sameUnitPosField[0]].cost *= 3;
                        champsOnField[sameUnitPosField[0]].level++;
                        champsOnField[sameUnitPosField[1]] = new Champion().placeHolder;
                        inventory[i] = new Champion().placeHolder;
                        break;
                    }
                }
            }
        }
        //TO DO: ADD CHECKING FIELD!! THERE MIGHT BE JUST 1 ON THE FIELD!!
            for (int i = 0; i < 8; i++)
            {
                numOfSameUnit = 0;
                if (inventory[i].championName != "Empty")
                {
                    for (int j = i + 1; j < 9; j++)
                    {
                        //if a pair is found, remember its location and keep count of it
                        if (inventory[i].championName == inventory[j].championName
                            && inventory[i].level == inventory[j].level)
                        {
                        //check the field. combine if one is found
                        for(int k = 0; k < 9; k++)
                        {
                            if(inventory[i].championName == champsOnField[k].championName 
                                && inventory[i].level == champsOnField[k].level)
                            {
                                combinedOnField = true;
                                champsOnField[k].cost *= 3;
                                champsOnField[k].level++;
                                inventory[i] = new Champion().placeHolder;
                                inventory[j] = new Champion().placeHolder;
                                break;
                            }
                        }
                            numOfSameUnit++;
                        if (numOfSameUnit == 2 && combinedOnField == false)
                            {
                                inventory[i].cost *= 3;
                                inventory[i].level++;
                                inventory[sameUnitPosInventory[0]] = new Champion().placeHolder;
                                inventory[j] = new Champion().placeHolder;

                                break;
                            }
                            else
                            {
                                sameUnitPosInventory[0] = j;
                            }
                        }
                    }
                }
            }
        sprites.UpdatePics();
    }


    //this combines when the inventory is full
    public bool CombineOverflow(ChampionObject Overflow)
    {
        numOfSameUnit = 0;
        //check for any pair on the field first
        for (int i = 0; i < 9; i++)
        {
            //check for any pair on the field first
            if (champsOnField[i].championName != "Empty")
            {
                if (champsOnField[i].championName == Overflow.championName
                    && champsOnField[i].level == Overflow.level)
                {
                    numOfSameUnit++;
                    if (numOfSameUnit == 2)
                    {
                        champsOnField[sameUnitPosField[0]].cost *= 3;
                        champsOnField[sameUnitPosField[0]].level++;
                        champsOnField[i] = new Champion().placeHolder;
                        return true;
                    }
                    else
                        sameUnitPosField[0] = i;
                }
            }
        }

        //combine if one exists in the field
        if (numOfSameUnit == 1)
        {
            for (int i = 0; i < 9; i++)
            {
                if (inventory[i].championName != "Empty")
                {
                    if (inventory[i].championName == Overflow.championName
                    && inventory[i].level == Overflow.level)
                    {
                        champsOnField[sameUnitPosField[0]].cost *= 3;
                        champsOnField[sameUnitPosField[0]].level++;
                        inventory[i] = new Champion().placeHolder;
                        return true;
                    }
                }
            }
        }

        //if none are in the field, just check the inventory normally
        numOfSameUnit = 0;
        for (int i = 0; i < 9; i++)
        {
            if (inventory[i].championName != "Empty")
            {
                if (inventory[i].championName == Overflow.championName
                    && inventory[i].level == Overflow.level)
                {
                    numOfSameUnit++;
                    if (numOfSameUnit == 2)
                    {
                        Debug.Log("overflow triple found");
                        inventory[sameUnitPosInventory[0]].cost *= 3;
                        inventory[sameUnitPosInventory[0]].level++;
                        inventory[i] = new Champion().placeHolder;
                        return true;
                    }
                    else
                    {
                        sameUnitPosInventory[0] = i;
                    }
                }
            }
        }
        return false;
    }

    public void ReturnToDeck(ChampionObject toReturn)
    {
        //ChampionObject temp = new ChampionObject(toReturn.championName,toReturn.classType1,toReturn.classType2,toReturn.classType3,toReturn.cost,toReturn.level);
        if (toReturn.championName != "Empty")
        {
            switch (toReturn.cost)
            {
                //make sure to add the appropriate champions into their respective decks
                //the number of champions is simply 3 ^ (level - 1)
                case 1:
                    for (int i = 0; i < Mathf.Pow(3, toReturn.level - 1); i++)
                        deck.tier1Deck.Add(new ChampionObject(toReturn, 1));
                    break;
                case 2:
                    for (int i = 0; i < Mathf.Pow(3, toReturn.level - 1); i++)
                        deck.tier2Deck.Add(new ChampionObject(toReturn, 1));
                    break;
                case 3:
                    for (int i = 0; i < Mathf.Pow(3, toReturn.level - 1); i++)
                        deck.tier3Deck.Add(new ChampionObject(toReturn, 1));
                    break;
                case 4:
                    for (int i = 0; i < Mathf.Pow(3, toReturn.level - 1); i++)
                        deck.tier4Deck.Add(new ChampionObject(toReturn, 1));
                    break;
                case 5:
                    for (int i = 0; i < Mathf.Pow(3, toReturn.level - 1); i++)
                        deck.tier5Deck.Add(new ChampionObject(toReturn, 1));
                    break;
            }
        }
    }

    public void debug()
    {
        for (int i = 0; i < 9; i++)
        {
            Debug.Log("onField[" + i + "] Champion Name =" + champsOnField[i].championName + " level " + champsOnField[i].level);
        }
        for (int i = 0; i < 9; i++)
        {
            Debug.Log("inventory[" + i + "] Champion Name =" + inventory[i].championName + " level " + inventory[i].level);
        }
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("cardsRevealed[" + i + "] Champion Name =" + cardsRevealed[i].championName);
        }
    }

    public string Traits()
    {
        List<Tuple<string, int>> allTraits = new List<Tuple<string, int>>();
        string temp = null;
        bool found1 = false;
        bool found2 = false;
        bool found3 = false;

        for (int i = 0; i < 9; i++)
        {
            found1 = false;
            found2 = false;
            found3 = false;
            if (champsOnField[i].championName != "Empty")
            {
                if (allTraits.Count < 1)
                {
                    allTraits.Add(new Tuple<string, int>(champsOnField[i].classType1, 1));
                    allTraits.Add(new Tuple<string, int>(champsOnField[i].classType2, 1));
                    if (champsOnField[i].classType3 != null)
                        allTraits.Add(new Tuple<string, int>(champsOnField[i].classType3, 1));
                }

                else
                {
                    //i need a temp number because the size of alltraits can increase in size during the loop
                    int countTemp = allTraits.Count;
                    for (int j = 0; j < countTemp; j++)
                    {
                        if (champsOnField[i].classType1 == allTraits[j].Item1)
                        {
                            found1 = true;
                            allTraits[j] = new Tuple<string, int>(champsOnField[i].classType1, allTraits[j].Item2 + 1);
                        }
                        if (champsOnField[i].classType2 == allTraits[j].Item1)
                        {
                            found2 = true;
                            allTraits[j] = new Tuple<string, int>(champsOnField[i].classType2, allTraits[j].Item2 + 1);
                        }
                        if (champsOnField[i].classType3 == allTraits[j].Item1)
                        {
                            found3 = true;
                            allTraits[j] = new Tuple<string, int>(champsOnField[i].classType2, allTraits[j].Item2 + 1);
                        }


                        //if i reach the end of the list and its not found... then add it!
                        if (j == countTemp - 1 && found1 == false)
                        {
                            //Debug.Log("Adding Type1: "+ champsOnField[i].classType1);
                            allTraits.Add(new Tuple<string, int>(champsOnField[i].classType1, 1));
                        }
                        if (j == countTemp - 1 && found2 == false)
                        {
                            //Debug.Log("Adding Type2: "+ champsOnField[i].classType2);
                            allTraits.Add(new Tuple<string, int>(champsOnField[i].classType2, 1));
                        }
                        if (champsOnField[i].classType3 != null)
                        {
                            if (j == countTemp - 1 && found3 == false)
                            {
                                allTraits.Add(new Tuple<string, int>(champsOnField[i].classType3, 1));
                            }
                        }


                    }
                }
            }
        }

        //generate the list into text now!
        for (int i = 0; i < allTraits.Count; i++)
        {
            switch (allTraits[i].Item1)
            {
                case "Assassin":
                    if (allTraits[i].Item2 < 3)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Demon":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else if (allTraits[i].Item2 < 4)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Blademaster":
                    if (allTraits[i].Item2 < 3)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    else if (allTraits[i].Item2 < 6)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/9" + System.Environment.NewLine;
                    break;
                case "Dragon":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    break;
                case "Brawler":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else if (allTraits[i].Item2 < 4)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Exile":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/1" + System.Environment.NewLine;
                    break;
                case "Glacial":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else if (allTraits[i].Item2 < 4)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Elementalist":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    break;
                case "Hextech":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    break;
                case "Guardian":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    break;
                case "Gunslinger":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else if (allTraits[i].Item2 < 4)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Imperial":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    break;
                case "Knight":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else if (allTraits[i].Item2 < 4)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Ninja":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    break;
                case "Ranger":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    break;
                case "Pirate":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    break;
                case "Shapeshifter":
                    if (allTraits[i].Item2 < 3)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Phantom":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    break;
                case "Sorcerer":
                    if (allTraits[i].Item2 < 3)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;
                case "Robot":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/1" + System.Environment.NewLine;
                    break;
                case "Void":
                    temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    break;
                case "Wild":
                    if (allTraits[i].Item2 < 2)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/2" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/4" + System.Environment.NewLine;
                    break;
                case "Yordle":
                    if (allTraits[i].Item2 < 3)
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/3" + System.Environment.NewLine;
                    else
                        temp += allTraits[i].Item1 + " " + allTraits[i].Item2 + "/6" + System.Environment.NewLine;
                    break;

            }
        }
        return temp;
    }

    IEnumerator StartLater()
    {
        yield return new WaitForEndOfFrame();
        sprites.UpdatePics();
    }

}