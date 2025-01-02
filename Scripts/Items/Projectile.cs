using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private GameObject player;
    private Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        target = new Vector3(player.transform.position.x, player.transform.position.y);

    }
    private void Update()
    {
        //stubborn projectile
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //follow projectile
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed* Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);

    }


}
