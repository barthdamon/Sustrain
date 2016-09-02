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
	public Text FuelSizeText;

	public Text FootprintColorText;
	public Text FootprintShapeText;
	public Text FootprintSizeText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetForPlantComponent(PlantComponent Plant)
	{
		PlantProperty PP = Plant.PlantProperty;
		OutputText.text = "Output: " + PP.ProductionValue.ToString();

		FuelColorText.text = "Color: " + PP.Fuel.Color.ToString ();
		FuelShapeText.text = "Shape: " + PP.Fuel.Shape.ToString ();
		FuelSizeText.text = "Size: " + PP.Fuel.Size.ToString ();

		FootprintColorText.text = "Color: " + PP.Footprint.Color.ToString ();
		FootprintShapeText.text = "Shape: " + PP.Footprint.Shape.ToString ();
		FootprintSizeText.text = "Size: " + PP.Footprint.Size.ToString ();
//		FuelText.text = Plant.PlantProperty.ProductionValue;
		
	}

	public void SetForSlotFootprint(SlotResponder Slot)
	{
		// Remove unecessary fuel and output elements, just have footprint
	}
}
