using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemPoleScript : MonoBehaviour
{
    public float x_Start, y_Start;
    public int columnLength, rowLength;
    public float x_Space, y_Space;
    public GameObject[] prefab;
    private Queue<GameObject> stack1, stack2, stack3, stack4;

    // Start is called before the first frame update
    void Start()
    {
        Queue<GameObject>[] arrayQueue = { stack1, stack2, stack3, stack4 };
        stack1 = new Queue<GameObject>();
        stack2 = new Queue<GameObject>();
        stack3 = new Queue<GameObject>();
        stack4 = new Queue<GameObject>();
        for (int i = 0; i < 4; i++)
        {//lägg till skit som flyttar x_start :)
            for (int j = 0; j < rowLength; j++)
            {

                GameObject tempBox = Instantiate(prefab[Random.Range(0, 4)], new Vector3(x_Start, y_Start + (y_Space * j)), Quaternion.identity);
                stack1.Enqueue(tempBox);
            }
        }


    }

    /*arrayQueue[i].Enqueue(Instantiate(prefab[Random.Range(0, 4)], new Vector3(x_Start + (x_Space * (i % columnLength)), y_Start + (y_Space * (i / columnLength))), Quaternion.identity));
    
        Queue<GameObject>[] arrayQueue = { stack1, stack2, stack3, stack4 };
        stack1 = new Queue<GameObject>();
        stack2 = new Queue<GameObject>();
        stack3 = new Queue<GameObject>();
        stack4 = new Queue<GameObject>();
        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < rowLength; j++)
            {

                GameObject tempBox = Instantiate(prefab[Random.Range(0, 4)], new Vector3(x_Start, y_Start + (y_Space * j)), Quaternion.identity);
                stack1.Enqueue(tempBox);
            }
        }

    */

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(stack1.Dequeue());

            Debug.Log("a pressed");
        }
        
    }
}
