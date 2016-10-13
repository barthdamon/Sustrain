using UnityEngine;
using System.Collections;

public class SurviosTest : MonoBehaviour {

	string[] dirtyWords = new string[] {
		"shit",
		"ass"
	};


	// Use this for initialization
	void Start () {
		Debug.Log(FilterText ("A healthy ass can shit fertilizer"));
	}

	string FilterText(string dirtyText) {

		string filteredText = "";
		string currentWord = "";

		for (int j = 0; j < dirtyText.Length; j++) {
			char c = dirtyText[j];
			currentWord += c.ToString();
			string trimmedCurrent = currentWord.Trim ();
			// if end of word or end of string...
			if ((char.IsWhiteSpace (c)) || (j == dirtyText.Length - 1)) {
				if (CheckIfDirty (trimmedCurrent)) {
					filteredText += CleanString(trimmedCurrent);
					if (char.IsWhiteSpace (c)) {
						// add back space if it got trimmed
						filteredText += " ";
					}
				} else {
					filteredText += currentWord;
				}
				currentWord = "";
			}
		}
		return filteredText;
	}

	string CleanString(string currentWord) {
		// all but the first and last letter replaced with *
		string cleanWord = currentWord [0].ToString ();
		for (int i = 1; i < currentWord.Length - 1; i++) {
			cleanWord += "*";
		}
		cleanWord += currentWord [currentWord.Length - 1];
		return cleanWord;
	}

	bool CheckIfDirty(string word) {
		foreach (string w in dirtyWords) {
			if (w == word) {
				return true;
			}
		}
		return false;
	}
}
