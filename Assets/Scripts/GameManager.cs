using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject Plant;
	public int Resources;
	public int Round;

	public UIController UIController;
	public ResourceInfoUIController ResourceInfoUIController;
	public GameObject[] SpawnPoints;


	private Vector2[] Grid;
	private List<PlantComponent> ActivePlantComponents;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UIController.SetResources (Resources);
	}

	public void NextRoundPressed()
	{
		// Do some animation then calculate resources...
		CalculateResources();
		SpawnPlants ();
	}

	public void SpawnPlants()
	{
		// Spawn three new plants

		GameObject NewPlant = GameObject.Instantiate (Plant, SpawnPoints [0].transform.position, Quaternion.identity) as GameObject;
		// Calculate the cumulative score for each plant...

		Round++;
		UIController.SetRound (Round);
	}

	void CalculateResourcesLost()
	{
		Resources -= Round * 1000;
	}

	IEnumerator DrainResources()
	{
		yield return new WaitForSeconds(3f);
		CalculateResourcesLost ();
	}

	IEnumerator FillResources(int Amount)
	{
		int integral = 100;
		// account for if there are 0 resources
		while (Amount > 0) {
			Resources += integral;
			Amount -= integral;
			yield return new WaitForSeconds(0.05f);
		}
		if (Amount <= 0) {
			StartCoroutine (DrainResources ());
		}
	}

	void CalculateResources()
	{
		GameObject[] PlantObjects = GameObject.FindGameObjectsWithTag ("Plant");
		ActivePlantComponents = new List<PlantComponent> ();
		foreach (GameObject plant in PlantObjects) {
			PlantComponent component = plant.GetComponent<PlantComponent> ();
			if (component.IsSlotted ()) {
				ActivePlantComponents.Add (component);
			}
		}
		// Have the resource value set on ActivePlant component, this is set based on the footprint of what was there before
		int GeneratedResources = 0;
		foreach (PlantComponent Plant in ActivePlantComponents) {
			GeneratedResources += Plant.CurrentProductionValue ();
		}
		StartCoroutine(FillResources(GeneratedResources));
		

		// perhaps produce first, drain second.....



	}

}
	
