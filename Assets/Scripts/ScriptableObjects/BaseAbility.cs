using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New Ability", menuName = "ScriptableObjects/Ability")]
public  class BaseAbility : ScriptableObject {

//	public Ability AbilityScript;	// The Script getting attached to the player
	public string Name;				// The display name of the ability
	public string Description;		// The description of the ability
	public Sprite ArtWork;			// The sprite representing the ability in the game UI
	public Animation Animation;		// The player animation which gets played on ability usage
	public AudioClip SoundEffect;	// The sound which gets played on ability usage

	public string Event;		// The name of the event which is triggering the attack
	public float Damage;		// The amount that gets subtracted from the enemies healthpoints
	public float Cooldown;		// The time after the ability can be used again


}
