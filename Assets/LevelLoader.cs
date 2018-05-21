using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public Text	percentageText;

	public void LoadLevel (int sceneIndex) {
		StartCoroutine (LoadAsynchronously (sceneIndex));
	}

	IEnumerator LoadAsynchronously (int sceneIndex) {
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);

		loadingScreen.SetActive (true);

		while (!operation.isDone) {
			// Unity has two parts of the loading process
			// Part 1 , which we want to display, has an operation progress from 0.0 - 0.9
			// Clamp the progress value from 0 to 1
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.value = progress;
			percentageText.text = progress * 100f + "%";

			yield return null;
		}
	}
}
