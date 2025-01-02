using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseShoot : MonoBehaviour
{
    public float speed;
    public float stoppingdistance;
    public float retreatDistance;
    public float startTimeBtwShots;

    private float timeBtwShots;

    public GameObject projectile;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {

        //this defines if the enemie should move towards the player, stay still or move back from the player
        if (Vector3.Distance(transform.position, player.transform.position) > stoppingdistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        }
        else if (Vector3.Distance(transform.position, player.transform.position) < stoppingdistance && Vector2.Distance(transform.position, player.transform.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, -player.transform.position, speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
