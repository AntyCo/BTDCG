using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckInfo : MonoBehaviour
{
    public DeckSO deck;
    public Text nameText;
    //[SerializeField] DeckMaker deckMaker;
    
    /*void Update()
    {
        nameText.text=deck.deckName;
    }*/

    public void ChooseThisDeck(){
        GameObject.Find("Controller").GetComponent<DeckMakerCtrl>().EditDeck(deck);
    }
}
