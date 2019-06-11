using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    private Transform target;
    [Range(1, 100)]
    public float scrollLerp = 2.25f, yOffset;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    private void LateUpdate()
    {
        var movement = transform.position;
        var scroll = target.position;
        scroll.z = transform.position.z;


        var upward = Vector3.zero;
        upward.y = target.position.y;

        //lerp on 2 different axes with 2 different speeds
        movement = Vector3.Lerp(movement, scroll + Vector3.up * yOffset, scrollLerp * Time.deltaTime); /*+ 
            Vector3.Lerp(movement, upward, upLerp * Time.deltaTime);*/

        //Debug.Log(movement.ToString());
        transform.position = movement;
    }
}
