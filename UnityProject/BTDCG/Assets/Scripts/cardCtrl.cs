using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cardCtrl : MonoBehaviour
{
    public CardSO cardOg;
    public bool infoSet=false;
    public int cost, maxHp, atk, clock, hp, banana;
    [SerializeField] Text nameT, costT, atkT, bananaT, clockT, hpT, descT, splashT;
    [SerializeField] GameObject atkI, bananaI, clockI, hpI;
    [SerializeField] Image bgImage, cardImage, rarityImage;

    void Update(){
        if(infoSet){
            infoSet=false;
            nameT.text=cardOg.cardName;
        }
    }
}
