using UnityEngine;
using System.Collections;

public class SurviosTestSpec1 : MonoBehaviour {

	void Start () {
		// Example call
		Debug.Log("Shoot projectile at this velocity: " 
				+ FindVelocityToHitTarget(new Vector2(0,0), 2f, new Vector2(10,0), new Vector2(0,1f)));

	}

	Vector2 FindVelocityToHitTarget(Vector2 pPos, float pSpeed, Vector2 tPos, Vector2 tVelocity) {

		float sDistance = (tPos - pPos).magnitude;
		float relativeSpeed = tVelocity.magnitude / pSpeed;

		// Relative speed and starting distance determines distance they can meet
		float tDistance = sDistance * Mathf.Tan (Mathf.Asin ((relativeSpeed)));
		// Just nice to know
		Debug.Log ("Time to hit target: " + tDistance / tVelocity.magnitude);

		// the angle the projectile needs to intersect at that distance
		float theta = Mathf.Atan (tDistance / sDistance);

		Vector2 pDirection = new Vector2 (Mathf.Cos (theta), Mathf.Sin (theta));
		return pDirection * pSpeed;
	}
}

	/*

	Please write pseudo code for rendering a shadow map in a forward renderer. 
	The procedure should be able to determine if a given pixel is shadowed.

	Get the cross product from the pixel’s triangle mesh from lights
	for each pixel then depending on how that goes towards the camera position make pixel lighter or darker

	foreach (pixel in pixels) {
		vector3 meshFacing = crossProduct(pixel.tranglemesh).normalized;
		foat lightFactor = dotProduct(lightSourceUnitVector, meshFacing);
		
		// lightFactor subtracts from darkness depending on how opposite the vectors are facing
		// fully opposite = fully in the light
		pixel.darknessLevel += lightFactor * scaler
	}

	// my VERY limited understanding (from doing some quick research) of how this might work for directional lights...
	void RenderShadowMap() {
		foreach (pixel in pixels) {
			if (ObjectBetweenLightAndPixel(light, pixel)) {
				pixel.DrawShadowed()
			}
		}
	}
	
	*/

