using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWorldScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> player_list = new List<GameObject>();
    public GameObject player_model;
    private GameObject[] platforms;
    private Text debug_text;
    public bool debug;
    private void print_debug_info()
    {
        if(debug)
        {
            string player_info()
            {
                var temp = new System.Text.StringBuilder();
                foreach(GameObject player in player_list)
                {
                    PlayerInfo temp_player_info = player.GetComponent<PlayerInfo>();
                    temp.Append("Player: " + temp_player_info.get_player_name() + "\n" +
                        "Coins: " + temp_player_info.get_coins() +  "\n" + 
                        "Moves left: " + temp_player_info.get_moves_left() + "\n\n");
                }
                return temp.ToString();


            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + player_list.Count + "\n\n" +
                player_info() + "\n";
        }
        else
        {
            debug_text.text = "";
        }
    }
    private void add_players_to_list()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            player_list.Add(players[i]);
        }
    }
    private Vector3[] get_position_from_platform(GameObject platform)
    {
        Vector3[] temp = new Vector3[4];
        GameObject[] postions_platforms = platform.GetComponent<PlatformScript>().platforms;
        for(int i = 0; i<postions_platforms.Length;i++)
        {
            temp[i] = postions_platforms[i].transform.position;
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
    void Start()
    {
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        debug_text = cam.GetComponentInChildren<Text>();
        platforms = GameObject.FindGameObjectsWithTag("Platform");

        //Creates player 1 and puts it on position 0 on starting platform
        GameObject player1 = Instantiate(player_model, get_position_from_platform(find_first_platform())[0], Quaternion.identity);
        player1.GetComponent<PlayerInfo>().setup_player("Player 1");

        //Creates player 2 and puts it on position 0 on second platform
        GameObject player2 = GameObject.Instantiate(player_model, get_position_from_platform(find_first_platform().GetComponent<PlatformScript>().next_platform)[0], Quaternion.identity);
        player2.GetComponent<PlayerInfo>().setup_player("Player 2");

        //Adds all players to global player list
        add_players_to_list();
    }

    // Update is called once per frame
    void Update()
    {
        print_debug_info();

        
    }
}
