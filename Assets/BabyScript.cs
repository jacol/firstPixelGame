using UnityEngine;
using System.Collections;

public class BabyScript : MonoBehaviour {

    private IRagePixel _ragePixel;

	// Use this for initialization
	void Start () {
        _ragePixel = GetComponent<RagePixelSprite>();
        _ragePixel.PlayNamedAnimation("sleep");
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
