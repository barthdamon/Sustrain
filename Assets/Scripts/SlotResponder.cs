using UnityEngine;
using System.Collections;

public class SlotResponder : MonoBehaviour {

	public Material HighlightedMaterial;
	public Material FootprintedMaterial;
	public Vector2 Coordinates;

	//USED TO DETERMINE FOOTPRINT
	public PlantProperty PastProperty;

	private GameManager GameManager;

	Material StandardMaterial;

	// Use this for initialization
	void Start () {
		// set coordinates based on position within grid
		Coordinates.x = transform.position.x;
		Coordinates.y = transform.position.y;
		StandardMaterial = GetComponent<Renderer> ().material;
		GameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		if (PastProperty) {
			GameManager.ResourceInfoUIController.SetForSlotFootprint (PastProperty);
		}
	}

	public void FootprintPlanted(PlantProperty FootprintedProperty){
		PastProperty = FootprintedProperty;
		GetComponent<Renderer> ().material = FootprintedMaterial;
	}


	public void HighlightForHovering()
	{
		// set background color shadow for block hovering over it
		if (PastProperty == null) {
			if (GetComponent<Renderer> ().material != HighlightedMaterial) {
				GetComponent<Renderer>().material = HighlightedMaterial;
			}
		}

	}

	public void ResetToStandardMaterial()
	{
		if (PastProperty != null) {
			GetComponent<Renderer> ().material = FootprintedMaterial;
		} else {
			GetComponent<Renderer> ().material = StandardMaterial;
		}
	}

}
