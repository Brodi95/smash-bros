using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField] private Character _Character;		// Reference to player script
	private Image _Image;

	private void Awake() {
		_Image = GetComponent<Image> ();
		_Character.UpdateUI += UpdateHealthbar;
		_Character.Flip += Flip;
	}

	private void UpdateHealthbar() {
		_Image.fillAmount = _Character.CurrentHealthpoints / _Character.MaxHealthpoints;
	}

	// Flipping the image horizontally
	private void Flip() {
		// Inverting the images horizontal scale
		Vector3 localScale = transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
