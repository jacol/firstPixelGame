using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

    public static int Score = 0;

    public AudioClip Awake;
    public AudioClip Hurt;

    public GameObject Baby { get; set; }
    public SpawnScript SpawnScript { get; set; }

    IRagePixel _ragePixel;
    float _speed = 20;
    bool _done;

	void Start () {
        _ragePixel = GetComponent<RagePixelSprite>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!_done && !SpawnScript.GameOver)
        {
            float horizontalMove, verticalMove;

            if (transform.position.x > Baby.transform.position.x)
            {
                horizontalMove = Random.Range(-1f, 0f);
            }
            else
            {
                horizontalMove = Random.Range(0f, 1f);
            }

            if (transform.position.y > Baby.transform.position.y)
            {
                verticalMove = Random.Range(-1f, 0f);
            }
            else
            {
                verticalMove = Random.Range(0f, 1f);
            }

            //Debug.Log(horizontalMove + " " + verticalMove);

            if (verticalMove != 0 || horizontalMove != 0)
            {
                if (!_ragePixel.isPlaying())
                {
                    _ragePixel.PlayNamedAnimation("walk");
                }
            }
            else
            {
                _ragePixel.StopAnimation();
                _ragePixel.SetSprite("monster", 0);
            }


            transform.position += transform.right * horizontalMove * Time.deltaTime * _speed;
            transform.position += transform.up * verticalMove * Time.deltaTime * _speed;

            //CheckCollision();
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_done && !SpawnScript.GameOver)
        {
            if (col.gameObject.tag == "baby")
            {
                audio.PlayOneShot(Awake);
                _done = true;

                IRagePixel babyPixel = Baby.GetComponent<RagePixelSprite>();

                babyPixel.StopAnimation();
                babyPixel.PlayNamedAnimation("awake");

                _ragePixel.StopAnimation();
                _ragePixel.PlayNamedAnimation("victory");

                SpawnScript.GameOver = true;
                PlayerScript ps = GameObject.FindObjectOfType<PlayerScript>();
                ps.SendMessage("BabyAwake");

                
            }
            else if (col.gameObject.tag == "player")
            {
                audio.PlayOneShot(Hurt);
                _done = true;
                _ragePixel.StopAnimation();
                _ragePixel.PlayNamedAnimation("destroy");

                collider2D.enabled = false;

                GameObject.Destroy(this.gameObject, 1);
                Score += 10;
                SpawnScript.SendMessage("MonsterDestroyed");
            }
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(1, 0, 200, 20), "Your score: " + Score);

        if (SpawnScript.GameOver)
        {
            if (GUI.Button(new Rect((Screen.width / 2) - 100 , (Screen.height / 2) - 250, 200, 100), "Restart"))
            {
                SpawnScript.GameOver = false;
                Score = 0;
                Application.LoadLevel(0);
            }
        }
    }

}
