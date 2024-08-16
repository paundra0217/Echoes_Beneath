using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityMove : MonoBehaviour
{
    [SerializeField] GameObject _object;
    [SerializeField] GameObject waypoint;
    [SerializeField] float speed = 12f;
    bool move,back;
    Vector3 origin;

    void Start()
    {
        move = false;
        back = false;
        origin = _object.transform.position;
    }

    void Update()
    {
        if(move)
        {
            _object.transform.position = Vector3.MoveTowards(_object.transform.position, waypoint.transform.position, speed * Time.deltaTime);
            if(_object.transform.position == waypoint.transform.position)
            {
                move = false;
                _object.transform.position = origin;
            }
        }
    }

    public void _Move()
    {
        move = true;
    }
}
