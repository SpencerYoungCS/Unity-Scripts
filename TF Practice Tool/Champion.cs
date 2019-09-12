﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion
{
    public ChampionObject placeHolder = new ChampionObject("Empty", null, null, null, 0, 0);
    public ChampionObject Khazix = new ChampionObject("Khazix", "Assassin", "Void", null, 1, 1);
    public ChampionObject Pyke = new ChampionObject("Pyke", "Assassin", "Pirate", null, 2, 1);
    public ChampionObject Zed = new ChampionObject("Zed", "Assassin", "Ninja", null, 2, 1);
    public ChampionObject Rengar = new ChampionObject("Rengar", "Assassin", "Wild", null, 3, 1);
    public ChampionObject Evelynn = new ChampionObject("Evelynn", "Assassin", "Demon", null, 3, 1);
    public ChampionObject Katarina = new ChampionObject("Katarina", "Assassin", "Imperial", null, 3, 1);
    public ChampionObject Akali = new ChampionObject("Akali", "Assassin", "Ninja", null, 4, 1);
    public ChampionObject Fiora = new ChampionObject("Fiora", "Blademaster", "Noble", null, 1, 1);
    public ChampionObject Shen = new ChampionObject("Shen", "Blademaster", "Ninja", null, 2, 1);
    public ChampionObject Gangplank = new ChampionObject("Gangplank", "Blademaster", "Pirate", "Gunslinger", 3, 1);
    public ChampionObject Aatrox = new ChampionObject("Aatrox", "Blademaster", "Demon", null, 3, 1);
    public ChampionObject Draven = new ChampionObject("Draven", "Blademaster", "Blademaster", null, 4, 1);
    public ChampionObject Yasuo = new ChampionObject("Yasuo", "Blademaster", "Exile", null, 5, 1);
    public ChampionObject Blitzcrank = new ChampionObject("Blitzcrank", "Brawler", "Robot", null, 2, 1);
    public ChampionObject Chogath = new ChampionObject("Chogath", "Brawler", "Void", null, 4, 1);
    public ChampionObject RekSai = new ChampionObject("Reksai", "Brawler", "Void", null, 1, 1);
    public ChampionObject Warwick = new ChampionObject("Warwick", "Brawler", "Wild", null, 3, 1);
    public ChampionObject Volibear = new ChampionObject("Volibear", "Brawler", "Glacial", null, 3, 1);
    public ChampionObject Lissandra = new ChampionObject("Lissandra", "Elementalist", "Glacial", null, 2, 1);
    public ChampionObject Kennen = new ChampionObject("Kennen", "Elementalist", "Ninja", "Yordle", 3, 1);
    public ChampionObject Brand = new ChampionObject("Brand", "Elementalist", "Demon", null, 4, 1);
    public ChampionObject Anivia = new ChampionObject("Anivia", "Elementalist", "Glacial", null, 5, 1);
    public ChampionObject Braum = new ChampionObject("Braum", "Guardian", "Glacial", null, 2, 1);
    public ChampionObject Leona = new ChampionObject("Leona", "Guardian", "Noble", null, 4, 1);
    public ChampionObject Graves = new ChampionObject("Graves", "Gunslinger", "Pirate", null, 1, 1);
    public ChampionObject MissFortune = new ChampionObject("Miss Fortune", "Pirate", "Gunslinger", null, 5, 1);
    public ChampionObject Tristana = new ChampionObject("Tristana", "Gunslinger", "Yordle", null, 1, 1);
    public ChampionObject Lucian = new ChampionObject("Lucian", "Gunslinger", "Noble", null, 2, 1);
    public ChampionObject Darius = new ChampionObject("Darius", "Knight", "Imperial", null, 1, 1);
    public ChampionObject Garen = new ChampionObject("Garen", "Noble", "Knight", null, 1, 1);
    public ChampionObject Kayle = new ChampionObject("Kayle", "Knight", "Noble", null, 5, 1);
    public ChampionObject Mordekaiser = new ChampionObject("Mordekaiser", "Knight", "Phantom", null, 1, 1);
    public ChampionObject Poppy = new ChampionObject("Poppy", "Knight", "Yordle", null, 3, 1);
    public ChampionObject Sejuani = new ChampionObject("Sejuani", "Knight", "Glacial", null, 4, 1);
    public ChampionObject Ashe = new ChampionObject("Ashe", "Ranger", "Glacial", null, 4, 1);
    public ChampionObject Kindred = new ChampionObject("Kindred", "Ranger", "Phantom", null, 4, 1);
    public ChampionObject Varus = new ChampionObject("Varus", "Ranger", "Demon", null, 2, 1);
    public ChampionObject Vayne = new ChampionObject("Vayne", "Ranger", "Noble", null, 1, 1);
    public ChampionObject Elise = new ChampionObject("Elise", "Shapeshifter", "Demon", null, 1, 1);
    public ChampionObject Gnar = new ChampionObject("Gnar", "Shapeshifter", "Wild", "Yordle", 4, 1);
    public ChampionObject Nidalee = new ChampionObject("Nidalee", "Shapeshifter", "Wild", null, 1, 1);
    public ChampionObject Shyvana = new ChampionObject("Shyvana", "Shapeshifter", "Dragon", null, 3, 1);
    public ChampionObject Swain = new ChampionObject("Swain", "Shapeshifter", "Demon", "Imperial", 5, 1);
    public ChampionObject Ahri = new ChampionObject("Ahri", "Sorcerer", "Wild", null, 2, 1);
    public ChampionObject AurelionSol = new ChampionObject("Aurelion Sol", "Sorcerer", "Dragon", null, 4, 1);
    public ChampionObject Karthus = new ChampionObject("Karthus", "Sorcerer", "Phantom", null, 5, 1);
    public ChampionObject Kassadin = new ChampionObject("Kassadin", "Sorcerer", "Void", null, 1, 1);
    public ChampionObject Lulu = new ChampionObject("Lulu", "Sorcerer", "Yordle", null, 2, 1);
    public ChampionObject Morgana = new ChampionObject("Morgana", "Sorcerer", "Demon", null, 3, 1);
    public ChampionObject TwistedFate = new ChampionObject("Twisted Fate", "Sorcerer", "Pirate", null, 2, 1);
    public ChampionObject Veigar = new ChampionObject("Veigar", "Sorcerer", "Yordle", null, 3, 1);
    public ChampionObject Camille = new ChampionObject("Camille", "Hextech", "Blademaster", null, 1, 1);
    public ChampionObject Jayce = new ChampionObject("Jayce", "Hextech", "Shapeshifter", null, 2, 1);
    public ChampionObject Vi = new ChampionObject("Vi", "Hextech", "Brawler", null, 3, 1);
    public ChampionObject Jinx = new ChampionObject("Jinx", "Hextech", "Gunslinger", null, 4, 1);
}
