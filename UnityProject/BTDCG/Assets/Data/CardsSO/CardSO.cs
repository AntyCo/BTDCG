using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType{tower, bloon};
public enum SupportType{none, normal, quickplay};
//public enum CardKeywords{Ultravision, LPP, Splash, Blazing, Slow, Freeze, DOT, Boomerang, BigBoom};
public enum Rarity{Common, Rare, Epic, Legendary};
public enum CardTypeSpecific{primary, military, magic, support, bloons, tSupport, bSupport, hero};

[CreateAssetMenu(fileName="New Card", menuName="Cards/New Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    public int cardId;
    //[Multiline] public string description;
    public CardType cardType;
    public SupportType supportType;
    public Rarity rarity;
    public CardTypeSpecific specificType;
    public Sprite image;
    public CardSO upgradeFrom;

    //[Multiline] public string CardDesc;

    //public CardSO upgradeFrom;


    public int cost, atk, maxBananas, clock, health;
    public string typeText;
    [Multiline] public string bonusDesc;
    [Multiline] public string splashText;
    //public List<CardKeywords> keywordsList;
    [SerializeField] public List<KeywordStats> keywordsDesc;

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

public enum KeywordsKinds{
    Bank,
    BigBoom,
    BlackHole,
    Blazing,
    Boomerang,
    Camo,
    Ceramic,
    CeramicBuster,
    Discount,
    DOT,
    Double,
    Empower,
    Feed,
    Fortibuster,
    Fortified,
    Freeze,
    FrostImmune,
    Hover,
    Interest,
    Leaded,
    LPP,
    Magic,
    MOAB,
    MOABBuster,
    MOABSlow,
    NoGrow,
    Omegapopper,
    PrimaryBuff,
    PurpleImmune,
    Recover,
    Reveal,
    Scattershot,
    Sentry,
    ShinobiBuff,
    Slow,
    Snipe,
    Spawn,
    Speed,
    SpikeSetter,
    Splash,
    SplashImmune,
    Teleport,
    Ultravision,
    Village,
    Watercraft,
    WOF,
};

[System.Serializable]
public class KeywordStats{
    public KeywordsKinds name;
    public int tier, secondaryTier;

    public KeywordStats(){

    }
}