using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public GameManager GameManager;
	public Text RoundText;
	public Text ResourcesText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextRoundPressed()
	{
		GameManager.NextRoundPressed ();
	}

	public void SetRound(int Round){
		RoundText.text = Round.ToString();
	}

	public void SetResources(int Resources){
		ResourcesText.text = Resources.ToString();
	}
}
