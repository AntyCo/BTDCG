using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType{Low, High, Water};

public class LineScript : MonoBehaviour
{
    public LandType landType; public CardGen cardGen; public List<CardCtrl> playersCard, opponentsCard;
    public GameObject playersSpawn, opponentsSpawn, button;

    void Start(){
        playersCard = new List<CardCtrl>(); opponentsCard = new List<CardCtrl>();
    }

    void Update(){
        bool shouldShow=false;
        if(cardGen.selectedCard!=null){
            shouldShow=cardGen.selectedLine==null && cardGen.isPlayersTurnNow;
            
            if(cardGen.selectedCard.cardType!=CardType.support && playersCard.Count>0){
                shouldShow=(playersCard[0].cardOg==cardGen.selectedCard.cardOg.upgradeFrom);
            }

            if(playersCard.Count==0 && cardGen.selectedCard.cardOg.upgradeFrom!=null) shouldShow=false;
        }
        button.SetActive(shouldShow);
    }

    public void SelectThisLane(){
        cardGen.selectedLine=this;
    }

    public void DiscardAll(bool fromPlayer, HandScript handScript){
        if(fromPlayer){
            while(playersCard.Count>0){
                handScript.discard.Add(playersCard[0].cardOg);
                Destroy(playersCard[0].gameObject);
                playersCard.RemoveAt(0);
            }
        }
        else{
            while(opponentsCard.Count>0){
                handScript.discard.Add(opponentsCard[0].cardOg);
                Destroy(opponentsCard[0].gameObject);
                opponentsCard.RemoveAt(0);
            }
        }
    }
}
