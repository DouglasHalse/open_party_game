using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletDodgeGameScript : MonoBehaviour
{
    public GameObject[] players;
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
                foreach (GameObject player in players)
                {
                    PlayerInfo temp_player_info = player.GetComponent<PlayerInfo>();
                    temp.Append("Player: " + temp_player_info.get_player_name() + "\n" +
                        "Health: " + temp_player_info.get_minigame_health() + "\n" +
                        "Is alive: " + temp_player_info.is_alive_minigame() + "\n\n");
                }
                return temp.ToString();


            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + players.Length + "\n\n" +
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

    private void setup_player(string name, int ID, int player_comtroller)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //Setup debug text
        debug_text = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Text>();
        //Setup spawn locations
        GameObject[] spawn_platforms = GameObject.FindGameObjectsWithTag("Platform");

        //Spawn player 1 with appropriate settings and give it wasd controlls
        GameObject player1 = Instantiate(player_model, spawn_platforms[0].transform.position, Quaternion.identity);
        player1.GetComponent<PlayerInfo>().setup_player("Player 1", 0);
        player1.GetComponent<PlayerInfo>().set_minigame_health(10);
        give_player_wasd_controlls(player1);
        
        players = GameObject.FindGameObjectsWithTag("Player");
        cannons = GameObject.FindGameObjectsWithTag("turret");
        //setup all cannons and target player1
        foreach(GameObject cannon in cannons)
        {
            cannon.GetComponent<TurretScript>().set_target_and_run(player1);
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
