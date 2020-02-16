using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameVariables : Singleton<GlobalGameVariables>
{
    protected GlobalGameVariables() { }

    //global player_list
    private List<GameObject> player_list = new List<GameObject>();
    public int get_nr_of_players()
    {
        return player_list.Count;
    }
    public List<GameObject> get_player_list()
    {
        return this.player_list;
    }
    public void add_player(string name, int id, GameObject body, Vector3 position)
    {
        //PlayerInfo player = GetComponent<PlayerInfo>();
        GameObject player = Instantiate(body, position, Quaternion.identity);
        DontDestroyOnLoad(player);
        player.GetComponent<PlayerInfo>().setup_player(name, id, body);
        player_list.Add(player);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
