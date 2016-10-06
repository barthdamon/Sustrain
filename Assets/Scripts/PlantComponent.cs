using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlantComponent : MonoBehaviour {


//	public 
	// need a class for a property
	public PlantProperty PlantProperty;

	private Vector3 screenPoint;
	private Vector3 offset;
	private List<SlotResponder> Slots = new List<SlotResponder> ();

	private SlotResponder CurrentSlot;
	private int TimeToPlant = 1;
	private SlotResponder LastPlacedSlot;
	private Vector3 StartingPosition;
	private GameManager GameManager;

	// Use this for initialization
	void Start () {
		StartingPosition = transform.position;
		PlantProperty = GetComponent<PlantProperty> ();
		GameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		FindAllSlots ();
		GameManager.ResourceInfoUIController.SetForPlantComponent (this);
		GameManager.CurrentlySelected = GetComponent<GameObject>();
//		UnityEngine.Color MaterialColor = GetComponent<Renderer> ().material.color;
//		MaterialColor.a = 0.5f;
//		MaterialColor.g = 1f;
//		GetComponent<Renderer> ().material.color = MaterialColor;
//
//
//		UnityEngine.Color CurrentMaterialColor = GameManager.CurrentlySelected.GetComponent<Renderer> ().material.color;
//		CurrentMaterialColor.a = 0.5f; 
//		CurrentMaterialColor.g = 1f;
//		GameManager.CurrentlySelected.GetComponent<Renderer> ().material.color = CurrentMaterialColor;
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		curPosition.y = 2;
		transform.position = curPosition;
		CheckForSlotHighlights ();
	}

	void OnMouseUp()
	{
		if (CurrentSlot) {
			if (LastPlacedSlot && LastPlacedSlot != CurrentSlot) {
//				LastPlacedSlot.PastProperty = null;
			}
			LastPlacedSlot = CurrentSlot;
			transform.position = CurrentSlot.transform.position;
			if (CurrentSlot != LastPlacedSlot) {
				// otherwise the last one won't stay....
			}
			//
			if (CurrentSlot.PastProperty != PlantProperty) {
				TimeToPlant = 1;
			}
		} else {
			transform.position = StartingPosition;
		}
	}

	public void UpdateFootprint()
	{
		if (CurrentSlot) {
			TimeToPlant -= 1;
			if (TimeToPlant == 0) {
				CurrentSlot.FootprintPlanted (PlantProperty);
			}
		}
	}

	void FindAllSlots()
	{
		GameObject[] SlotObjects = GameObject.FindGameObjectsWithTag("Slot");
		foreach (GameObject Slot in SlotObjects)
		{
			SlotResponder res = Slot.GetComponent<SlotResponder> ();
			if (res){
				Slots.Add (res);
//				Debug.Log ("Found Slot");
			}
		}
	}
	
	void CheckForSlotHighlights() {
		bool foundSlot = false;
		foreach (SlotResponder slot in Slots) {
//			Debug.Log ("Checking Slots...");
			if (ShouldHighlightSlot (slot)) {
//				Debug.Log ("Should Highlight Slot");
				slot.HighlightForHovering ();
				CurrentSlot = slot;
				foundSlot = true;
			} else {
				slot.ResetToStandardMaterial ();
			}
		}

		if (!foundSlot) {
			CurrentSlot = null;
		}
	}


	bool ShouldHighlightSlot(SlotResponder Slot) {
		float offset = 0.5f;
		float xMin = Slot.transform.position.x - offset;
		float xMax = Slot.transform.position.x + offset;
		float zMin = Slot.transform.position.z - offset;
		float zMax = Slot.transform.position.z + offset;

		float plantX = transform.position.x;
		float plantZ = transform.position.z;

		if (plantX > xMin && plantX < xMax && plantZ > zMin && plantZ < zMax) {
			return true;
		} else {
			return false;
		}
	}

	public bool IsSlotted()
	{
		return CurrentSlot != null;
	}

	public int CurrentProductionValue()
	{
		// calculate value based on how much of its resources are being fed
		int ProductionValue = 1000;
		Debug.Log ("Finding Current Production Value...");
		if (CurrentSlot.PastProperty && CurrentSlot.PastProperty != PlantProperty) {
			Debug.Log ("Found Past Property");
			// Color Boost
			if (PlantProperty.Fuel.Color == CurrentSlot.PastProperty.Footprint.Color) {
				// Add any boosts in these sections
				ProductionValue += 500;
				Debug.Log ("Color matched");
			}
			// Shape Boost
			if (PlantProperty.Fuel.Shape == CurrentSlot.PastProperty.Footprint.Shape) {
				// Add any boosts in these sections
				ProductionValue += 500;
				Debug.Log ("Shape matched");
			}
			// Size Boost
			if (PlantProperty.Fuel.Size == CurrentSlot.PastProperty.Footprint.Size) {
				// Add any boosts in these sections
				ProductionValue += 500;
				Debug.Log ("Size matched");
			}
		}

		return ProductionValue;
	}

}
