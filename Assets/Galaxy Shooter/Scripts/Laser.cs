using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;

    // Use this for initialization
    private void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void Update ()
    {
        //move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if position on the y my laser is greater than o equal to 6
        //destroy the laser

        if (transform.position.y >= 6)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

	}
}
