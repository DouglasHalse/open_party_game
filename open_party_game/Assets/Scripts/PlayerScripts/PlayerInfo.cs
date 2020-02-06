using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    private string player_name;
    private int coins;
    private int moves_left;
    private GameObject body;
    public void setup_player(string player_name)
    {
        this.player_name = player_name;
        this.coins = 0;
        this.moves_left = 0;
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
    public string get_player_name()
    {
        return this.player_name;
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
