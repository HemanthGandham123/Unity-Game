using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreListGenerator : MonoBehaviour {

	public GameObject ScoreEntryPrefab;

	// Use this for initialization
	void Start () {
		Hashtable collection = Score.Instance.getAllPayers();
		foreach(DictionaryEntry dictionaryEntry in collection){
			GameObject go = (GameObject) Instantiate (ScoreEntryPrefab);
			go.transform.SetParent(this.transform);
			Score.player player = (Score.player) dictionaryEntry.Value;
			go.transform.Find ("PlayerName").GetComponent<Text> ().text = dictionaryEntry.Key.ToString();
			go.transform.Find ("IsKilledAtt").GetComponent<Text> ().text = player.attackerStats.isKilled.ToString();
			go.transform.Find ("NoOfBulletsFired").GetComponent<Text> ().text = player.attackerStats.NoOfBulletsFired.ToString();
			go.transform.Find ("IsKilledDef").GetComponent<Text> ().text = player.defenderStats.isKilled.ToString();
			go.transform.Find ("NoOfBulletsEaten").GetComponent<Text> ().text = player.defenderStats.NoOfBulletsEaten.ToString();
			go.transform.Find ("TimeSurvived").GetComponent<Text> ().text = player.defenderStats.TimeSurvived.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
