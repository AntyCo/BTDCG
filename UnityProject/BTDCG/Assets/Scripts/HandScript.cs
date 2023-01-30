using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public DeckSO selectedDeck;
    public List<CardSO> deck, bloonDeck, discard, bloonDiscard;
    public List<CardCtrl> cardsInHand;
    [SerializeField] RectTransform rT;
    [SerializeField] bool isPlayers;
    public int coins, heroHp;
    [SerializeField] Text coinText, hpText;

    void Start(){
        if(selectedDeck!=null){
            deck = new List<CardSO>(); bloonDeck = new List<CardSO>();
            for(int i=0; i<selectedDeck.towerDeck.Count; i++){
                deck.Add(selectedDeck.towerDeck[i]);
            }
            for(int i=0; i<selectedDeck.bloonDeck.Count; i++){
                bloonDeck.Add(selectedDeck.bloonDeck[i]);
            }
        }
    }

    void Update(){
        coinText.text=coins*100+"";
        hpText.text=heroHp+"";
        if(isPlayers){
            if((Input.mousePosition.y/Screen.height*1080)<rT.anchoredPosition.y+200)
                rT.anchoredPosition=new Vector2(0, 200);
            else
                rT.anchoredPosition=new Vector2(0, -100);
        }
    }
}
