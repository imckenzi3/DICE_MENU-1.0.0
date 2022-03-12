using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardClass{
	Human,
	Orc,
	Dragon,
	Beast,
	Other
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {

	public new string name;
	public string description;
	// public string cardClass;

	public Sprite artwork;

	public CardClass cardClass;

	public int manaCost;
	public int attack;
	public int health;

	public bool isHeld;
	public int holdNumber;
}
