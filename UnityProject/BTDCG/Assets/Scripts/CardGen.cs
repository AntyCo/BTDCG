using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TurnOrder{Upkeep, MainPhaze, EndOfTurn};

public class CardGen : MonoBehaviour
{
    //okay, time to clean up the code a bit
    [SerializeField] GameObject cardP, dcI, towerDbtn, bloonDbtn, endTurnBtn; // cardP = card prefab, dcI = draw card Icon, xDbtn = draw buttons from both decks
    [SerializeField] Text dcT; // draw card Text
    [SerializeField] Transform pHand, oHand; // player's and opponet's hands
    public List<CardSO> possibleCards; // card database
    public HandScript playersHand, opponentsHand; // connections to hands of both players
    public int cards2Draw, opCards2Draw; // how many cards need to be drawn
    bool isOpponentDrawingRn=false;

    public int currentTurn, currentTurnStage; public TurnOrder turnOrder; // Turn stuff
    public bool doesPlayerStart, isPlayersTurnNow; // More turn stuff

    public CardCtrl selectedCard; public LineScript selectedLine; // Selecting... Soon will be changed to dragging

    public List<LineScript> lines;

    void Start(){
        selectedCard = null; selectedLine = null;
        Screen.SetResolution(1920, 1080, true);
        currentTurn=0; currentTurnStage=0; turnOrder=TurnOrder.EndOfTurn; isPlayersTurnNow=doesPlayerStart;
        cards2Draw=4; opCards2Draw=4;
        //for(int i=1; i<=4; i++) GiveCard(i);
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
                            if(isPlayersTurnNow){
                                for(int i=0; i<lines.Count; i++){
                                    Debug.Log("");
                                    if(lines[i].playersCard.Count>0){
                                        Debug.Log("A");
                                        CardCtrl curCard = lines[i].playersCard[lines[i].playersCard.Count-1];
                                        if(curCard.cardType==CardType.bloon){
                                            curCard.clock--;
                                            if(curCard.clock<=0){
                                                opponentsHand.heroHp-=curCard.hp;
                                                lines[i].DiscardAll(true, playersHand);
                                            }
                                        }
                                    }
                                }
                            } else{
                                for(int i=0; i<lines.Count; i++){
                                    if(lines[i].opponentsCard.Count>0){
                                        CardCtrl curCard = lines[i].opponentsCard[lines[i].opponentsCard.Count-1];
                                        if(curCard.cardType==CardType.tower){
                                            curCard.clock--;
                                            if(curCard.clock<=0){
                                                playersHand.heroHp-=curCard.hp;
                                                lines[i].DiscardAll(false, opponentsHand);
                                            }
                                        }
                                    }
                                }
                            }
                            currentTurnStage++;
                            break;
                        }
                        case 3: {
                            if(isPlayersTurnNow) Debug.Log("Player's monkes attack"); else Debug.Log("Opponents's monkes attack");
                            currentTurnStage++;
                            break;
                        }
                        case 4: {
                            if(isPlayersTurnNow){
                                for(int i=0; i<lines.Count; i++){
                                    if(lines[i].playersCard.Count>0){
                                        CardCtrl curCard = lines[i].playersCard[lines[i].playersCard.Count-1];
                                        if(curCard.cardType==CardType.tower){
                                            curCard.banana--;
                                            if(curCard.banana<=0){
                                                lines[i].DiscardAll(true, playersHand);
                                            }
                                        }
                                    }
                                }
                            } else{
                                for(int i=0; i<lines.Count; i++){
                                    if(lines[i].opponentsCard.Count>0){
                                        CardCtrl curCard = lines[i].opponentsCard[lines[i].opponentsCard.Count-1];
                                        if(curCard.cardType==CardType.tower){
                                            curCard.banana--;
                                            if(curCard.banana<=0){
                                                lines[i].DiscardAll(false, opponentsHand);
                                            }
                                        }
                                    }
                                }
                            }
                            currentTurnStage++;
                            break;
                        }
                        case 5: {
                            if(isPlayersTurnNow){
                                playersHand.coins=Mathf.Clamp(currentTurn, 0, 10);
                            }else{
                                opponentsHand.coins=Mathf.Clamp(currentTurn, 0, 10);
                            }
                            currentTurnStage++;
                            break;
                        }
                        case 6: {
                            if(isPlayersTurnNow) cards2Draw=2; else opCards2Draw=2;
                            currentTurnStage++;
                            break;
                        }
                        case 7: {
                            turnOrder=TurnOrder.MainPhaze;
                            currentTurnStage=1;
                            break;
                        }
                    }
                    break;
                }
                case TurnOrder.MainPhaze: {
                    if(isPlayersTurnNow){
                        if(selectedCard!=null && selectedLine!=null){
                            if(selectedCard.supportType==SupportType.none){
                                selectedCard.transform.SetParent(selectedLine.playersSpawn.transform);
                                selectedCard.transform.position=selectedLine.playersSpawn.transform.position;
                                /*if(selectedLine.playersCard.Count>0) selectedLine.playersCard[0].transform.position=selectedLine.playersSpawn.transform.position + new Vector3(0, 20, 0);*/
                                selectedLine.playersCard.Add(selectedCard);
                                selectedLine.curPCard=selectedCard;
                                playersHand.coins-=selectedCard.cost;

                                selectedCard.transform.localScale=new Vector2(0.25f,0.25f);
                                selectedCard.isSelected=false;
                                selectedCard.lineItIsOn=selectedLine;
                            } else{
                                switch(selectedCard.cardOg.cardId){
                                    case 105: {
                                        selectedLine.curPCard.banana++;
                                        break;
                                    }
                                    case 106: {
                                        AddKeyword(KeywordsKinds.Ultravision);
                                        break;
                                    }
                                    case 107: {
                                        selectedLine.curPCard.atk++;
                                        break;
                                    }
                                    case 108: {
                                        selectedLine.curOCard.banana--;
                                        break;
                                    }
                                    case 109: {
                                        AddKeyword(KeywordsKinds.Fortibuster);
                                        break;
                                    }
                                    case 111: {
                                        bool canDestroy=true;
                                        for(int i=0; i<selectedLine.curPCard.keywords.Count; i++){
                                            if(selectedLine.curPCard.keywords[i].name==KeywordsKinds.PurpleImmune) canDestroy=false;
                                        }
                                        if(canDestroy) selectedLine.DiscardAll(true, playersHand);
                                        canDestroy=true;
                                        for(int i=0; i<selectedLine.curOCard.keywords.Count; i++){
                                            if(selectedLine.curOCard.keywords[i].name==KeywordsKinds.PurpleImmune) canDestroy=false;
                                        }
                                        if(canDestroy) selectedLine.DiscardAll(false, opponentsHand);
                                        break;
                                    }
                                    case 114: {
                                        AddKeyword(KeywordsKinds.Blazing);
                                        break;
                                    }
                                    case 115: {
                                        AddKeyword(KeywordsKinds.Camo);
                                        break;
                                    }
                                    case 116: {
                                        AddKeyword(KeywordsKinds.Fortified);
                                        break;
                                    }
                                    case 117: {
                                        AddKeyword(KeywordsKinds.FrostImmune);
                                        break;
                                    }
                                    case 118: {
                                        AddKeyword(KeywordsKinds.SplashImmune);
                                        break;
                                    }
                                    case 119: {
                                        AddKeyword(KeywordsKinds.PurpleImmune);
                                        break;
                                    }
                                    case 120: {
                                        AddKeywordChangeValues(KeywordsKinds.Freeze, 1, 0);
                                        break;
                                    }
                                    case 121: {
                                        playersHand.heroHp+=5;
                                        break;
                                    }
                                    case 122: {
                                        AddKeywordChangeValues(KeywordsKinds.Recover, 1, 0);
                                        break;
                                    }
                                }
                                playersHand.coins-=selectedCard.cost;
                                selectedCard.Discard();
                            }
                            selectedCard=null;
                            selectedLine=null;
                        }
                    }
                    else{
                        turnOrder=TurnOrder.EndOfTurn; currentTurnStage=1;
                    }
                    break;
                }
                case TurnOrder.EndOfTurn: {
                    Debug.Log("EOF");
                    turnOrder=TurnOrder.Upkeep;
                    currentTurnStage=1;
                    isPlayersTurnNow=!isPlayersTurnNow;
                    break;
                }
            }
        }

        endTurnBtn.SetActive(turnOrder==TurnOrder.MainPhaze && cards2Draw==0 && opCards2Draw==0 && isPlayersTurnNow);
    }

    void AddKeyword(KeywordsKinds kK){
        KeywordStats kS = new KeywordStats();
        kS.name = kK;
        selectedLine.curPCard.keywords.Add(kS);
    }

    void AddKeywordChangeValues(KeywordsKinds kK, int t1, int t2){
        List<KeywordStats> cardKSlist = selectedLine.curPCard.keywords;
        bool isThere=false; int i;
        for(i=0; i<cardKSlist.Count; i++){
            if(cardKSlist[i].name==kK){
                isThere=true;
                break;
                }
            }
        if(isThere){
            cardKSlist[i].tier+=t1;
            cardKSlist[i].secondaryTier+=t2;
        }else{
            KeywordStats kS = new KeywordStats();
            kS.name = KeywordsKinds.Recover;
            kS.tier=t1;
            kS.secondaryTier=t2;
            selectedLine.curPCard.keywords.Add(kS);
        }
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
                playersHand.cardsInHand.Add(createdCard.GetComponent<CardCtrl>());
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
                playersHand.cardsInHand.Add(createdCard.GetComponent<CardCtrl>());
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
            opponentsHand.cardsInHand.Add(createdCard.GetComponent<CardCtrl>());
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
            opponentsHand.cardsInHand.Add(createdCard.GetComponent<CardCtrl>());
            opponentsHand.bloonDeck.RemoveAt(randCard);
            createdCard.GetComponent<CardCtrl>().SetCardData(opponentsHand, true, this);

        }
    }

    public void EndTurn(){
        turnOrder=TurnOrder.EndOfTurn; currentTurnStage=1;
    }
}
