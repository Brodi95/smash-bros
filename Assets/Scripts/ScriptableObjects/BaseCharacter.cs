using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObjects/Character")]
public class BaseCharacter : ScriptableObject {

	public string Name;
	public float HealthPoints;
	public List<BaseAbility> Abilities;

}
