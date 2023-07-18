using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[RequireComponent(typeof(Border))] 
public class Moving_block : MonoBehaviour
{

    [Header("Move down")]
    public float speed_move = 0.5f;
    public float speed_Down = 0.5f;

    [Header("Stop")]
    public bool stop = false;
    public GameObject[] block_junior;
    public float time_to_Stop = 0.5f;
    private Border border;
    private Spawn_block spawn_Block;
    private Display_blocks display_Blocks;

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
        spawn_Block = GameObject.Find("Spawn_block").GetComponent <Spawn_block>();
        display_Blocks = GameObject.Find("Display_board").GetComponent<Display_blocks>();
        time_to_Stop = spawn_Block.time_to_Stop_Present;
        move_Down_Slow();
    }

    void Update()
    {    
        if (!stop)
            move();
    }

    public void spawn()
    {
        spawn_Block.SpawnObject();
        display_Blocks.ActivateRandomObject();
    }

    public void move_Down_Slow()
    {
        transform.Translate(Vector2.down * speed_Down, Space.World);
        border.Invoke("time_delay_to_stop", time_to_Stop);
    }    

    private void move()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && border.check_side_block() == true && gameObject.name != "Block (6)(Clone)")
        {                      
            transform.Rotate(0, 0, transform.position.z - 90f, Space.World);
            border.check_border_block();
        }    

        if (Input.GetKeyDown(KeyCode.DownArrow) && border.check_block_Down() == false)
            transform.Translate(Vector2.down * speed_Down, Space.World); 

        if (Input.GetKeyDown(KeyCode.LeftArrow) && border.check_block_L() == true)
        {
            transform.Translate(Vector2.left * speed_move, Space.World);
            border.check_border_block();
        }    
        else if (Input.GetKeyDown(KeyCode.RightArrow) && border.check_block_R() == true)
        {   
            transform.Translate(Vector2.right * speed_move, Space.World);
            border.check_border_block();
        }

        transform.position = new Vector2(Mathf.Floor(transform.position.x / 0.1f) * 0.1f, transform.position.y);
    }

}
