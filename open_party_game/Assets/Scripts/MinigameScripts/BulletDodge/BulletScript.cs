using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject target;
    private float speed = 10f;
    private float rotating_speed = 20f;
    private float self_destruct_time = 5f;
    public void setup_and_shoot(GameObject target, float speed, float rotating_speed, float self_destruct_timer)
    {
        this.target = target;
        this.speed = speed;
        this.rotating_speed = rotating_speed;
        this.self_destruct_time = self_destruct_timer;
        this.enabled = true;
    }
    public void set_target(GameObject target)
    {
        this.target = target;
    }
    public void set_speed(int speed)
    {
        this.speed = speed;
    }
    public void set_rotating_speed(int rotating_speed)
    {
        this.rotating_speed = rotating_speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.)
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInfo>().give_minigame_damage(1);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "projectile")
        {
            Destroy(this.gameObject);
        }
    }
    private Quaternion look_at_target()
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized;
        Quaternion rot = Quaternion.Euler(90, Quaternion.LookRotation(direction, Vector3.up).eulerAngles.y, 0);
        return rot;
    }
    private void move_forward()
    {
        Vector3 frw_dir = this.gameObject.transform.up.normalized;
        this.transform.Translate(frw_dir * Time.deltaTime * speed, Space.World);
        //this.transform.position += frw_dir * Time.deltaTime * speed;
    }
    private void rotate_toward_target()
    {
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, look_at_target(), rotating_speed * Time.deltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, self_destruct_time);
    }

    // Update is called once per frame
    void Update()
    {
        move_forward();
        rotate_toward_target();
    }
}
