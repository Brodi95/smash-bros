using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[HideInInspector] public static GameManager Instance;

	public BaseCharacter[] SelectedCharacters;
//	[HideInInspector] public GameObject Player;
//	[HideInInspector] public Character Character;

	[SerializeField] private GameObject _PlayerPrefab;
	[SerializeField] private Vector3 _SpawnLocation;

	public bool Attacking = false;

	private void Awake() {
		// Apply Singleton pattern
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		SpawnPlayer ();
	}

	private void Start() {
		
	}

	private void SpawnPlayer() {
		foreach (var character in SelectedCharacters) {
			GameObject Player = Instantiate (_PlayerPrefab, _SpawnLocation, Quaternion.identity) as GameObject;
			var characterScript = Player.GetComponent<Character> ();
			characterScript._CharacterData = character;
		}
	}
}
