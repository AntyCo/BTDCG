using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCtrl : MonoBehaviour
{
    public CardSO cardOg; public HandScript handOwner;
    bool infoSet=false;
    public bool ableToBeSelected=false, upsideDown;
    public int cost, maxHp, atk, clock, hp, banana;
    [SerializeField] Text nameT, costT, atkT, bananaT, clockT, hpT, descT, splashT;
    [SerializeField] GameObject atkI, bananaI, clockI, hpI, a2BScolor, cardBack;
    [SerializeField] Image bgImage, cardImage, rarityImage;
    [SerializeField] Color colorCommon, colorRare, colorEpic, colorLegendary, catPrimary, catMilitary, catSupport, catMagic, bloons, tSupport, bSupport, hero;

    void Update(){
        if(!infoSet) return;
        a2BScolor.SetActive(ableToBeSelected);
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

    public void SetCardData(HandScript hO, bool spawnUD){
        upsideDown = spawnUD;
        handOwner=hO;
        nameT.text=cardOg.cardName;
        splashT.text=cardOg.splashText;
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
        ableToBeSelected=!ableToBeSelected;
    }
}