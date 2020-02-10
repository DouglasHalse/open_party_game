using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosInformation : MonoBehaviour
{
    public GameObject next_cam_pos;
    public bool is_start_pos;
    public bool is_end_pos;
    // Start is called before the first frame update
    void Start()
    {
        if(next_cam_pos == null && !is_end_pos)
        {
            Debug.Log("CamPos Incorrectly linked! Make sure all CamPos have the right settings!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
