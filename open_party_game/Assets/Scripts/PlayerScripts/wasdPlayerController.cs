using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasdPlayerController : MonoBehaviour
{
    public GameObject cam;
    public GameObject actor;
    private Vector3 ortho;
    private Vector3 get_cam_to_actor()
    {
        Vector3 cam_to_actor = actor.transform.position - cam.transform.position;
        cam_to_actor.y = 0f;
        return cam_to_actor;
    }
    // Start is called before the first frame update
    void Start()
    {
        ortho = Vector3.Cross(get_cam_to_actor(), Vector3.up).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            actor.transform.position += get_cam_to_actor().normalized * 5f * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            actor.transform.position += ortho * 5f * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            actor.transform.position -= get_cam_to_actor().normalized * 5f * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            actor.transform.position -= ortho * 5f * Time.deltaTime;
        }
        



        ortho = Vector3.Cross(get_cam_to_actor(), Vector3.up).normalized;
    }
}
