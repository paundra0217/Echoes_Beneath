using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] GameObject waypoint;
    [SerializeField] GameObject waypoint2;
    float speed = 4f;
    bool move, back;

    void Start()
    {
        move = false;
        back = false;
    }

    void Update()
    {
        if(move)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed * Time.deltaTime);
        }
        if(back)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, speed * Time.deltaTime);
            if(transform.position == waypoint2.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Move()
    {
        move = true;
    }

    public void Back()
    {
        // transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, speed * Time.deltaTime);
        move = false;
        back = true;
    }
}
