using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject actor;
    private Vector3 start_point, middle_point, end_point;
    private float t;
    private void set_points( Vector3 end_point)
    {
        this.start_point = actor.transform.position;
        this.end_point = end_point + actor.transform.lossyScale.y * Vector3.up;
        this.middle_point = ((start_point + end_point) / 2) + Vector3.up * 4;
        t = 0;
    }
    private Vector3 get_bez_point(float t)
    {
        return (1f - t) * ((1f - t) * start_point + t * middle_point) + t * ((1f - t) * middle_point + t * end_point);
    }
    public void jump_to(Vector3 end_point)
    {
        set_points(end_point);
        this.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actor.transform.position = get_bez_point(t);
        if(t>=1f)
        {
            this.enabled = false;
        }

        t += Time.deltaTime;
    }
}
