using UnityEngine;
using System.Collections;

public class SlotResponder : MonoBehaviour {

	public Material HighlightedMaterial;
	public Vector2 Coordinates;

	Material StandardMaterial;

	// Use this for initialization
	void Start () {
		// set coordinates based on position within grid
		Coordinates.x = transform.position.x;
		Coordinates.y = transform.position.y;
		StandardMaterial = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HighlightForHovering()
	{
		// set background color shadow for block hovering over it
		if (GetComponent<Renderer> ().material != HighlightedMaterial) {
			GetComponent<Renderer>().material = HighlightedMaterial;
		}

	}

	public void ResetToStandardMaterial()
	{
		GetComponent<Renderer> ().material = StandardMaterial;
	}

}
