using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardShowcase : MonoBehaviour
{
    public CardSO cardOg;
    public int ammount;
    bool infoSet=false;
    public bool ableToBeSelected=false, isSelected=false;
    public int cost, maxHp, atk, clock, hp, banana;
    public List<KeywordStats> keywords;
    public CardType cardType; public CardTypeSpecific cardTypeSpecific;
    [SerializeField] Text nameT, costT, atkT, bananaT, clockT, hpT, descT, splashT, typeT, ammountT;
    [SerializeField] GameObject atkI, bananaI, clockI, hpI, a2BScolor, cardBack;
    [SerializeField] Image bgImage, cardImage, rarityImage;
    [SerializeField] Color colorCommon, colorRare, colorEpic, colorLegendary, catPrimary, catMilitary, catSupport, catMagic, bloons, tSupport, bSupport, hero;

    void Update(){
        if(!infoSet) return;

        /*ableToBeSelected=true;
        if(cardOg.upgradeFrom!=null){
            ableToBeSelected=false;
            for(int i=0; i<cardGen.lines.Count; i++){
                if(cardGen.lines[i].playersCard.Count!=0){
                    if(cardOg.upgradeFrom==cardGen.lines[i].playersCard[cardGen.lines[i].playersCard.Count-1].cardOg) ableToBeSelected=true;
                }
            }
        }
        ableToBeSelected=(cardGen.selectedCard==null && cardGen.turnOrder==TurnOrder.MainPhaze && cardGen.isPlayersTurnNow==true && handOwner.coins>=cost && ableToBeSelected);

        if (lineItIsOn!=null) for(int i=0; i<lineItIsOn.playersCard.Count; i++){
            if(lineItIsOn.playersCard[i]==this) ableToBeSelected=false;
        }

        a2BScolor.SetActive(ableToBeSelected || isSelected);
        if(ableToBeSelected || isSelected){
            if(isSelected) a2BScolor.GetComponent<Image>().color=colorLegendary;
            else a2BScolor.GetComponent<Image>().color=colorCommon;
        }*/

        descT.text="";
            
        for(int i=0; i<keywords.Count; i++){
            descT.text+=keywords[i].name;
            if(keywords[i].tier>0) descT.text+=" "+keywords[i].tier;
            descT.text+="\r\n";
        }
        if(cardOg.bonusDesc.Length>0){
            descT.text+=cardOg.bonusDesc;
        }

        costT.text=cost+"";
        atkT.text=atk+"";
        bananaT.text=banana+"";
        clockT.text=clock+"";
        hpT.text=hp+"";
        ammountT.text=ammount+"";

        atkI.SetActive(atk!=0);
        bananaI.SetActive(banana!=0);
        clockI.SetActive(clock!=0);
        hpI.SetActive(hp!=0);

        cardBack.SetActive(ammount==0);
    }

    public void SetCardData(){
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
        //keywords=cardOg.keywordsDesc;
        for(int i=0; i<cardOg.keywordsDesc.Count; i++){
            KeywordStats kS = new KeywordStats();
            kS.name=cardOg.keywordsDesc[i].name;
            kS.tier=cardOg.keywordsDesc[i].tier;
            kS.secondaryTier=cardOg.keywordsDesc[i].secondaryTier;
            keywords.Add(kS);
        }

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
            
        for(int i=0; i<keywords.Count; i++){
            descT.text+=keywords[i].name;
            if(keywords[i].tier>0) descT.text+=" "+keywords[i].tier;
            descT.text+="\r\n";
        }
        if(cardOg.bonusDesc.Length>0){
            descT.text+=cardOg.bonusDesc;
        }
        infoSet=true;

        //if(!infoSet) return;
        costT.text=cost+"";
        atkT.text=atk+"";
        bananaT.text=banana+"";
        clockT.text=clock+"";
        hpT.text=hp+"";
        ammountT.text=ammount+"";

        if(atk==0) atkI.SetActive(false);
        if(banana==0) bananaI.SetActive(false);
        if(clock==0) clockI.SetActive(false);
        if(hp==0) hpI.SetActive(false);

        cardBack.SetActive(ammount==0);
    }

    public void Selected(){
        Debug.Log("Click!");
        //if(ableToBeSelected || isSelected) isSelected=!isSelected;
    }
}
