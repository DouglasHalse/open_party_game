using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWorldCameraController : MonoBehaviour
{
    public GameObject cam;
    public GameObject[] players;
    private GameObject[] cam_pos;
    private GameObject current_player;
    private GameObject current_cam_pos;
    private GameObject next_cam_pos;
    private Vector3 current_pos_to_next_pos;
    private bool moving;
    private float t;
    public bool is_moving()
    {
        if(t == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private GameObject find_closest_cam_pos(GameObject player)
    {
        float minDist = Mathf.Infinity;
        GameObject closest_position = null;

        foreach(GameObject position in cam_pos)
        {
            float dist = Vector3.Distance(player.transform.position, position.transform.position);
            if(dist < minDist)
            {
                closest_position = position;
                minDist = dist;
            }
        }
        return closest_position;
    }
    public void set_current_player(GameObject player)
    {
        current_player = player;
    }
    // Start is called before the first frame update
    void Start()
    {
        //players = GameObject.FindGameObjectsWithTag("Player");
        cam_pos = GameObject.FindGameObjectsWithTag("CamPos");
        //current_player = players[0];
        current_cam_pos = cam_pos[0];
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(players.Length == 0)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            current_player = players[0];
        }

        //New position found
        if(current_cam_pos != find_closest_cam_pos(current_player) && !moving)
        {
            next_cam_pos = find_closest_cam_pos(current_player);
            current_pos_to_next_pos = next_cam_pos.transform.position - current_cam_pos.transform.position;
            t = 0f;
            moving = true;
        }
        //Move camera if moving tag set
        if(moving)
        {
            cam.transform.position = current_cam_pos.transform.position + current_pos_to_next_pos * t;
            t += 1f * Time.deltaTime;
            //Reset variables if movement is done
            if(t >= 1f)
            {
                moving = false;
                t = 0;
                current_cam_pos = next_cam_pos;
            }
        }

        //Temporary camera controller
        var target_rotation = Quaternion.LookRotation(current_player.transform.position - cam.transform.position);
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, target_rotation, 2 * Time.deltaTime);
    }
}
