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
			transform.position = CurrentSlot.transform.position;
			CurrentSlot.PastProperty = PlantProperty;
		} else {
			transform.position = StartingPosition;
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
				Debug.Log ("Found Slot");
			}
		}
	}
	
	void CheckForSlotHighlights() {
		bool foundSlot = false;
		foreach (SlotResponder slot in Slots) {
			Debug.Log ("Checking Slots...");
			if (ShouldHighlightSlot (slot)) {
				Debug.Log ("Should Highlight Slot");
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
		if (CurrentSlot.PastProperty && CurrentSlot.PastProperty != PlantProperty) {
			// Color Boost
			if (PlantProperty.Fuel.Color == CurrentSlot.PastProperty.Footprint.Color) {
				// Add any boosts in these sections
				ProductionValue += 500;
			}
			// Shape Boost
			if (PlantProperty.Fuel.Shape == CurrentSlot.PastProperty.Footprint.Shape) {
				// Add any boosts in these sections
				ProductionValue += 500;
			}
			// Size Boost
			if (PlantProperty.Fuel.Size == CurrentSlot.PastProperty.Footprint.Size) {
				// Add any boosts in these sections
				ProductionValue += 500;
			}
		}

		return ProductionValue;
	}

}
