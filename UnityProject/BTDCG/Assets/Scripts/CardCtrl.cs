using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCtrl : MonoBehaviour
{
    public CardSO cardOg; public HandScript handOwner; public CardGen cardGen;
    bool infoSet=false;
    public bool ableToBeSelected=false, isSelected=false, upsideDown;
    public int cost, maxHp, atk, clock, hp, banana;
    public CardType cardType; public CardTypeSpecific cardTypeSpecific;
    [SerializeField] Text nameT, costT, atkT, bananaT, clockT, hpT, descT, splashT, typeT;
    [SerializeField] GameObject atkI, bananaI, clockI, hpI, a2BScolor, cardBack;
    [SerializeField] Image bgImage, cardImage, rarityImage;
    [SerializeField] Color colorCommon, colorRare, colorEpic, colorLegendary, catPrimary, catMilitary, catSupport, catMagic, bloons, tSupport, bSupport, hero;

    void Update(){
        if(!infoSet) return;
        ableToBeSelected=(cardGen.selectedCard==null && cardGen.turnOrder==TurnOrder.MainPhaze && cardGen.isPlayersTurnNow==true && handOwner.coins>=cost);

        a2BScolor.SetActive(ableToBeSelected || isSelected);
        if(ableToBeSelected || isSelected){
            if(isSelected) a2BScolor.GetComponent<Image>().color=colorLegendary;
            else a2BScolor.GetComponent<Image>().color=colorCommon;
        }

        costT.text=cost+"";
        atkT.text=atk+"";
        bananaT.text=banana+"";
        clockT.text=clock+"";
        hpT.text=hp+"";

        if(atk==0) atkI.SetActive(false);
        if(banana==0) bananaI.SetActive(false);
        if(clock==0) clockI.SetActive(false);
        if(hp==0) hpI.SetActive(false);

        cardBack.SetActive(upsideDown);
    }

    public void SetCardData(HandScript hO, bool spawnUD, CardGen cardG){
        cardGen=cardG;
        upsideDown = spawnUD;
        handOwner=hO;
        nameT.text=cardOg.cardName;
        splashT.text=cardOg.splashText;
        typeT.text=cardOg.typeText;
        cardImage.sprite=cardOg.image;
        cost=cardOg.cost;
        atk=cardOg.atk;
        clock=cardOg.clock;
        maxHp=cardOg.health;
        hp=maxHp;
        banana=cardOg.maxBananas;

        switch(cardOg.rarity){ //setting rarity color
            case Rarity.Common: {
                rarityImage.color = colorCommon;
                break;
            }
            case Rarity.Rare: {
                rarityImage.color = colorRare;
                break;
            }
            case Rarity.Epic: {
                rarityImage.color = colorEpic;
                break;
            }
            case Rarity.Legendary: {
                rarityImage.color = colorLegendary;
                break;
            }
        }
        cardType=cardOg.cardType;
        cardTypeSpecific=cardOg.specificType;

        switch(cardOg.specificType){ //setting backgrounds as card types (specific)
            case CardTypeSpecific.primary: {
                bgImage.color = catPrimary;
                break;
            }
            case CardTypeSpecific.military: {
                bgImage.color = catMilitary;
                break;
            }
            case CardTypeSpecific.magic: {
                bgImage.color = catMagic;
                break;
            }
            case CardTypeSpecific.support: {
                bgImage.color = catSupport;
                break;
            }
            case CardTypeSpecific.bloons: {
                bgImage.color = bloons;
                break;
            }
            case CardTypeSpecific.tSupport: {
                bgImage.color = tSupport;
                break;
            }
            case CardTypeSpecific.oSupport: {
                bgImage.color = tSupport;
                break;
            } //to change
            case CardTypeSpecific.bSupport: {
                bgImage.color = bSupport;
                break;
            }
            case CardTypeSpecific.hero: {
                bgImage.color = hero;
                break;
            }
        }

        descT.text="";
            
        for(int i=0; i<cardOg.keywordsDesc.Count; i++){
            descT.text+=cardOg.keywordsDesc[i]+"\r\n";
        }
        if(cardOg.bonusDesc.Length>0){
            descT.text+=cardOg.bonusDesc;
        }
        infoSet=true;

        if(!infoSet) return;
        costT.text=cost+"";
        atkT.text=atk+"";
        bananaT.text=banana+"";
        clockT.text=clock+"";
        hpT.text=hp+"";

        if(atk==0) atkI.SetActive(false);
        if(banana==0) bananaI.SetActive(false);
        if(clock==0) clockI.SetActive(false);
        if(hp==0) hpI.SetActive(false);

        cardBack.SetActive(upsideDown);
    }

    public void Selected(){
        Debug.Log("Click!");
        if(ableToBeSelected || isSelected) isSelected=!isSelected;
        if(isSelected) cardGen.selectedCard=this;
        else cardGen.selectedCard=null;
    }
}