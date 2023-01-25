using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public List<CardSO> deck, bloonDeck, discard, bloonDiscard, cardsInHand;
    [SerializeField] RectTransform rT;
    [SerializeField] bool isPlayers;
    public int coins, heroHp;
    [SerializeField] Text coinText, hpText;

    void Update(){
        coinText.text=coins+"";
        hpText.text=heroHp+"";
        if(isPlayers){
            if((Input.mousePosition.y/Screen.height*1080)<rT.anchoredPosition.y+200)
                rT.anchoredPosition=new Vector2(0, 200);
            else
                rT.anchoredPosition=new Vector2(0, -100);
        }
    }
}
