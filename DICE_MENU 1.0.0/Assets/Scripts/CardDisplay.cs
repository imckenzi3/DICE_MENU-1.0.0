using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

	public CardManager cardManager;
	public Card card;

	public Text nameText;
	public Text descriptionText;

	public Image artworkImage;
	
	public Text classText;

	public Text manaText;
	public Text attackText;
	public Text healthText;

	public Image lockImage;


	private void Start(){
		DisplayCard();
	}

	private void DisplayCard () {
		nameText.text = card.name;
		descriptionText.text = card.description;

		artworkImage.sprite = card.artwork;

		manaText.text = card.manaCost.ToString();
		attackText.text = card.attack.ToString();
		healthText.text = card.health.ToString();

		
		if (!card.isHeld){	// We dont have this card - Is Locked
			lockImage.gameObject.SetActive(true);
			artworkImage.color = new Color(0,0,0);	// Black Mode
		}	else {
			lockImage.gameObject.SetActive(false);
			artworkImage.color = new Color(255,255,255);	// Normal Mode
			
			switch (card.cardClass){
				case CardClass.Human:{
					classText.text = "Human";
					break; 
				}
				case CardClass.Orc:{
					classText.text = "Orc";
					break; 
				}
				case CardClass.Dragon:{
					classText.text = "Dragon";
					break; 
				}
				case CardClass.Beast:{
					classText.text = "Beast";
					break; 
				}
				case CardClass.Other:{
					classText.text = "Other";
					break; 
				}
			}
		}
	}
}
