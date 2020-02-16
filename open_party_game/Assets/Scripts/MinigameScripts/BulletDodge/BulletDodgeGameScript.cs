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
    public float round_timer = 0f;
    public float change_target_timer = 0f;
    public int phase = 0;
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
            string cannon_info()
            {
                var temp = new System.Text.StringBuilder();
                int id = 0;
                foreach (GameObject cannon in cannons)
                {
                    TurretScript temp_cannon_info = cannon.GetComponent<TurretScript>();
                    string temp_name = "None";
                    if(temp_cannon_info.get_current_target()!= null)
                    {
                        temp_name = temp_cannon_info.get_current_target().GetComponent<PlayerInfo>().get_player_name();
                    }
                    temp.Append("Cannon: " + id + "\n" +
                        "Current Target: " + temp_name + "\n" +
                        "Fire-delay: " + temp_cannon_info.get_fire_rate() + "\n" + 
                        "t: " + temp_cannon_info.get_t() + "\n\n");
                    id++;
                }
                return temp.ToString();
            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + GlobalGameVariables.Instance.get_nr_of_players() + "\n" +
                "Phase: " + phase + "\n\n" + 
                player_info() + "\n" + 
                cannon_info() + "\n";
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
        if(GlobalGameVariables.Instance.get_nr_of_players() != 0)
        {
            int id = 0;
            foreach (GameObject player in GlobalGameVariables.Instance.get_player_list())
            {
                Vector3 start_pos = starting_platform[id].transform.position;
                player.transform.position = start_pos;
                id++;
            }
        }
        else
        {
            Debug.LogWarning("No players found in GlobalGameVariables, Creating debug-player...");
            GlobalGameVariables.Instance.add_player("debug-player", 0, player_model, starting_platform[0].transform.position);
        }
        
    }
    private void fire_cannon(int cannon_id)
    {
        cannons[cannon_id].GetComponent<TurretScript>().fire_turret();
    }
    private void select_random_target(GameObject cannon)
    {
        cannon.GetComponent<TurretScript>().set_target(GlobalGameVariables.Instance.get_player_list()[Random.Range(0, GlobalGameVariables.Instance.get_nr_of_players())]);
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
            //cannon.GetComponent<TurretScript>().set_target_and_run(GlobalGameVariables.Instance.get_player_list()[0]);
            cannon.GetComponent<TurretScript>().set_projectile_speed(20f);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fire cannons at different intervalls
        if(round_timer > 3f)
        {
            if(phase == 0)
            {
                foreach(GameObject cannon in cannons)
                {
                    select_random_target(cannon);
                }
                phase = 1;
            }

        }
        if(round_timer > 5f && phase == 1)
        {
            foreach (GameObject cannon in cannons)
            {
                cannon.GetComponent<TurretScript>().set_fire_rate(Random.Range(5f, 10f));
                cannon.GetComponent<TurretScript>().toggle_firing();
            }
            phase++;
        }
        if(round_timer > 20f && phase == 2)
        {
            foreach (GameObject cannon in cannons)
            {
                cannon.GetComponent<TurretScript>().set_fire_rate(Random.Range(3f, 5f));
                cannon.GetComponent<TurretScript>().toggle_firing();
            }
            phase++;
        }
        if(round_timer > 40f && phase == 3)
        {
            foreach (GameObject cannon in cannons)
            {
                cannon.GetComponent<TurretScript>().set_fire_rate(Random.Range(1f, 2f));
                cannon.GetComponent<TurretScript>().toggle_firing();
            }
            phase++;
        }
        if(round_timer > 60f && phase == 4)
        {
            foreach (GameObject cannon in cannons)
            {
                cannon.GetComponent<TurretScript>().set_fire_rate(Random.Range(0.7f, 1f));
                cannon.GetComponent<TurretScript>().toggle_firing();
            }
            phase++;
        }
        if(round_timer > 80f && phase == 5)
        {
            foreach (GameObject cannon in cannons)
            {
                cannon.GetComponent<TurretScript>().set_fire_rate(Random.Range(0.2f, 0.7f));
                cannon.GetComponent<TurretScript>().toggle_firing();
            }
            phase++;
        }


        if(phase < 3)
        {
            if (change_target_timer > 2f)
            {
                foreach (GameObject cannon in cannons)
                {
                    select_random_target(cannon);
                }
                change_target_timer = 0f;
            }
        }
        if(phase == 3)
        {
            if (change_target_timer > 1.5f)
            {
                foreach (GameObject cannon in cannons)
                {
                    select_random_target(cannon);
                }
                change_target_timer = 0f;
            }
        }
        if(phase == 4)
        {
            if (change_target_timer > 1f)
            {
                foreach (GameObject cannon in cannons)
                {
                    select_random_target(cannon);
                }
                change_target_timer = 0f;
            }
        }
        if (phase == 5)
        {
            if (change_target_timer > 0.7f)
            {
                foreach (GameObject cannon in cannons)
                {
                    select_random_target(cannon);
                }
                change_target_timer = 0f;
            }
        }
        if (phase == 6)
        {
            if (change_target_timer > 0.7f)
            {
                foreach (GameObject cannon in cannons)
                {
                    select_random_target(cannon);
                }
                change_target_timer = 0f;
            }
        }


        change_target_timer += Time.deltaTime;
        round_timer += Time.deltaTime;
        print_debug_info();
    }
}
