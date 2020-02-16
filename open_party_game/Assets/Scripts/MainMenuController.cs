using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject player_model;
    private Text debug_text;
    private GameObject cam;
    public bool debug;
    private Vector3[] get_starting_platforms()
    {
        Vector3[] positions = new Vector3[4];
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        int id = 0;
        foreach (GameObject platform in platforms)
        {
            positions[id] = platform.transform.position;
            id++;
        }
        return positions;
    }
    private Vector3[] starting_position;
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
                    temp.Append("Player ID: " + temp_player_info.get_player_id() + "\n" + 
                        "Player name: " + temp_player_info.get_player_name() + "\n\n");
                }
                return temp.ToString();


            }
            debug_text.text = "Debug information: \n" +
                "Number of players: " + GlobalGameVariables.Instance.get_player_list().Count + "\n\n" +
                player_info() + "\n";
        }
        else
        {
            debug_text.text = "";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        debug_text = cam.GetComponentInChildren<Text>();


        starting_position = get_starting_platforms();


        GlobalGameVariables.Instance.add_player("Steve", 0, player_model, starting_position[0]);
        GlobalGameVariables.Instance.add_player("John", 1, player_model, starting_position[1]);
        GlobalGameVariables.Instance.add_player("Adam", 2, player_model, starting_position[2]);
        GlobalGameVariables.Instance.add_player("Eva", 3, player_model, starting_position[3]);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("MainWorld");
        }
        print_debug_info();
    }
}
