using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnOrder{Upkeep, MainPhaze, EndOfTurn};

public class CardGen : MonoBehaviour
{
    //float reload, maxReload=0.5f;
    [SerializeField] GameObject cardP, dcI, towerDbtn, bloonDbtn, endTurnBtn; // cardP = card prefab, dcI = "draw X cards!" icon, tower/bloon-Dbtn = are draw buttons
    [SerializeField] Text dcT; // dcI text
    [SerializeField] Transform pHand, oHand; // player's/opponent's hand's transform
    public List<CardSO> possibleCards; // list of all possible cards in database - not used, but dont delete
    public HandScript playersHand, opponentsHand; // player's/opponent's hand script
    public int cards2Draw, opCards2Draw; // how many cards to draw
    bool isOpponentDrawingRn=false;
    List<string> saidKeywords = new List<string>();

    public int currentTurn, currentTurnStage; public TurnOrder turnOrder;
    public bool doesPlayerStart, isPlayersTurnNow;

    public CardCtrl selectedCard; public LineScript selectedLine;

    void Start(){
        selectedCard = null; selectedLine = null;
        Screen.SetResolution(1920, 1080, true);
        currentTurn=0; currentTurnStage=0; turnOrder=TurnOrder.EndOfTurn; isPlayersTurnNow=doesPlayerStart;
        cards2Draw=4; opCards2Draw=4;
        //GiveCard(59);
        GiveCard(1);
        GiveCard(103);
        GiveCard(105);
        /*for(int x=0; x<possibleCards.Count; x++){
            for(int y=0; y<possibleCards[x].keywordsDesc.Count; y++){
                bool wasThere=false;
                for(int z=0; z<saidKeywords.Count; z++){
                    if(saidKeywords[z]==possibleCards[x].keywordsDesc[y]) wasThere=true;
                }
                if(!wasThere){
                    saidKeywords.Add(possibleCards[x].keywordsDesc[y]);
                    Debug.Log($"{possibleCards[x].keywordsDesc[y]} | #{x+1} - {possibleCards[x].cardName}");
                }
            }
        }*/
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

        if(currentTurn==0 && cards2Draw==0 && opCards2Draw==0){ //randomly selects who starts
            //for(int i=0; i<10; i++) Debug.Log(Random.Range(0,2));
            doesPlayerStart=true;
            isPlayersTurnNow=doesPlayerStart;
            turnOrder=TurnOrder.Upkeep; currentTurnStage=1; 
        }

        if(cards2Draw==0 && opCards2Draw==0){
            switch(turnOrder){
                case TurnOrder.Upkeep: {
                    switch(currentTurnStage){
                        case 1: {
                            if(isPlayersTurnNow == doesPlayerStart) currentTurn++;
                            currentTurnStage++;
                            break;
                        }
                        case 2: {
                            if(isPlayersTurnNow) Debug.Log("Player's bloons' clock"); else Debug.Log("Opponents's bloons' clock");
                            currentTurnStage++;
                            break;
                        }
                        case 3: {
                            if(isPlayersTurnNow) Debug.Log("Player's monkes attack"); else Debug.Log("Opponents's monkes attack");
                            currentTurnStage++;
                            break;
                        }
                        case 4: {
                            if(isPlayersTurnNow) Debug.Log("Player's monkes loose bananas"); else Debug.Log("Opponents's monkes loose bananas");
                            currentTurnStage++;
                            break;
                        }
                        case 5: {
                            if(isPlayersTurnNow) cards2Draw=2; else opCards2Draw=2;
                            currentTurnStage++;
                            break;
                        }
                        case 6: {
                            if(isPlayersTurnNow){
                                playersHand.coins=Mathf.Clamp(currentTurn, 0, 10);
                            } else{
                                opponentsHand.coins=Mathf.Clamp(currentTurn, 0, 10);
                            }
                            turnOrder=TurnOrder.MainPhaze;
                            currentTurnStage=1;
                            break;
                        }
                    }
                    break;
                }
                case TurnOrder.MainPhaze: {
                    if(selectedCard!=null && selectedLine!=null){
                        if(isPlayersTurnNow){
                            selectedCard.transform.SetParent(selectedLine.playersSpawn.transform);
                            selectedCard.transform.position=selectedLine.playersSpawn.transform.position;
                            selectedLine.playersCard=selectedCard;
                            playersHand.coins-=selectedCard.cost;
                        }
                        else{
                            selectedCard.transform.SetParent(selectedLine.opponentsSpawn.transform);
                            selectedCard.transform.position=selectedLine.opponentsSpawn.transform.position;
                            selectedLine.opponentsCard=selectedCard;
                            opponentsHand.coins-=selectedCard.cost;
                        }
                        selectedCard.transform.localScale=new Vector2(0.25f,0.25f);
                        selectedCard=null;
                        selectedLine=null;

                    }
                    break;
                }
                case TurnOrder.EndOfTurn: {
                    break;
                }
            }
        }

        endTurnBtn.SetActive(turnOrder==TurnOrder.MainPhaze && cards2Draw==0 && opCards2Draw==0 && isPlayersTurnNow);
    }

    public void GiveCard(int id){
        GameObject createdCard = (GameObject)Instantiate(cardP, this.transform.position, Quaternion.Euler(0, 0, 0), pHand);
        createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
        createdCard.GetComponent<CardCtrl>().cardOg = possibleCards[id-1];
        createdCard.GetComponent<CardCtrl>().SetCardData(playersHand, false, this);
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
                createdCard.GetComponent<CardCtrl>().SetCardData(playersHand, false, this);
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
                createdCard.GetComponent<CardCtrl>().SetCardData(playersHand, false, this);
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
            createdCard.GetComponent<CardCtrl>().SetCardData(opponentsHand, true, this);
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
            createdCard.GetComponent<CardCtrl>().SetCardData(opponentsHand, true, this);

        }
    }

    public void EndTurn(){
        turnOrder=TurnOrder.EndOfTurn; currentTurnStage=1;
    }
}
