using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{

    public CardManager cardManager;
    public GameObject[] cardSlots;
    
    public int page;
    public Text pageText;

  	private void Update(){
		UpdatePage();

		if(Input.GetKeyDown(KeyCode.D)){	// Next Page
			// iIf the page is greater than the cards total number divided by 8 --> page turn back to 0
			if(page >= Mathf.Floor((cardManager.cards.Count -1) /8 )){
				page = 0;
			} else {
				page++;
			}
			Debug.Log(page);
            CallWhenTurnPage();
		}

		if(Input.GetKeyDown(KeyCode.A)){	// Last Page
			// If the page is less than or equal than 0, the page should turn to the cards total number divided by 8
			if(page <= 0){
				page = Mathf.FloorToInt((cardManager.cards.Count -1) /8);
			} else {
				page--;
			}
			Debug.Log(page);
            CallWhenTurnPage();
		}
	}

	private void UpdatePage(){
		// pageText.text = (page + 1).ToString();
        pageText.text = (page + 1) + "/" + (Mathf.Ceil(cardSlots.Length / 8) +1).ToString();
	}

    private void CallWhenTurnPage(){
        
        for (int i = 0; i<cardManager.cards.Count; i++){
            if (i>=page *8 && i<(page + 1) *8){
                cardSlots[i].gameObject.SetActive(true);
            } else { 
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
