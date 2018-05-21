using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

	public string SubmitButton;
	public string HorizontalAxis;

	public Image[] CharacterImages;
	private Image image;
	private int currentCharacterIndex = 0;

	private bool isPlaying;
	private bool characterSelected;

	private void Start () {
		image = GetComponent<Image> ();
	}

	private void Update () {
		if (!isPlaying && Input.GetButtonDown (SubmitButton)) {
			isPlaying = true;
		} else if (isPlaying) {
			if (Input.GetButtonDown (HorizontalAxis)) {
				float horizontalInput = Input.GetAxis (HorizontalAxis);
				if (horizontalInput < 0) {
					currentCharacterIndex = currentCharacterIndex == 0 ? CharacterImages.Length - 1 : currentCharacterIndex - 1;
					Debug.Log (currentCharacterIndex);
					//image.sprite = CharacterImages [currentCharacterIndex];
				} else if (horizontalInput > 0) {
					currentCharacterIndex = currentCharacterIndex == CharacterImages.Length - 1 ? 0 : currentCharacterIndex + 1;
					Debug.Log (currentCharacterIndex);
					//image.sprite = CharacterImages [currentCharacterIndex];
				}
			} else if (Input.GetButtonDown (SubmitButton)) {
				characterSelected = true;
				Debug.Log ("Character " + currentCharacterIndex + " selected");
			}
		}
	}
}
