using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGen : MonoBehaviour
{
    //float reload, maxReload=0.5f;
    [SerializeField] GameObject cardP, dcI, towerDbtn, bloonDbtn; // cardP = card prefab, dcI = "draw X cards!" icon, tower/bloon-Dbtn = are draw buttons
    [SerializeField] Text dcT; // dcI text
    [SerializeField] Transform pHand, oHand; // player's/opponent's hand's transform
    public List<CardSO> possibleCards; // list of all possible cards in database - not used, but dont delete
    public HandScript playersHand, opponentsHand; // player's/opponent's hand script
    public int cards2Draw, opCards2Draw; // how many cards to draw
    bool isOpponentDrawingRn=false;

    void Start(){
        cards2Draw=4; opCards2Draw=4;
    }

    void Update(){
        if(cards2Draw>0){
            dcI.SetActive(true); towerDbtn.SetActive(true); bloonDbtn.SetActive(true);
            dcT.text=$"Draw\r\n{cards2Draw} cards!";
        }
        else{
            dcI.SetActive(false); towerDbtn.SetActive(false); bloonDbtn.SetActive(false);
        }
        if(opCards2Draw>0 && !isOpponentDrawingRn){
            isOpponentDrawingRn=true;
            StartCoroutine(OpponentDrawCards(opCards2Draw));
        }
    }

    public void DrawCard(bool fromTowerDeck){
        if(cards2Draw>0){
            if(fromTowerDeck){
                if(playersHand.deck.Count==0) return;
                GameObject createdCard = (GameObject)Instantiate(cardP, this.transform.position, Quaternion.Euler(0, 0, 0), pHand);
                createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
                //CardSO selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

                int randCard = Random.Range(0, playersHand.deck.Count);
                createdCard.GetComponent<CardCtrl>().cardOg = playersHand.deck[randCard];
                playersHand.cardsInHand.Add(playersHand.deck[randCard]);
                playersHand.deck.RemoveAt(randCard);
                cards2Draw--;
                createdCard.GetComponent<CardCtrl>().SetCardData(playersHand, false);
            }
            else{
                if(playersHand.bloonDeck.Count==0) return;
                GameObject createdCard = (GameObject)Instantiate(cardP, this.transform.position, Quaternion.Euler(0, 0, 0), pHand);
                createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
                //CardSO selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

                int randCard = Random.Range(0, playersHand.bloonDeck.Count);
                createdCard.GetComponent<CardCtrl>().cardOg = playersHand.bloonDeck[randCard];
                playersHand.cardsInHand.Add(playersHand.bloonDeck[randCard]);
                playersHand.bloonDeck.RemoveAt(randCard);
                cards2Draw--;
                createdCard.GetComponent<CardCtrl>().SetCardData(playersHand, false);
            }
        }
    }

    IEnumerator OpponentDrawCards(int howMany){
        for(int i=0; i<howMany; i++){
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
            if(Random.Range(0,2)==0) OpponentDrawCard(true); else OpponentDrawCard(false);
        }
        isOpponentDrawingRn=false;
    }

    public void OpponentDrawCard(bool fromTowerDeck){
        if(opponentsHand.deck.Count==0 && opponentsHand.bloonDeck.Count==0) return;
        opCards2Draw--;
        if(fromTowerDeck){
            if(opponentsHand.deck.Count==0) OpponentDrawCard(false);
            GameObject createdCard = (GameObject)Instantiate(cardP, this.transform.position, Quaternion.Euler(0, 0, 0), oHand);
            createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
            //CardSO selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

            int randCard = Random.Range(0, opponentsHand.deck.Count);
            createdCard.GetComponent<CardCtrl>().cardOg = opponentsHand.deck[randCard];
            opponentsHand.cardsInHand.Add(opponentsHand.deck[randCard]);
            opponentsHand.deck.RemoveAt(randCard);
            createdCard.GetComponent<CardCtrl>().SetCardData(opponentsHand, true);
        }
        else{
            if(opponentsHand.bloonDeck.Count==0) OpponentDrawCard(true);
            GameObject createdCard = (GameObject)Instantiate(cardP, this.transform.position, Quaternion.Euler(0, 0, 0), oHand);
            createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
            //CardSO selectedCard = possibleCards[Random.Range(0, possibleCards.Count)];

            int randCard = Random.Range(0, opponentsHand.bloonDeck.Count);
            createdCard.GetComponent<CardCtrl>().cardOg = opponentsHand.bloonDeck[randCard];
            opponentsHand.cardsInHand.Add(opponentsHand.bloonDeck[randCard]);
            opponentsHand.bloonDeck.RemoveAt(randCard);
            createdCard.GetComponent<CardCtrl>().SetCardData(opponentsHand, true);

        }
    }
}
