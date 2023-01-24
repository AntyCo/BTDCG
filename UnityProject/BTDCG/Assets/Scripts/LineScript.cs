using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType{Low, High, Water};

public class LineScript : MonoBehaviour
{
    public LandType landType; public CardGen cardGen; public CardCtrl playersCard, opponentsCard;
    public GameObject playersSpawn, opponentsSpawn, button;

    void Update(){
        bool shouldShow=cardGen.selectedCard!=null && cardGen.selectedLine==null && cardGen.isPlayersTurnNow;
        if(cardGen.selectedCard!=null) if(cardGen.selectedCard.cardType!=CardType.support && playersCard!=null) shouldShow=false;
        button.SetActive(shouldShow);
    }

    public void SelectThisLane(){
        cardGen.selectedLine=this;
    }
}
