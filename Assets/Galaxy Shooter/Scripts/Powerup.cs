﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; //0 = triple shot, 1 = speed boost, 2 = shields

    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);

            if (player != null)
            {

                //enable triple shot
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                //enable speed boost
                else if (powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                //enable shields
                else if (powerupID == 2)
                {
                    player.EnableShields();
                }
            }

            //destroy our selves
            Destroy(this.gameObject);
        }
    }
}
