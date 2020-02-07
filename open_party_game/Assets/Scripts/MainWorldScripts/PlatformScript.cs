using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public GameObject next_platform;
    public GameObject[] platforms;
    public bool is_goal;
    public bool is_start;

    //Add information about the type of platform

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("number of platforms: " + platforms.Length);
    }
}
