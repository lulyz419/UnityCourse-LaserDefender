using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Llevamos el score a la pantalla final.
		Text myText = GetComponent<Text>();
		myText.text = ScoreKeeper.score.ToString();
		ScoreKeeper.Reset();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
