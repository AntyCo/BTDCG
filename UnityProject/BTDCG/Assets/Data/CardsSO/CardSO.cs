using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType{tower, bloon, support};
//public enum CardKeywords{Ultravision, LPP, Splash, Blazing, Slow, Freeze, DOT, Boomerang, BigBoom};
public enum Rarity{Common, Rare, Epic, Legendary};
public enum CardTypeSpecific{primary, military, magic, support, bloons, tSupport, bSupport, oSupport, hero};

[CreateAssetMenu(fileName="New Card", menuName="Cards/New Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    //[Multiline] public string description;
    public CardType cardType;
    public Rarity rarity;
    public CardTypeSpecific specificType;
    public Sprite image;

    //[Multiline] public string CardDesc;

    //public CardSO upgradeFrom;


    public int cost, atk, maxBananas, clock, health;
    [Multiline] public string bonusDesc;
    [Multiline] public string splashText;
    //public List<CardKeywords> keywordsList;
    public List<string> keywordsDesc;

    /*[Header("Below are just Keywords that need an stat")]
    public bool useAtkSplash; public int splash;

    [Header("stuff not shown to players")]
    public List<string> towerFunctions; 
    public List<string> atkType; 
    public List<string> whenPlayed; 
    public List<string> whenDestroyed; 
    public List<string> startOfTurn; 
    public List<string> endOfTurn;
    public List<string> whenUpgraded;*/
}