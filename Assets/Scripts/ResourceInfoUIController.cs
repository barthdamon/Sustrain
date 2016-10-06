using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceInfoUIController : MonoBehaviour {

	// Primary Text
	public Text OutputText;
	public Text FuelText;
	public Text FootprintText;
	public Text SpecialText;

	// DetailText
	public Text FuelColorText;
	public Text FuelShapeText;

	public Text FootprintColorText;
	public Text FootprintShapeText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetForPlantComponent(PlantComponent Plant)
	{
		ToggleForSlot (false);
		PlantProperty PP = Plant.PlantProperty;
		OutputText.text = "Output: " + PP.ProductionValue.ToString();

		FuelColorText.text = "Color: " + PP.Fuel.Color.ToString ();
		FuelShapeText.text = "Shape: " + PP.Fuel.Shape.ToString ();
//		FuelSizeText.text = "Size: " + PP.Fuel.Size.ToString ();

		FootprintColorText.text = "Color: " + PP.Footprint.Color.ToString ();
		FootprintShapeText.text = "Shape: " + PP.Footprint.Shape.ToString ();
//		FootprintSizeText.text = "Size: " + PP.Footprint.Size.ToString ();
//		FuelText.text = Plant.PlantProperty.ProductionValue;
		
	}

	public void SetForSlotFootprint(PlantProperty Plant)
	{
		ToggleForSlot (true);
		OutputText.text = "Slot";
		// Remove unecessary fuel and output elements, just have footprint
		FootprintColorText.text = "Color: " + Plant.Footprint.Color.ToString ();
		FootprintShapeText.text = "Shape: " + Plant.Footprint.Shape.ToString ();
	}

	void ToggleForSlot(bool isSlot)
	{
//		OutputText.enabled = !isSlot;
		FuelColorText.enabled = !isSlot;
		FuelShapeText.enabled = !isSlot;
		FuelText.enabled = !isSlot;
	}
}
