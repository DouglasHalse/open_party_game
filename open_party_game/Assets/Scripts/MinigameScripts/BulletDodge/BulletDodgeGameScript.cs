using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletDodgeGameScript : MonoBehaviour
{
    public GameObject[] cannons;
    public GameObject player_model;
    public bool debug;
    private Text debug_text;
    private int fired = 0;
    private void print_debug_info()
    {
        if (debug)
        {
            string player_info()
            {
                var temp = new System.Text.StringBuilder();
                foreach (GameObject player in GlobalGameVariables.Instance.get_player_list())
                {
                    PlayerInfo temp_player_info = player.GetComponent<PlayerInfo>();
                    temp.Append("Player: " + temp_player_info.get_player_name() + "\n" +
                        "Health: " + temp_player_info.get_minigame_health() + "\n" +
                        "Is alive: " + temp_player_info.is_alive_minigame() + "\n\n");
                }
                return temp.ToString();


            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + GlobalGameVariables.Instance.get_nr_of_players() + "\n\n" +
                player_info() + "\n";
        }
        else
        {
            debug_text.text = "";
        }
    }
    private void give_player_wasd_controlls(GameObject player)
    {
        player.AddComponent<wasdPlayerController>();
        player.GetComponent<wasdPlayerController>().cam = GameObject.FindGameObjectWithTag("MainCamera");
        player.GetComponent<wasdPlayerController>().actor = player.gameObject;
    }
    private void put_playes_on_starting_platform(GameObject[] starting_platform)
    {
        int id = 0;
        foreach (GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            Vector3 start_pos = starting_platform[id].transform.position;
            player.transform.position = start_pos;
            id++;
        }
    }

    private void setup_players()
    {
        foreach(GameObject player in GlobalGameVariables.Instance.get_player_list())
        {
            player.GetComponent<PlayerInfo>().set_minigame_health(10);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Setup debug text
        debug_text = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Text>();
        //Setup spawn locations
        GameObject[] spawn_platforms = GameObject.FindGameObjectsWithTag("Platform");

        put_playes_on_starting_platform(spawn_platforms);
        setup_players();

        give_player_wasd_controlls(GlobalGameVariables.Instance.get_player_list()[0]);
        
        cannons = GameObject.FindGameObjectsWithTag("turret");
        //setup all cannons and target player1
        foreach(GameObject cannon in cannons)
        {
            cannon.GetComponent<TurretScript>().set_target_and_run(GlobalGameVariables.Instance.get_player_list()[0]);
            cannon.GetComponent<TurretScript>().set_projectile_speed(20f);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fire cannons at different intervalls
        if(fired%600==200)
        {
            cannons[0].GetComponent<TurretScript>().fire_turret();
        }
        if (fired % 500 == 400)
        {
            cannons[1].GetComponent<TurretScript>().fire_turret();
        }
        if (fired % 600 == 0)
        {
            cannons[2].GetComponent<TurretScript>().fire_turret();
        }
        fired++;
        print_debug_info();
    }
}
