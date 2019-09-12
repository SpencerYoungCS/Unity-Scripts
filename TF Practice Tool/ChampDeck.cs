using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampDeck
{
    //public List<ChampionObject> championDeck = new List<ChampionObject>();
    public List<ChampionObject> tier1Deck = new List<ChampionObject>();
    public List<ChampionObject> tier2Deck = new List<ChampionObject>();
    public List<ChampionObject> tier3Deck = new List<ChampionObject>();
    public List<ChampionObject> tier4Deck = new List<ChampionObject>();
    public List<ChampionObject> tier5Deck = new List<ChampionObject>();

    // Start is called before the first frame update

    public ChampDeck()
    {
        Champion Champions = new Champion();
        //Generate Deck of Champions

        //tier 1
        for (int i = 0; i < 39; i++)
        {
            tier1Deck.Add(Champions.Khazix);
            tier1Deck.Add(Champions.Mordekaiser);
            tier1Deck.Add(Champions.Fiora);
            tier1Deck.Add(Champions.Warwick);
            tier1Deck.Add(Champions.Tristana);
            tier1Deck.Add(Champions.Graves);
            tier1Deck.Add(Champions.Elise);
            tier1Deck.Add(Champions.Vayne);
            tier1Deck.Add(Champions.Nidalee);
            tier1Deck.Add(Champions.Darius);
            tier1Deck.Add(Champions.Camille);
            tier1Deck.Add(Champions.Khazix);
            tier1Deck.Add(Champions.Kassadin);
            tier1Deck.Add(Champions.Garen);
        }

        //tier 2
        for (int i = 0; i < 26; i++)
        {
            tier2Deck.Add(Champions.Jayce);
            tier2Deck.Add(Champions.TwistedFate);
            tier2Deck.Add(Champions.Shen);
            tier2Deck.Add(Champions.Zed);
            tier2Deck.Add(Champions.Varus);
            tier2Deck.Add(Champions.RekSai);
            tier2Deck.Add(Champions.Lulu);
            tier2Deck.Add(Champions.Lucian);
            tier2Deck.Add(Champions.Lissandra);
            tier2Deck.Add(Champions.Braum);
            tier2Deck.Add(Champions.Blitzcrank);
            tier2Deck.Add(Champions.Pyke);
            tier2Deck.Add(Champions.Ahri);
        }

        //tier 3
        for (int i = 0; i < 21; i++)
        {
            tier3Deck.Add(Champions.Katarina);
            tier3Deck.Add(Champions.Gangplank);
            tier3Deck.Add(Champions.Poppy);
            tier3Deck.Add(Champions.Evelynn);
            tier3Deck.Add(Champions.Aatrox);
            tier3Deck.Add(Champions.Vi);
            tier3Deck.Add(Champions.Veigar);
            tier3Deck.Add(Champions.Rengar);
            tier3Deck.Add(Champions.Morgana);
            tier3Deck.Add(Champions.Kennen);
            tier3Deck.Add(Champions.Volibear);
            tier3Deck.Add(Champions.Ashe);
            tier3Deck.Add(Champions.Shyvana);
        }

        //tier 4
        for (int i = 0; i < 13; i++)
        {
            tier4Deck.Add(Champions.Leona);
            tier4Deck.Add(Champions.Jinx);
            tier4Deck.Add(Champions.Akali);
            tier4Deck.Add(Champions.Sejuani);
            tier4Deck.Add(Champions.Kindred);
            tier4Deck.Add(Champions.Brand);
            tier4Deck.Add(Champions.AurelionSol);
            tier4Deck.Add(Champions.Gnar);
            tier4Deck.Add(Champions.Draven);
            tier4Deck.Add(Champions.Chogath);
        }

        //tier 5

        for (int i = 0; i < 10; i++)
        {
            tier5Deck.Add(Champions.Yasuo);
            tier5Deck.Add(Champions.MissFortune);
            tier5Deck.Add(Champions.Swain);
            tier5Deck.Add(Champions.Kayle);
            tier5Deck.Add(Champions.Karthus);
            tier5Deck.Add(Champions.Anivia);
        }

    }


    public void Shuffle()
    {
        List<ChampionObject> temp = new List<ChampionObject>();
        int randIndex;
        while (tier1Deck.Count > 0)
        {
            randIndex = Random.Range(0, tier1Deck.Count);
            temp.Add(tier1Deck[randIndex]);
            tier1Deck.RemoveAt(randIndex);
        }
        tier1Deck = temp;

        temp = new List<ChampionObject>();
        while (tier2Deck.Count > 0)
        {
            randIndex = Random.Range(0, tier2Deck.Count);
            temp.Add(tier2Deck[randIndex]);
            tier2Deck.RemoveAt(randIndex);
        }
        tier2Deck = temp;
        temp = new List<ChampionObject>();
        while (tier3Deck.Count > 0)
        {
            randIndex = Random.Range(0, tier3Deck.Count);
            temp.Add(tier3Deck[randIndex]);
            tier3Deck.RemoveAt(randIndex);
        }
        tier3Deck = temp;
        temp = new List<ChampionObject>();
        while (tier4Deck.Count > 0)
        {
            randIndex = Random.Range(0, tier4Deck.Count);
            temp.Add(tier4Deck[randIndex]);
            tier4Deck.RemoveAt(randIndex);
        }
        tier4Deck = temp;
        temp = new List<ChampionObject>();
        while (tier5Deck.Count > 0)
        {
            randIndex = Random.Range(0, tier5Deck.Count);
            temp.Add(tier5Deck[randIndex]);
            tier5Deck.RemoveAt(randIndex);
        }
        tier5Deck = temp;



    }
}
