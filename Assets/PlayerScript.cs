using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    IRagePixel _ragePixel;
    float _speed = 50;
    bool _done;

	void Start () {
        _ragePixel = GetComponent<RagePixelSprite>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!_done)
        {
            float horizontalMove, verticalMove;
            
#if         UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

#elif       UNITY_ANDROID
            Touch t = Input.GetTouch(0);
            Vector2 wPos = Camera.main.ScreenToWorldPoint(t.position);

            horizontalMove = wPos.x > (transform.position.x + 9) ? 1 : -1; //Input.GetAxis("Horizontal");
            verticalMove = wPos.y > (transform.position.y + 10) ? 1 : -1;//Input.GetAxis("Vertical");
#endif
            float speed = _speed;

            //Debug.Log(horizontalMove + " " + verticalMove);

            if (Mathf.Abs(verticalMove) + Mathf.Abs(horizontalMove) == 2)
            {
                speed *= 0.8f;
            }

            if (horizontalMove != 0 || verticalMove != 0)
            {
                if (!_ragePixel.isPlaying())
                {
                    _ragePixel.PlayNamedAnimation("walk");
                }
            }
            else
            {
                _ragePixel.StopAnimation();
                _ragePixel.SetSprite("player", 0);
            }


            transform.position += transform.right * horizontalMove * Time.deltaTime * speed;
            transform.position += transform.up * verticalMove * Time.deltaTime * speed;
        }
        
	}

    void BabyAwake()
    {
        _done = true;
        _ragePixel.StopAnimation();
        _ragePixel.PlayNamedAnimation("cry");
        
    }
}
