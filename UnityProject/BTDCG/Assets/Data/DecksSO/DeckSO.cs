using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum DeckType{Tower, Bloon};

[CreateAssetMenu(fileName="New Deck", menuName="Cards/New Deck")]
public class DeckSO : ScriptableObject
{
    public string deckName;
    //public DeckType deckType;
    public List<CardSO> towerDeck;
    public List<CardSO> bloonDeck;
}
