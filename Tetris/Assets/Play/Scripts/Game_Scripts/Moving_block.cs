using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

[RequireComponent(typeof(Border))] 
public class Moving_block : MonoBehaviour
{

    [Header("Move down")]
    public float speed_move = 0.5f;
    public float speed_Down = 0.5f;
    public float time_to_Delay;

    [Header("Stop")]
    public bool stop_Game;
    public bool stop;
    public GameObject[] block_junior;
    private Border border;
    
    [Header("Delete")]
    private float address_Destroy_Y = -5f;
    private bool cancelled;
    private int quantity_destroy;

    private Spawn_block spawn_Block;
    private Display_blocks display_Blocks;
    private Delete_block delete_Block;

    private void Awake()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>();
        block_junior = new GameObject[childTransforms.Length - 1];
        for (int i = 1; i < childTransforms.Length; i++)
        {
            block_junior[i-1] = childTransforms[i].gameObject;
        }      
    }
    void Start()
    {
        border = GetComponent<Border>();
        spawn_Block = GameObject.Find("Spawn_block").GetComponent<Spawn_block>();
        display_Blocks = GameObject.Find("Display_board").GetComponent<Display_blocks>();
        delete_Block = GameObject.Find("Block_detection").GetComponent<Delete_block>();

        time_to_Delay = spawn_Block.time_to_Stop_Present;
        Invoke("Move_and_stop", 0.1f);     
    }
    void Update()
    {    
        if (!stop)
            move();
    }

    public void Spawn()
    {      
        if (!stop_Game)
        {
            spawn_Block.SpawnObject();
            display_Blocks.ActivateRandomObject();
        }
    }       

    private void move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stop = true;
            Vector2 teleport_Distance = Teleport_Distance();
            transform.Translate(teleport_Distance, Space.World);

            Invoke("Stop_and_destroy", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && border.Check_next_move() == true && gameObject.name != "Block (6)(Clone)")
        {                      
            transform.Rotate(0, 0, transform.position.z - 90f, Space.World);
            border.Check_border_block();
        }    

        if (Input.GetKeyDown(KeyCode.DownArrow) && border.Check_block_Down() == false)
            transform.Translate(Vector2.down * speed_Down, Space.World); 

        if (Input.GetKeyDown(KeyCode.LeftArrow) && border.Check_block_L() == true)
        {
            transform.Translate(Vector2.left * speed_move, Space.World);
            border.Check_border_block();
        }    
        else if (Input.GetKeyDown(KeyCode.RightArrow) && border.Check_block_R() == true)
        {   
            transform.Translate(Vector2.right * speed_move, Space.World);
            border.Check_border_block();
        }

        transform.position = new Vector2(Mathf.Floor(transform.position.x / 0.1f) * 0.1f, transform.position.y);
    }

    private void Move_and_stop()
    {
        if (!stop && border.Check_block_Down() == true)
        {
            stop = true;
            Stop_and_destroy();
        }
        else if (!stop)
        {
            transform.Translate(Vector2.down * speed_Down, Space.World);
            border.Check_border_block();
            Invoke("Move_and_stop", time_to_Delay);
        }    
    }

    public void Stop_and_destroy()
    {
        foreach (var item in block_junior)
            if (item.transform.position.y >= 4)
            {
                stop_Game = true;
                spawn_Block.EndGame();
                goto End;
            }    


        foreach (GameObject jun in block_junior)
        {
            jun.gameObject.layer = 3;
            delete_Block.transform.position = new Vector2(delete_Block.transform.position.x, jun.transform.position.y);
            if (delete_Block.enough_Counting())
            {
                if (delete_Block.transform.position.y >= address_Destroy_Y)
                    address_Destroy_Y = delete_Block.transform.position.y;
                delete_Block.destroy_Block();
                cancelled = true;
                quantity_destroy++;
            }
        }

        if (cancelled)
            do
            {
                delete_Block.move_Block_after_destroy(address_Destroy_Y);
                quantity_destroy--;
            } while (quantity_destroy > 0);

        Invoke("Spawn", 0.5f);
        End:;
    }

    private Vector2 Teleport_Distance()
    {
        float shortestDistance = Mathf.Infinity;

        foreach (var item in block_junior)
        {
            RaycastHit2D hit = Physics2D.Raycast(item.transform.position, Vector2.down, border.raycastDistance * (1 + 18 * 2), border.layerMask_Stop);

            float distance = Vector2.Distance(item.transform.position, hit.point);
            if (hit.collider != null && distance < shortestDistance)
                shortestDistance = distance;
        }

        return new Vector2(0, -shortestDistance + 0.25f);
    }

}
