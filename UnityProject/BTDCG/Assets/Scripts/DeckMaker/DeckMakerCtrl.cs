using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckMakerCtrl : MonoBehaviour
{
    [SerializeField] GameObject deckHolder;
    [SerializeField] PlayerInfo playerInfo;

    public void Start(){
        if(playerInfo.playersCards.Count==0){
            Debug.Log("Player has no cards!");
            for(int i=0; i<playerInfo.possibleCards.Count; i++){
                CardAmmount cA = new CardAmmount();
                cA.whatCard=playerInfo.possibleCards[i];
                cA.howMany=0;
                playerInfo.playersCards.Add(cA);
            }
        }
    }
    
    public void EditDeck(DeckSO editedDeck){
        deckHolder.SetActive(false);
        Debug.Log($"Editing: {editedDeck.deckName}");
    }
}
