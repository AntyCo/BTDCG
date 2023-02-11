using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType{Low, High, Water};

public class LineScript : MonoBehaviour
{
    public LandType landType; public CardGen cardGen; public List<CardCtrl> playersCard, opponentsCard; public CardCtrl curPCard, curOCard;
    public GameObject playersSpawn, opponentsSpawn, button;

    void Start(){
        playersCard = new List<CardCtrl>(); opponentsCard = new List<CardCtrl>();
    }

    void Update(){
        bool shouldShow=false;
        if(cardGen.selectedCard!=null){
            shouldShow=cardGen.selectedLine==null && cardGen.isPlayersTurnNow;
            
            if(cardGen.isPlayersTurnNow){
                if(cardGen.selectedCard.cardType!=CardType.support && playersCard.Count>0){
                    shouldShow=(playersCard[0].cardOg==cardGen.selectedCard.cardOg.upgradeFrom);
                }
                else if(cardGen.selectedCard.cardType==CardType.support){
                    switch(cardGen.selectedCard.cardOg.cardId){
                        case 104: case 105: case 106: case 107: case 109: case 114: case 120: case 122: {
                            if(playersCard.Count>0){
                                shouldShow=curPCard.cardType==CardType.tower;
                                switch(cardGen.selectedCard.cardOg.cardId){
                                    case 114: {
                                        for(int i=0; i<curPCard.keywords.Count; i++){
                                            if(curPCard.keywords[i].name==KeywordsKinds.Freeze) shouldShow=false;
                                        }
                                        break;
                                    }
                                    case 120: {
                                        for(int i=0; i<curPCard.keywords.Count; i++){
                                            if(curPCard.keywords[i].name==KeywordsKinds.Blazing) shouldShow=false;
                                        }
                                        break;
                                    }
                                    default: break;
                                }
                            } else shouldShow=false;
                            break;
                        }
                        
                        case 115: case 116: case 117: case 118: case 119: {
                            if(playersCard.Count>0){
                                shouldShow=curPCard.cardType==CardType.bloon;
                            } else shouldShow=false;
                            break;
                        }
                        
                        default: {
                            break;
                        }
                    }
                }
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
            curPCard=null;
            while(playersCard.Count>0){
                handScript.discard.Add(playersCard[0].cardOg);
                Destroy(playersCard[0].gameObject);
                playersCard.RemoveAt(0);
            }
        }
        else{
            curOCard=null;
            while(opponentsCard.Count>0){
                handScript.discard.Add(opponentsCard[0].cardOg);
                Destroy(opponentsCard[0].gameObject);
                opponentsCard.RemoveAt(0);
            }
        }
    }
}
