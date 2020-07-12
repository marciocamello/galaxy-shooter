using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

	// Use this for initialization
	private void Start ()
    {
        Destroy(this.gameObject, 4f);
	}
}
