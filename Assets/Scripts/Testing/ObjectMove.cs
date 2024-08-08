using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] GameObject _object;
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
            _object.transform.position = Vector3.MoveTowards(_object.transform.position, waypoint.transform.position, speed * Time.deltaTime);
        }
        if(back)
        {
            _object.transform.position = Vector3.MoveTowards(_object.transform.position, waypoint2.transform.position, speed * Time.deltaTime);
            if(_object.transform.position == waypoint2.transform.position)
            {
                Destroy(gameObject);
            }
        }
    }

    public void _Move()
    {
        move = true;
    }

    public void _Back()
    {
        // transform.position = Vector3.MoveTowards(transform.position, waypoint2.transform.position, speed * Time.deltaTime);
        move = false;
        back = true;
    }
}
