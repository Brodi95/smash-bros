using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

	private Dictionary <string, UnityEvent> _EventDictionary;	// A dictionary used to call methods

	private static EventManager _EventManager;					// A private reference to itself

	public static EventManager Instance {						// Returns the private reference to EventManager
		get	{
			// If eventManager hasn't been instantiated yet...
			if (!_EventManager) {
				// ...search for a gameobject, which contains an EventManager script, in our hierarchy
				_EventManager = FindObjectOfType (typeof(EventManager)) as EventManager;

				// If an EventManager doesn't exist...
				if (!_EventManager) {
					// ...throw a warning
					Debug.LogWarning ("The scene doesn't contain an event manager.");
				} else {
					_EventManager.Initialize ();
				}
			} 
			return _EventManager;
		}
	}

	private void Initialize() {
		// If there isn't an eventDictionary...
		if (_EventDictionary == null) {
			// ...create one
			_EventDictionary = new Dictionary <string, UnityEvent> ();
		}
	}

	// Starts listening to invokes of the given eventName inside the eventDictionary
	public static void StartListening (string eventName, UnityAction listener) {
		UnityEvent thisEvent = null;
		// If the dictionary contains the eventName...
		if (Instance._EventDictionary.TryGetValue (eventName, out thisEvent)) {
			// ...add a listener to it
			thisEvent.AddListener (listener);
		}
		// Otherwise create a new event and add it to the dictionary
		else {
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			Instance._EventDictionary.Add (eventName, thisEvent);
		}
	}

	// Stops listening to invokes of the given eventName inside the eventDictionary
	public static void StopListening (string eventName, UnityAction listener) {
		// Make sure there is an eventDictionary
		if (_EventManager == null) return;
		UnityEvent thisEvent = null;
		// If the event exists in the dictionary...
		if (Instance._EventDictionary.TryGetValue (eventName, out thisEvent)) {
			// ...remove the listener to it
			thisEvent.RemoveListener(listener);
		}
	}

	// Triggers the given event
	public static void TriggerEvent (string eventName) {
		UnityEvent thisEvent = null;
		// If the event exists in the dictionary...
		if (Instance._EventDictionary.TryGetValue (eventName, out thisEvent)) {
			// ...execute all methods listening to it
			thisEvent.Invoke ();
		}
	}
}
