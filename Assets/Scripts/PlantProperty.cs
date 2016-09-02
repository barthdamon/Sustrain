using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Color { Red, Yellow, Green };
public enum Shape { Circle, Triangle, Square };
public enum Size { Small, Medium, Large };

public struct Resource {
	public Color Color;
	public Shape Shape;
	public Size Size;
}

public class PlantProperty: MonoBehaviour {

	public int ProductionValue = 1000;

	public Resource Fuel;
	public Resource Footprint;

	void Start()
	{
		// Generate Random Production Value
		ProductionValue = Random.Range(250, 1000);
		Fuel = GenerateRandomResource();
		Footprint = GenerateRandomResource ();
	}

	private Resource GenerateRandomResource()
	{
		Resource RandResource = new Resource ();
		RandResource.Color = (Color)Random.Range(0, 3);
		RandResource.Shape = (Shape)Random.Range(0, 3);
		RandResource.Size = (Size)Random.Range(0, 3);
		return RandResource;
	}

}


