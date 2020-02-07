using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    private string player_name;
    private int coins;
    private int moves_left;
    private int player_id;
    private GameObject body;
    public void setup_player(string player_name, int player_id)
    {
        this.player_name = player_name;
        this.coins = 0;
        this.moves_left = 0;
        this.player_id = player_id;
        this.body = this.gameObject;
    }
    public int get_coins()
    {
        return this.coins;
    }
    public int get_moves_left()
    {
        return this.moves_left;
    }
    public void set_moves_left(int moves)
    {
        this.moves_left = moves;
    }
    public string get_player_name()
    {
        return this.player_name;
    }
    public int get_player_id()
    {
        return player_id;
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
