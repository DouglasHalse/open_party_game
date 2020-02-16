using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWorldScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player_model;
    private GameObject[] platforms;
    private Text debug_text;
    private GameObject cam;
    public bool debug;
    public bool is_idle()
    {
        //Only takes into account if an animation is running, Add scripts that needs to be ran without player-interuption in the if-statement 
        foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            if(player.GetComponent<PlayerAnimation>().enabled || GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainWorldCameraController>().is_moving())
            {
                return false;
            }
        }
        return true;
    }
    private void print_debug_info()
    {
        if(debug)
        {
            string player_info()
            {
                var temp = new System.Text.StringBuilder();
                foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
                {
                    PlayerInfo temp_player_info = player.GetComponent<PlayerInfo>();
                    temp.Append("Player: " + temp_player_info.get_player_name() + "\n" +
                        "Coins: " + temp_player_info.get_coins() +  "\n" + 
                        "Moves left: " + temp_player_info.get_moves_left() + "\n\n");
                }
                return temp.ToString();


            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + GlobalGameVariables.Instance.get_player_list().Count + "\n" +
                "Main Camera is moving: " + cam.GetComponent<MainWorldCameraController>().is_moving() + "\n" + 
                "Scene is idle: " + is_idle() + "\n\n" + 
                player_info() + "\n";
        }
        else
        {
            debug_text.text = "";
        }
    }
    private Vector3[] get_position_from_platform(GameObject platform)
    {
        Vector3[] temp = new Vector3[4];
        GameObject[] postions_platforms = platform.GetComponent<PlatformScript>().platforms;
        for(int i = 0; i<postions_platforms.Length;i++)
        {
            temp[i] = postions_platforms[i].transform.position + Vector3.up*player_model.transform.lossyScale.y; //positions + model offset
        }
        return temp;


    }
    private GameObject find_first_platform()
    {
        GameObject starting_platform = new GameObject();
        for (int i = 0; i < platforms.Length; i++)
        {
            if (platforms[i].GetComponent<PlatformScript>().is_start)
            {
                starting_platform = platforms[i];

            }
        }
        if(starting_platform.GetComponent<PlatformScript>().is_start)
        {
            return starting_platform;
        }
        else
        {
            Debug.Log("Starting Platform not found!");
            return null;
        }
    }
    public GameObject find_current_platform(GameObject actor)
    {
        GameObject closest_platform = null;
        float minDist = Mathf.Infinity;
        foreach (GameObject platform in platforms)
        {
            float dist = Vector3.Distance(platform.transform.position, actor.transform.position);
            if(dist<minDist)
            {
                minDist = dist;
                closest_platform = platform;
            }
        }
        if(closest_platform == null)
        {
            Debug.Log("Current platform not found!");
        }
        return closest_platform;
    }
    private Vector3 get_next_point(GameObject actor, int actor_id)
    {
        GameObject current_platform = find_current_platform(actor);
        GameObject next_platform = current_platform.GetComponent<PlatformScript>().next_platform;
        return next_platform.GetComponent<PlatformScript>().platforms[actor_id].transform.position;
    }
    private void move_player_nr_steps(GameObject player, int nr_steps)
    {
        if(nr_steps > steps_to_goal(player))
        {
            Debug.Log("Nr. steps more than steps to finish, steps set to: " + steps_to_goal(player));
            nr_steps = steps_to_goal(player);
        }
        Debug.Log("Player " + player.GetComponent<PlayerInfo>().get_player_id() + " is moving " + nr_steps + " space(s)");
        player.GetComponent<PlayerAnimation>().move_nr_steps(nr_steps);
    }
    private int steps_to_goal(GameObject player)
    {
        GameObject current_platform = find_current_platform(player);
        int count = 0;
        while(!current_platform.GetComponent<PlatformScript>().is_goal)
        {
            current_platform = current_platform.GetComponent<PlatformScript>().next_platform;
            count++;
        }
        return count;
    }
    private void put_playes_on_starting_platform(Vector3[] starting_positions)
    {
        int id = 0;
        foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            player.transform.position = starting_positions[id];
            id++;
        }
    }
    void Start()
    {
        //setups for debug information for on-screen overlay
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        debug_text = cam.GetComponentInChildren<Text>();

        //finds all platforms in scene, used by functions
        platforms = GameObject.FindGameObjectsWithTag("Platform");

        //Find the 4 starting positions
        Vector3[] starting_positions = get_position_from_platform(find_first_platform());

        put_playes_on_starting_platform(starting_positions);

        cam.GetComponent<MainWorldCameraController>().set_current_player(GlobalGameVariables.Instance.get_player_list()[0]);
        
    }

    //-----------TODO------------
    // Add dynamically the correct number of players as selected from main menu DONE
    // Create turn-system to give control to each player as there turn comes around, make sure to set_current player in camera controls
    // 

    // Update is called once per frame
    void Update()
    {
        
        //This if loop is ran when the game is waiting for an input
        if(is_idle())
        {

            if(Input.GetMouseButtonDown(0))
            {
                move_player_nr_steps(GlobalGameVariables.Instance.get_player_list()[0], 3);
            }

        }
        
        //Print debug information on-screen
        print_debug_info();
    }
}
