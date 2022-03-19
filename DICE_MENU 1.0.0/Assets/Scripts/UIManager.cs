using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour{

    public CardManager cardManager;
    public GameObject[] cardSlots;

	// For Card-Class colors 
	// public Color humanColor, orcColor, dragonColor, beastColor, otherColor;
    
    public int page;
    public Text pageText;

	// REFACTORING

	[SerializeField] private bool isSearch;	// Default value is false. Once the value is false. normal display mode
	[SerializeField] private int totalNumbers;
	[SerializeField] private int currentSearchMana;

	// Complete Search by card class
	[SerializeField] private bool isSearchByMana;
	[SerializeField] private bool isSearchByClass;
	[SerializeField] private string currentSearchClass;

	// Search feature
	[SerializeField] private string searchName;
	public InputField searchInput;
	[SerializeField] private bool isSearchByInput;

	private void Start(){
		DisplayCards();
		UpdatePageUI();
	}
	
	// Working
  	private void Update(){
		// UpdatePageUI();	// Move out of update method

		TurnPage();

		if (!isSearch)
			totalNumbers = 0; 
	}

	// Working
	private void UpdatePageUI(){
		if (!isSearch){
			// pageText.text = (page + 1).ToString();
			pageText.text = (page + 1) + "/" + (Mathf.Ceil(cardSlots.Length / 8) +1).ToString();	// current page + "/" + max Page
		}	else	{
			// pageText.text = "Search By MANA / CLASS MODE";
			pageText.text = (page + 1) + "/" + (Mathf.Ceil(totalNumbers / 8) + 1).ToString();
		}
	}

	// Working
	private void TurnPage() {
		if (!isSearch){	// NORMAL MODE
			if(Input.GetKeyDown(KeyCode.RightArrow)){	// Next Page
				// iIf the page is greater than the cards total number divided by 8 --> page turn back to 0
				if(page >= Mathf.Floor((cardManager.cards.Count -1) /8 )){
					page = 0;
				} else {
					page++;
				}
				Debug.Log(page);
				DisplayCards();
				UpdatePageUI();
			}

			if(Input.GetKeyDown(KeyCode.LeftArrow)){	// Last Page
				// If the page is less than or equal than 0, the page should turn to the cards total number divided by 8
				if(page <= 0){
					page = Mathf.FloorToInt((cardManager.cards.Count -1) /8);
				} else {
					page--;
				}
				Debug.Log(page);
				DisplayCards();
				UpdatePageUI();
			}
		}
		else {
			if (isSearchByMana){
				if (Input.GetKeyDown(KeyCode.RightArrow)){
					if (page >= (Mathf.FloorToInt(totalNumbers / 8 ))){
						page = 0;
					} else {
						page++;
					}
					DisplayBySearchMana();
					UpdatePageUI();
				}
			
				if (Input.GetKeyDown(KeyCode.LeftArrow)){
					if (page<=0){
						page = (Mathf.FloorToInt(totalNumbers / 8));
					} else {
						page--;
					}
					DisplayBySearchMana();
					UpdatePageUI();
				}
			}

			if (isSearchByClass){
				if (Input.GetKeyDown(KeyCode.RightArrow)){
					if (page >= (Mathf.FloorToInt(totalNumbers / 8 ))){
						page = 0;
					} else {
						page++;
					}
					DisplayBySearchClass();
					UpdatePageUI();
				}
			
				if (Input.GetKeyDown(KeyCode.LeftArrow)){
					if (page<=0){
						page = (Mathf.FloorToInt(totalNumbers / 8));
					} else {
						page--;
					}
					DisplayBySearchClass();
					UpdatePageUI();
				}
			}
			if (isSearchByInput){

			}
		}
	}

	// Working
	public void SearchByMana(int _mana){

		isSearchByMana = true;
		isSearchByClass = false;
		isSearchByInput = false;

		isSearch = true;
		totalNumbers = 0;
		page = 0;
		currentSearchMana = _mana;

		List<Card> cards = new List<Card>();
		cards = ReturnCard(_mana);
		
		// DisplayCardWhenPressButton(cards);
		UpdatePageUI();


		for (int i = 0; i < cardManager.cards.Count; i++){
			if (_mana < 8){
				if(cardManager.cards[i].manaCost == _mana){
						DisplaySingleCard(i);
				} else {
					cardSlots[i].gameObject.SetActive(false);
				}
			} else {
				if (cardManager.cards[i].manaCost >= _mana){
						DisplaySingleCard(i);
				} else {
					cardSlots[i].gameObject.SetActive(false);
				}
			}
		}
	}

	// Working
	public void SearchByClass(string _cardClass){
		isSearchByMana = false;
		isSearchByClass = true;
		isSearchByInput = false;

		isSearch = true;
		totalNumbers = 0;
		page = 0;

		currentSearchClass = _cardClass;
		List<Card> cards = new List<Card>();
		cards = ReturnCard(_cardClass);

		// DisplayCardWhenPressButton(cards);
		UpdatePageUI();

		for (int i=0; i<cardSlots.Length; i++){
			cardSlots[i].gameObject.SetActive(false);
		}
		
		for (int i = 0; i < cardManager.cards.Count; i++){
			if(cardManager.cards[i].cardClass.ToString() == _cardClass){
				DisplaySingleCard(i);
			} else {
				cardSlots[i].gameObject.SetActive(false);
			}
		}
	}

	private void DisplayCardWhenPressButton(List<Card> _cards){
		for (int i = 0; i < cardSlots.Length; i++){
			cardSlots[i].gameObject.SetActive(false);
		}

		for (int i = 0; i < _cards.Count; i++){
			if (i >= page * 8 && i < (page + 1) * 8){
				totalNumbers++;
				cardSlots[i].gameObject.SetActive(true);
			} else {	
			cardSlots[i].gameObject.SetActive(false);
			}
		}
	}

	// Normal Mode - working
    private void DisplayCards(){
        for (int i = 0; i<cardManager.cards.Count; i++){
            if (i>=page *8 && i<(page + 1) *8){
                DisplaySingleCard(i);
            } else { 
                cardSlots[i].gameObject.SetActive(false);
            }
        }
    }

	// CODE REFACTORING - Working function for CODE REFACTORING 
	private void DisplaySingleCard(int i) {
		totalNumbers++;
		cardSlots[i].gameObject.SetActive(true);
	}

	// Working
	public void InitialCardsTab(){
		page = 0;
		isSearch = false;
		DisplayCards();

		isSearchByMana = false;
		isSearchByClass = false;
		isSearchByInput = false;

		UpdatePageUI();
	}

	//----------------------------------------------------------

	#region Helper Function
	//	Help FUNCTION search by mana - working
	private List<Card> ReturnCard(int _mana){
		List <Card> cards = new List <Card>();	// Create one empty list
		
		for (int i = 0; i < cardManager.cards.Count; i++){
			Card card;

			if (_mana < 8){
				if (cardManager.cards[i].manaCost == _mana){
					card = cardManager.cards[i];
					cards.Add(card);
				}
			} else {
				if (cardManager.cards[i].manaCost >= _mana){
					card = cardManager.cards[i];
					cards.Add(card);
				}
			}
		}
		Debug.Log(cards.Count);
		return cards; 
	}

	//	Help FUNCTION search by class - working
	private List<Card> ReturnCard(string _cardClass){
		List<Card> cards = new List<Card>();	// Create on Epty List

		for (int i = 0; i < cardManager.cards.Count; i++){
			Card card;

			if (cardManager.cards[i].cardClass.ToString() == _cardClass){
				card = cardManager.cards[i];
				cards.Add(card);
			}
		}
		Debug.Log(cards.Count);
		return cards; 
	} 
	#endregion
	
	//	Update when we turn page search by MANA - Working
	private void DisplayBySearchMana(){
		List <Card> cards = new List<Card>();
		cards = ReturnCard(currentSearchMana);

		DisplayCardBySearch(cards);
		UpdatePageUI();

		// for (int i = 0; i < cardSlots.Length; i++){
		// 	cardSlots[i].gameObject.SetActive(false);
		// }
		
		// for (int i = 0; i < cards.Count; i++){
		// 	if (i>= page *8 && i < (page +1) *8){
		// 		cardSlots[i].gameObject.SetActive(true);
		// 	} else {
		// 	cardSlots[i].gameObject.SetActive(false);
		// 	}
		// }
	}

	private void DisplayBySearchClass(){
		List <Card> cards = new List<Card>();
		cards = ReturnCard(currentSearchClass);

		DisplayCardBySearch(cards);
		UpdatePageUI();

		// for (int i = 0; i < cardSlots.Length; i++){
		// 	cardSlots[i].gameObject.SetActive(false);
		// }
		
		// for (int i = 0; i < cards.Count; i++){
		// 	if (i>= page *8 && i < (page +1) *8){
		// 		cardSlots[i].gameObject.SetActive(true);
		// 	} else {
		// 	cardSlots[i].gameObject.SetActive(false);
		// 	}
		// }
	}

	// Refactoring for these two functions above
	private void DisplayCardBySearch(List<Card> _cards){
		for (int i = 0; i < cardSlots.Length; i++){
			cardSlots[i].gameObject.SetActive(false);
		}
		
		for (int i = 0; i < _cards.Count; i++){
			if (i>= page * 8 && i < (page +1) *8){
				cardSlots[i].gameObject.SetActive(true);
			}
			else {
			cardSlots[i].gameObject.SetActive(false);
			}
		}
	}

	// When we press enter button or search icon, we will call this function
	public void SearchByInput(){
		isSearchByInput = true;
		isSearchByMana = false;
		isSearchByClass = false;

		searchName = searchInput.text; 

		for (int i = 0; i < cardSlots.Length; i++){
			cardSlots[i].gameObject.SetActive(false);
		}

		for (int i = 0; i < cardManager.cards.Count; i++){
			if (searchName.ToUpper() == cardManager.cards[i].name.ToUpper()){
				// Display the specific card!					

				cardSlots[i].gameObject.SetActive(true);
			} 	else {
				cardSlots[i].gameObject.SetActive(false);
			}
		}
		pageText.text = "1/1";	// Display correct Page UI
	}
}
