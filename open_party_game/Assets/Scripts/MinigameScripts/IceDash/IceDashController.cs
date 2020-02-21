using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceDashController : MonoBehaviour
{
    private List<GameObject> dead_players;
    private GameObject[] get_spawn_platforms()
    {
        return GameObject.FindGameObjectsWithTag("Platform");
    }
    private bool game_stopped = false;
    private GameObject cam;
    private Text debug_text;
    private GameObject cube_platform;
    public bool debug;
    public GameObject debug_player_model;
    public float t = 0;
    private void print_debug_info()
    {
        if(debug)
        {
            string player_info()
            {
                var temp = new System.Text.StringBuilder();
                foreach (GameObject player in GlobalGameVariables.Instance.get_player_list())
                {
                    PlayerInfo temp_player_info = player.GetComponent<PlayerInfo>();
                    temp.Append("Player: " + temp_player_info.get_player_name() + "\n" +
                        "Coins: " + temp_player_info.get_coins() + "\n" + 
                        "Can dash: " + player.GetComponent<IceDashMovement>().cooldown_ready() + "\n" +
                        "Is alive: " + temp_player_info.is_alive_minigame() + "\n\n");
                }
                return temp.ToString();
            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + GlobalGameVariables.Instance.get_nr_of_players() + "\n" +
                "Game over: " + game_stopped + "\n" + 
                "Time: " + t + "\n\n" +
                player_info() + "\n";
        }
        else
        {
            debug_text.text = "";
        }
    }
    //puts players from global list onto platforms and then deletes the platforms, Spawns debug player if list is empty
    private void put_playes_on_starting_platform()
    {
        GameObject[] spawn_platforms = get_spawn_platforms();
        if (GlobalGameVariables.Instance.get_nr_of_players() != 0)
        {
            int id = 0;
            foreach (GameObject player in GlobalGameVariables.Instance.get_player_list())
            {
                Vector3 start_pos = spawn_platforms[id].transform.position;
                player.transform.position = start_pos + Vector3.up;
                id++;
            }
        }
        else
        {
            Debug.LogWarning("No players found in GlobalGameVariables, Creating debug-player...");
            GlobalGameVariables.Instance.add_player("debug-player1", 0, debug_player_model, spawn_platforms[0].transform.position + Vector3.up);
            GlobalGameVariables.Instance.add_player("debug-player2", 0, debug_player_model, spawn_platforms[1].transform.position + Vector3.up);
            GlobalGameVariables.Instance.add_player("debug-player3", 0, debug_player_model, spawn_platforms[2].transform.position + Vector3.up);
            GlobalGameVariables.Instance.add_player("debug-player4", 0, debug_player_model, spawn_platforms[3].transform.position + Vector3.up);
        }
        foreach (GameObject platform in spawn_platforms)
        {
            Destroy(platform);
        }

    }
    //Gives movements script to all players
    private void give_movement_script_to_players()
    {
        foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            player.AddComponent<IceDashMovement>();
        }
    }
    //Removes the script when minigame is done
    private void remove_movement_script_from_players()
    {
        foreach (GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            Destroy(player.GetComponent<IceDashMovement>());
        }
    }
    //checks and adds dead players to dead_players list
    private void add_dead_players(List<GameObject> dead_players)
    {
        foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            if(!player.GetComponent<PlayerInfo>().is_alive_minigame() && !dead_players.Contains(player))
            {
                dead_players.Add(player);
            }
        }
    }
    private void game_over()
    {
        game_stopped = true;
        //dead_players.Reverse();
        
        foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            if(player.GetComponent<PlayerInfo>().is_alive_minigame())
            {
                player.GetComponent<PlayerInfo>().give_coins(500);
            }
        }

        int nr_of_players = GlobalGameVariables.Instance.get_nr_of_players();
        int i = nr_of_players;
        foreach (GameObject player in dead_players)
        {
            player.GetComponent<PlayerInfo>().give_coins(nr_of_players*100 - 100 * i);
            i--;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        dead_players = new List<GameObject>();
        cube_platform = GameObject.Find("Cube");
        cam = GameObject.Find("Main Camera");
        debug_text = cam.GetComponentInChildren<Text>();
        
        put_playes_on_starting_platform();
        give_movement_script_to_players();





    }
    // Update is called once per frame
    void Update()
    {
        if(!game_stopped)
        {
            //Phase logic
            if (10f < t && t < 20f)
            {
                cube_platform.transform.localScale -= new Vector3(Time.deltaTime, 0, Time.deltaTime) / 15f;
            }
            if (20f < t && t < 40f)
            {
                cube_platform.transform.localScale -= new Vector3(Time.deltaTime, 0, Time.deltaTime) / 10f;
            }
            if (40f < t && t < 60f)
            {
                cube_platform.transform.localScale -= new Vector3(Time.deltaTime, 0, Time.deltaTime) / 7f;
            }
            if (60f < t && t < 90f)
            {
                cube_platform.transform.localScale -= new Vector3(Time.deltaTime, 0, Time.deltaTime) / 5f;
            }
            if (90f < t && cube_platform)
            {
                //Remove cube is the scale is going to be 0
                if (cube_platform.transform.localScale.x - Time.deltaTime / 3f < 0f)
                {
                    Destroy(cube_platform);
                }
                else
                {
                    cube_platform.transform.localScale -= new Vector3(Time.deltaTime, 0, Time.deltaTime) / 3f;
                }
            }


            add_dead_players(dead_players);
            //Game over called when all but one player is dead
            if (dead_players.Count >= GlobalGameVariables.Instance.get_nr_of_players() - 1)
            {
                game_over();
            }
        }
        //Game stopped
        else
        {

        }
        

        print_debug_info();
        t += Time.deltaTime;
    }
    private void OnDestroy()
    {
        //Ran upon leaving scene
        remove_movement_script_from_players();
    }
}
