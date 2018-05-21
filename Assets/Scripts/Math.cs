using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math {

	// Check if object is infront or behind
	public static bool IsLookingAtObject(Transform self, Transform other) {
		// Vector from agent to target
		var heading = other.position - self.position;
		var dot = Vector3.Dot (self.forward, heading.normalized);
		Debug.Log (dot);
		// The object is infront if the dot product is bigger than 0...
		if (dot > 0f)
			return true;
		// ...otherwise it is behind
		return false;
	}

	// Using an angle to determine if the other object is infront
	public static bool IsLookingAtObject(Transform self, Transform other, float angle, bool facingRight) {

		// Change forward vector of player if player is directed to the left
		var rightFacing = facingRight ? self.right : -self.right;

		var compareAngle = Vector3.Angle (rightFacing, other.position - self.position);
		Debug.Log (compareAngle);
			
		if (compareAngle < angle) {
			return true;
		}

		return false;
	}
}
