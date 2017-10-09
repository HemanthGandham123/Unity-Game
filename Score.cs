using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public struct Attacker {
		public int NoOfBulletsFired;
		public bool isKilled;
	}

	public struct Defender {
		public int NoOfBulletsEaten;
		public bool isKilled;
		public float TimeSurvived;
	}

	public struct player {
		public Attacker attackerStats;
		public Defender defenderStats;
		public int test;
	}

	private Hashtable collection;

	public static Score Instance;

	public Hashtable getAllPayers(){
		return collection;
	}

	public player createPlayer(string key){
		if (collection != null) {
			if (!collection.Contains (key)) {
				player play = new player ();
				play.attackerStats = new Attacker ();
				play.defenderStats = new Defender ();

				play.attackerStats.isKilled = false;
				play.attackerStats.NoOfBulletsFired = 0;

				play.defenderStats.isKilled = false;
				play.defenderStats.NoOfBulletsEaten = 0;
				play.defenderStats.TimeSurvived = 0;

				collection.Add (key, play);
				Debug.Log ("Created a new player: "+key);
			}
			return (player)collection [key];
		} else {
			Debug.Log ("You're screwed!");
			collection = new Hashtable ();
			return createPlayer (key);
		}
	}

	public void setDefenderScore(string playerName, bool incrementBulletsEaten, bool isKilled, float TimeSurvived){
		player singleplayer = (player) collection[playerName];
		if (incrementBulletsEaten == true) {
			singleplayer.defenderStats.NoOfBulletsEaten++;
		} 

		if (isKilled == true) {
			singleplayer.defenderStats.isKilled = true;
		} 

		if (TimeSurvived != 0.0f) {
			singleplayer.defenderStats.TimeSurvived = TimeSurvived;
		}
		collection [playerName] = singleplayer;
	}

	public void setAttackerScore(string playerName, bool incrementBulletsFired, bool isKilled){
		player singlePlayer = (player) collection[playerName];
		if (incrementBulletsFired == true) {
			singlePlayer.attackerStats.NoOfBulletsFired++;
		}

		if (isKilled == true) {
			singlePlayer.attackerStats.isKilled = true;
		}
		collection [playerName] = singlePlayer;
	}

	// Use this for initialization
	void Start () {
		if (collection == null) {
			collection = new Hashtable ();
			Debug.Log ("STARTED ALL OVER AGAIN!!");
		}
	}


	void Awake () {
		if (Instance == null) {
			DontDestroyOnLoad (gameObject);
			Instance = this;
		} else if (Instance != this) {
			Debug.Log ("Destroying the ScoreBoard Object :P");
			Destroy (gameObject);
		}
	}
}