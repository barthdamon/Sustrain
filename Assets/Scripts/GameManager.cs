using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject Plant;
	public int Resources;
	public int Round;

	public UIController UIController;
	public GameObject[] SpawnPoints;


	private Vector2[] Grid;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextRoundPressed()
	{
		// Do some animation then calculate resources...
		CalculateResources();
		SpawnPlants ();
	}

	public void SpawnPlants()
	{
		GameObject.Instantiate (Plant, SpawnPoints [0].transform.position, Quaternion.identity);
		Round++;
		UIController.SetRound (Round);
	}

	void CalculateResources()
	{
		GameObject[] PlantObjects = GameObject.FindGameObjectsWithTag ("Plant");
		List<PlantComponent> ActivePlantComponents = new List<PlantComponent> ();
		foreach (GameObject plant in PlantObjects) {
			PlantComponent component = plant.GetComponent<PlantComponent> ();
			if (component.IsSlotted ()) {
				ActivePlantComponents.Add (component);
			}
		}
		Resources += ActivePlantComponents.Count;
		UIController.SetResources (Resources);
	}

}
	
