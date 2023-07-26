using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Border : MonoBehaviour
{   
    [Header("Down")]
    public float raycastDistance = 0.25f;
    public LayerMask layerMask_Stop;

    [Header("Left Right")]
    public float raycastDistance_left_right = 0.25f;

    [Header("Turn")]
    public GameObject[] block_Test;

    private Moving_block moving_Block;

    void Start()
    {
        moving_Block = GetComponent<Moving_block>();

        Transform[] childTransforms = GameObject.Find(gameObject.name + " -Test").GetComponentsInChildren<Transform>();
        block_Test = new GameObject[childTransforms.Length];
        for (int i = 0; i < childTransforms.Length; i++)
        {
            block_Test[i] = childTransforms[i].gameObject;
        }      
    }



    // kiểm tra xem có bị dính vào tường ko
    public void Check_border_block ()
    {
        foreach (GameObject block in moving_Block.block_junior)
        {
            // Nếu block dính vào block khác --> dịch lên 1 ô
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right * 0.5f, layerMask_Stop))
                transform.Translate(Vector2.up * moving_Block.speed_Down, Space.World);

            // Nếu có block bị dính vào tường --> dịnh sang bên
            if (block.transform.position.x <= -6)
                transform.Translate(Vector2.right * moving_Block.speed_move, Space.World);
            else if (block.transform.position.x >= 2)
                transform.Translate(Vector2.left * moving_Block.speed_move, Space.World);
        }
    }

    // kiểm tra xem nếu quay thì có xảy ta va chạm không
    public bool Check_next_move()
    {
        // sự di chuyển của block thay thế 
        block_Test[0].transform.position = transform.position;
        block_Test[0].transform.rotation = transform.rotation;
        block_Test[0].transform.Rotate(0, 0, transform.position.z - 90f, Space.World);

        foreach (GameObject block in block_Test)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right * 0.5f, layerMask_Stop))
                block_Test[0].transform.Translate(Vector2.up * moving_Block.speed_Down, Space.World);

        // kiểm tra 
        foreach (GameObject block in block_Test)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right * 0.5f, layerMask_Stop))
                return false;
        return true;
    }

    // Kiểm tra xem bưới tiết theo xuống dưới block có vật thể gì ko
    public bool Check_block_Down()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.down, raycastDistance, layerMask_Stop))
                return true;
        return false;
    }

    // Kiểm tra xem bên trái block có vật thể gì ko 
    public bool Check_block_L()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right, layerMask_Stop))
                return false;
        return true;
    }

    // Kiểm tra xem bên phải block có vật thể gì ko
    public bool Check_block_R()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.right, raycastDistance_left_right, layerMask_Stop))
                return false;
        return true;
    }
}
