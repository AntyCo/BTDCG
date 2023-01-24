using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnClock : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] CardGen cardGen;
    [SerializeField] Sprite imgUpkeep, imgMain, imgEndOfTurn;

    void Update(){
        switch(cardGen.turnOrder){
            case TurnOrder.Upkeep: {
                image.sprite=imgUpkeep;
                break;
            }
            case TurnOrder.MainPhaze: {
                image.sprite=imgMain;
                break;
            }
            case TurnOrder.EndOfTurn: {
                image.sprite=imgEndOfTurn;
                break;
            }
        }
    }
}
