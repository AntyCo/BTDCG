using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGen : MonoBehaviour
{
    //float reload, maxReload=0.5f;
    [SerializeField] GameObject cardP;
    [SerializeField] Transform pHand;
    public List<CardSO> possibleCards;

    /*void Update()
    {
        reload-=Time.deltaTime;
        if(reload<0 && possibleCards.Count>0){
            reload=maxReload;
            maxReload=Random.Range(0.01f, 0.3f);
            GameObject createdCard = (GameObject)Instantiate(cardP, new Vector3(Random.Range(0f, this.transform.position.x*2), (this.transform.position.y*2)+400), Quaternion.Euler(0, 0, Random.Range(0f,360f)), this.transform);
            CardSO selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

            cardCtrl card = createdCard.GetComponent<cardCtrl>();

            card.atkT.text=selectedCard.atk+"";
            card.bananaT.text=selectedCard.maxBananas+"";
            card.clockT.text=selectedCard.clock+"";
            card.hpT.text=selectedCard.health+"";
            if(selectedCard.atk==0) card.atkI.SetActive(false);
            if(selectedCard.maxBananas==0) card.bananaI.SetActive(false);
            if(selectedCard.clock==0) card.clockI.SetActive(false);
            if(selectedCard.health==0) card.hpI.SetActive(false);

            card.costT.text=selectedCard.cost+"";
            card.nameT.text=selectedCard.cardName+"";
            card.splashT.text=selectedCard.splashText;
            switch(selectedCard.rarity){
                case Rarity.Common:
                    card.bgImage.color=colorCommon;
                    break;
                case Rarity.Rare:
                    card.bgImage.color=colorRare;
                    break;
                case Rarity.Epic:
                    card.bgImage.color=colorEpic;
                    break;
                case Rarity.Legendary:
                    card.bgImage.color=colorLegendary;
                    break;
            }
            card.cardImage.sprite=selectedCard.image;
            card.descT.text="";
            for(int i=0; i<selectedCard.keywordsDesc.Count; i++){
                card.descT.text+=selectedCard.keywordsDesc[i]+"\r\n";
            }
            if(selectedCard.bonusDesc.Length>0){
                card.descT.text+=selectedCard.bonusDesc;
            }





            card.curRotation = Random.Range(0f,360f);
            card.rotationSpeed = Random.Range(-360f,360f);
            card.fallSpeed = Random.Range(180f,720f);
        }
    }*/
    public void generateCard(){
        GameObject createdCard = (GameObject)Instantiate(cardP, this.transform.position, Quaternion.Euler(0, 0, 0), pHand);
        createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
        //CardSO selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

        createdCard.GetComponent<CardCtrl>().cardOg = possibleCards[Random.Range(0, possibleCards.Count)];
        createdCard.GetComponent<CardCtrl>().SetCardData();

        /*card.atkT.text=selectedCard.atk+"";
        card.bananaT.text=selectedCard.maxBananas+"";
        card.clockT.text=selectedCard.clock+"";
        card.hpT.text=selectedCard.health+"";
        if(selectedCard.atk==0) card.atkI.SetActive(false);
        if(selectedCard.maxBananas==0) card.bananaI.SetActive(false);
        if(selectedCard.clock==0) card.clockI.SetActive(false);
        if(selectedCard.health==0) card.hpI.SetActive(false);

        card.costT.text=selectedCard.cost+"";
        card.nameT.text=selectedCard.cardName+"";
        card.splashT.text=selectedCard.splashText;
        switch(selectedCard.rarity){
            case Rarity.Common:
                card.bgImage.color=colorCommon;
                break;
            case Rarity.Rare:
                card.bgImage.color=colorRare;
                break;
            case Rarity.Epic:
                card.bgImage.color=colorEpic;
                break;
            case Rarity.Legendary:
                card.bgImage.color=colorLegendary;
                break;
        }
        card.cardImage.sprite=selectedCard.image;
        card.descT.text="";
        for(int i=0; i<selectedCard.keywordsDesc.Count; i++){
            card.descT.text+=selectedCard.keywordsDesc[i]+"\r\n";
        }
        if(selectedCard.bonusDesc.Length>0){
            card.descT.text+=selectedCard.bonusDesc;
        }*/
    }
}
