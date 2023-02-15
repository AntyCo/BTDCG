using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckMakerCtrl : MonoBehaviour
{
    [SerializeField] GameObject deckHolder, deckEditor, backBtn, nameInput;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] DeckSO editedDeck=null;
    [SerializeField] RectTransform rTdH, rTdE;
    List<GameObject> cardsShown = new List<GameObject>();
    bool editingDeck=false, currentlySeeingTowers=false, seesanything=true;
    [SerializeField] int cardsSpawned;

    void Start(){
        if(playerInfo.playersCards.Count==0){
            for(int i=0; i<playerInfo.possibleCards.Count; i++){
                CardAmmount cA = new CardAmmount();
                cA.whatCard=playerInfo.possibleCards[i];
                cA.howMany=0;
                playerInfo.playersCards.Add(cA);
            }
        }
        SpawnCards();
    }

    public void Update(){
        Debug.Log(Mathf.Floor(cardsSpawned/4)+" "+cardsSpawned/4);
        if(Input.mouseScrollDelta.y!=0){
            if(cardsSpawned>8 && editingDeck){
                rTdE.anchoredPosition=new Vector2(rTdE.anchoredPosition.x,Mathf.Clamp(rTdE.anchoredPosition.y+(Input.mouseScrollDelta.y*-100),-300,-805+Mathf.Floor((cardsSpawned-1)/4)*475));
            } else{
                rTdE.anchoredPosition=new Vector2(rTdE.anchoredPosition.x, -300);
            }
        }
    }

    public void SpawnCards(){
        for(int i=0; i<cardsShown.Count; i++){
            GameObject cardPlaceholder=cardsShown[i];
            cardsShown.RemoveAt(i);
            Destroy(cardPlaceholder);
        }
        cardsSpawned=0;
        for(int i=0; i<playerInfo.playersCards.Count; i++){
            if((currentlySeeingTowers==(playerInfo.playersCards[i].whatCard.cardType==CardType.tower))||seesanything){
                GameObject createdCard = (GameObject)Instantiate(cardPrefab, this.transform.position, Quaternion.Euler(0, 0, 0), deckEditor.transform);
                cardsShown.Add(createdCard);
                createdCard.transform.localScale=new Vector3(.5f, .5f, .5f);
                createdCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(cardsSpawned%4*275, Mathf.Floor(cardsSpawned/4)*-475);

                CardShowcase cS=createdCard.GetComponent<CardShowcase>();
                cS.cardOg = playerInfo.playersCards[i].whatCard;
                cS.ammount = playerInfo.playersCards[i].howMany;
                cS.SetCardData();
                cardsSpawned++;
            }
        }
    }
    
    public void EditDeck(DeckSO dSO){
        editingDeck=true;
        deckHolder.SetActive(false);
        deckEditor.SetActive(true); backBtn.SetActive(true); nameInput.SetActive(true);
        editedDeck=dSO;
        nameInput.GetComponent<InputField>().text=dSO.deckName;
        //Debug.Log($"Editing: {editedDeck.deckName}");
    }

    public void ChangeDeckName(){
        if(editedDeck!=null){
            editedDeck.deckName=nameInput.GetComponent<InputField>().text;
        }
    }

    public void EndEditing(){
        if(editedDeck!=null){
            editedDeck.deckName=nameInput.GetComponent<InputField>().text;
        }
        editedDeck=null;

        editingDeck=false;
        deckHolder.SetActive(true);
        deckEditor.SetActive(false); backBtn.SetActive(false); nameInput.SetActive(false);
    }
}
