using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDashMovement : MonoBehaviour
{
    public bool enable_wasd_override;
    public float max_movement_speed = 5f;
    private bool cooldown_active = false;
    private float cooldown_timer = 0f;
    private float cooldown_timer_target = 1f;
    public bool cooldown_ready()
    {
        if (cooldown_timer == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Starts time with set timer
    public void start_cooldown_timer()
    {
        if (!cooldown_active)
        {
            cooldown_timer = 0f;
            cooldown_active = true;
        }

    }
    //set timer cooldown time
    public void set_cooldown_time_target(float time)
    {
        this.cooldown_timer_target = time;
    }
    //Util function for running the timer
    private void cooldown_runner()
    {
        if (cooldown_active)
        {
            cooldown_timer += Time.deltaTime;
            //Reset timer when done
            if (cooldown_timer >= cooldown_timer_target)
            {
                cooldown_active = false;
                cooldown_timer = 0f;
            }
        }
    }
    //Temnporary WASD controll for debug, enabled in editor for a player
    private void wasd_controller(bool enable)
    {
        if(enable)
        {
            Rigidbody body = this.GetComponent<Rigidbody>();

            if (Input.GetKey(KeyCode.A))
            {
                body.AddForce(Vector3.left * 3f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                body.AddForce(Vector3.right * 3f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                body.AddForce(Vector3.forward * 3f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                body.AddForce(Vector3.back * 3f);
            }
            if (body.velocity.magnitude > max_movement_speed)
            {
                float diff = body.velocity.magnitude - max_movement_speed;
                body.AddForce((-body.velocity.normalized) * diff);
            }
        }

    }
    //Dash function to boost a players speed in a current direction
    private void dash()
    {
        Rigidbody body = this.GetComponent<Rigidbody>();
        if (!cooldown_active)
        {
            start_cooldown_timer();
            body.AddForce(body.velocity*100f);
        }
    }
    //collision with stuff
    private void OnCollisionEnter(Collision collision)
    {
        //Kills player if it falls of
        if(collision.gameObject.name == "DeathCube")
        {
            this.GetComponent<PlayerInfo>().give_minigame_damage(9999);
        }
        //Applies forces to knock players if they run into eachother
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody my_bod = this.GetComponent<Rigidbody>();
            Rigidbody other_bod = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 my_vel = my_bod.velocity;
            Vector3 other_vel = other_bod.velocity;
            my_bod.AddForce(-my_vel+other_vel);
            other_bod.AddForce(-other_vel + my_vel*30f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wasd_controller(enable_wasd_override);
        cooldown_runner();
        //Temp keybinding to dash
        if (Input.GetMouseButtonDown(0))
        {
            dash();
        }

    }
}
