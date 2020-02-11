using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] players;
    public GameObject projectile;
    private GameObject rotater;
    private GameObject barrel;
    private GameObject current_target;
    
    public GameObject get_current_target()
    {
        return current_target;
    }
    private Quaternion get_direction()
    {
        Vector3 player_pos = current_target.transform.position;
        player_pos.y = 0;
        Quaternion dir = Quaternion.LookRotation(player_pos, Vector3.up);
        
        return dir;
    }
    public void set_target(GameObject target)
    {
        this.current_target = target;
    }

    public void fire_turret()
    {
        //Vector3 fire_dir = (current_target.transform.position - barrel.transform.position).normalized;
        Vector3 direction = barrel.transform.up;
        Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
        rot.SetEulerAngles(90 * Mathf.Deg2Rad, Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y * Mathf.Deg2Rad, 0);
        GameObject bullet = Instantiate(projectile, barrel.transform.position + barrel.transform.up, rot);
        bullet.GetComponent<BulletScript>().setup_and_shoot(current_target, 10f, 20f, 5f);

    }
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rotater = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        barrel = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        current_target = players[0];

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            fire_turret();
        }
        rotater.transform.rotation = get_direction();
    }
}
