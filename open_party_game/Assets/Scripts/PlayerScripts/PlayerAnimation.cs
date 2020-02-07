using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject actor;
    private Vector3 start_point, middle_point, end_point;
    private float t;
    private int steps_remaining = 0;
    private void set_points(Vector3 end_point)
    {
        this.start_point = actor.transform.position;

        //End point + player model offset
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
    private GameObject find_current_platform(GameObject actor)
    {
        GameObject closest_platform = null;
        float minDist = Mathf.Infinity;
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject platform in platforms)
        {
            float dist = Vector3.Distance(platform.transform.position, actor.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest_platform = platform;
            }
        }
        if (closest_platform == null)
        {
            Debug.Log("Current platform not found!");
        }
        return closest_platform;
    }
    private void move_one_step()
    {
        int player_id = actor.GetComponent<PlayerInfo>().get_player_id();
        GameObject current_platform = find_current_platform(actor);
        GameObject next_platform = current_platform.GetComponent<PlatformScript>().next_platform;
        Vector3 goal_point = next_platform.GetComponent<PlatformScript>().platforms[player_id].transform.position;
        jump_to(goal_point);
        steps_remaining--;
        actor.GetComponent<PlayerInfo>().set_moves_left(steps_remaining);
    }
    public void move_nr_steps(int steps)
    {
        
        steps_remaining = steps;
        move_one_step();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moves actor along bezier curve
        actor.transform.position = get_bez_point(t);

        //End condition, no more steps in animation
        if(t>=1f && steps_remaining == 0)
        {
            actor.GetComponent<PlayerInfo>().interact_with_platform();
            this.enabled = false;

        }
        //End of step but more steps are planned
        else if(t>=1)
        {
            move_nr_steps(steps_remaining);
        }


        //advance time
        t += Time.deltaTime;
    }
}
