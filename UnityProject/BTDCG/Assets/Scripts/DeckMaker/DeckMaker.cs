using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class DeckMaker : MonoBehaviour
{
    [SerializeField] GameObject deckPrefab;
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] List<GameObject> decks = new List<GameObject>();
    [SerializeField] GameObject newDeckButton;

    void Start()
    {
        for(int i=0; i<playerInfo.deckList.Count; i++){
            GameObject createdDeck = (GameObject)Instantiate(deckPrefab, this.transform.position, Quaternion.Euler(0, 0, 0), this.transform);
            DeckInfo deckInfo = createdDeck.GetComponent<DeckInfo>();
            deckInfo.deck=playerInfo.deckList[i];
            deckInfo.nameText.text=deckInfo.deck.deckName;
            createdDeck.GetComponent<RectTransform>().anchoredPosition = new Vector2(i%4*275, Mathf.Floor(i/4)*-425);
            decks.Add(createdDeck);
        }
    }

    void Update()
    {
        for(int i=0; i<decks.Count; i++){
            decks[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i%4*275, Mathf.Floor(i/4)*-425);
            decks[i].GetComponent<DeckInfo>().nameText.text=decks[i].GetComponent<DeckInfo>().deck.deckName;
        }
        newDeckButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(decks.Count%4*275, Mathf.Floor(decks.Count/4)*-425);
    }

    public void MakeNewDeck(){
        DeckSO deckSO = ScriptableObject.CreateInstance<DeckSO>();
        int deckLenght=playerInfo.deckList.Count;
        AssetDatabase.CreateAsset(deckSO, $"Assets/Data/DecksSO/Decks/{deckLenght}.asset");
        deckSO.towerDeck=new List<CardSO>();
        deckSO.bloonDeck=new List<CardSO>();
        deckSO.deckName="Deck "+deckLenght;
        playerInfo.deckList.Add(deckSO);
        AssetDatabase.SaveAssets();

        GameObject createdDeck = (GameObject)Instantiate(deckPrefab, this.transform.position, Quaternion.Euler(0, 0, 0), this.transform);
        DeckInfo deckInfo = createdDeck.GetComponent<DeckInfo>();
        deckInfo.deck=deckSO;
        deckInfo.nameText.text=deckInfo.deck.deckName;
        createdDeck.GetComponent<RectTransform>().anchoredPosition = new Vector2(deckLenght%4*275, Mathf.Floor(deckLenght/4)*-425);
        decks.Add(createdDeck);
    }
}
