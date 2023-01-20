using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public List<CardSO> deck, bloonDeck, discard, bloonDiscard, cardsInHand;
    [SerializeField] RectTransform rT;
    [SerializeField] bool isPlayers;

    void Update(){
        if(isPlayers){
            if((Input.mousePosition.y/Screen.height*1080)<rT.anchoredPosition.y+200)
                rT.anchoredPosition=new Vector2(0, 200);
            else
                rT.anchoredPosition=new Vector2(0, -100);
        }
    }
}
