using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Info", menuName="Cards/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    public List<DeckSO> deckList;
    public List<CardSO> possibleCards;
    public List<CardAmmount> playersCards;
}

[System.Serializable]
public class CardAmmount{
    public CardSO whatCard;
    public int howMany;

    public CardAmmount(){

    }
}
