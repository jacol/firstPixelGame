using UnityEngine;
using System.Collections;
using System.Net;

public class SpawnScript : MonoBehaviour {

    public static bool GameOver = false;

    public GameObject Monster;
    public GameObject Baby;

	// Use this for initialization
	void Start () {
        SpawnMonster();
        SpawnMonster();
        SpawnMonster();

        //using (WebClient wc = new WebClient())
        //{
        //    string s = wc.DownloadString("http://www.wp.pl/");
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void MonsterDestroyed()
    {
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        int x = 0, y = 0;       //140, 90 is baby

        do
        { 
            x = Random.Range(0, 200);
        }
        while (x < 180 && x > 100);
    
        do
        { 
            y = Random.Range(0, 200);
        }
        while (y < 130 && y > 50);

        GameObject monster = GameObject.Instantiate(Monster, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
        MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        monsterScript.Baby = Baby;
        monsterScript.SpawnScript = this;
    }

    void BabyAwake()
    {
        
    }
}
