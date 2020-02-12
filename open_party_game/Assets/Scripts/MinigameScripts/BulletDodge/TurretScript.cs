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
    private float projectile_speed = 10f;
    private float projectile_turning_speed = 20f;
    private float projectile_self_destruct_time = 5f;
    public void set_projectile_speed(float speed)
    {
        this.projectile_speed = speed;
    }
    public void set_projectile_turning_speed(float turning_speed)
    {
        this.projectile_turning_speed = turning_speed;
    }
    public void set_projectile_self_destruct_time(float time)
    {
        this.projectile_self_destruct_time = time;
    }
    public GameObject get_current_target()
    {
        return current_target;
    }
    private Quaternion get_direction()
    {
        Vector3 player_pos = current_target.transform.position - this.transform.position;
        player_pos.y = 0;
        Quaternion dir = Quaternion.LookRotation(player_pos, Vector3.up);
        
        return dir;
    }
    public void set_target(GameObject target)
    {
        this.current_target = target;
    }
    public void set_target_and_run(GameObject target)
    {
        set_target(target);
        //this.enabled = true;
    }
    public void fire_turret()
    {
        Vector3 direction = this.barrel.transform.up;
        Quaternion rot = Quaternion.Euler(90, Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y, 0);
        GameObject bullet = Instantiate(projectile, this.barrel.transform.position + this.barrel.transform.up, rot);
        bullet.GetComponent<BulletScript>().setup_and_shoot(current_target, projectile_speed, projectile_turning_speed, projectile_self_destruct_time);

    }
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        rotater = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        barrel = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        rotater.transform.rotation = Quaternion.Slerp(rotater.transform.rotation, get_direction(), 2f * Time.deltaTime);
    }
}
