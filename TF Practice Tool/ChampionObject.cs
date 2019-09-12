using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionObject  
{
    public string championName;
    public string classType1;
    public string classType2;
    public string classType3;
    public int level;
    public int cost;
    
    public ChampionObject()
    {
        this.championName = "Empty";
        this.classType1 = null;
        this.classType2 = null;
        this.classType3 = null;
        this.cost = 0;
        this.level = 0;
    }
    public ChampionObject(string championName, string classType1, string classType2, string classType3, int cost, int level)
    {
        this.championName = championName;
        this.classType1 = classType1;
        this.classType2 = classType2;
        this.classType3 = classType3;
        this.cost = cost;
        this.level = level;
    }

    public ChampionObject(ChampionObject cloneThis)
    {
        this.championName = cloneThis.championName;
        this.classType1 = cloneThis.classType1;
        this.classType2 = cloneThis.classType2;
        this.classType3 = cloneThis.classType3;
        this.cost = cloneThis.cost;
        this.level = cloneThis.level;
    }

    public ChampionObject(ChampionObject cloneThis, int level)
    {
        this.championName = cloneThis.championName;
        this.classType1 = cloneThis.classType1;
        this.classType2 = cloneThis.classType2;
        this.classType3 = cloneThis.classType3;
        this.cost = cloneThis.cost;
        this.level = level;
    }

}




