using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Border : MonoBehaviour
{
    private Moving_block moving_Block;
    [Header("Down")]
    public float raycastDistance = 1f;
    public LayerMask layerMask_Stop;

    [Header("Left Right")]
    public float raycastDistance_left_right = 1f;

    [Header("Turn")]
    public GameObject[] block_Test;
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

    public void time_delay_to_stop()
    {
        if (!moving_Block.stop)
            foreach (GameObject block in moving_Block.block_junior)
                if (Physics2D.Raycast(block.transform.position, Vector2.down, raycastDistance, layerMask_Stop) && !moving_Block.stop)
                {
                    moving_Block.stop = true;
                    foreach (GameObject jun in moving_Block.block_junior)
                        jun.gameObject.layer = 3;
                    moving_Block.Invoke("spawn", 1);
                    break;
                }

        if (!moving_Block.stop)
            moving_Block.move_Down_Slow();   
    }

    #region kiểm tra 
    // kiểm tra xem có bị dính vào tường ko
    public void check_border_block ()
    {
        // Nếu có block bị dính vào tường --> dịnh sang bên
        foreach (GameObject block in moving_Block.block_junior)
        {
            // Nếu block dính vào block khác --> dịch lên 1 ô
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right * 0.5f, layerMask_Stop))
                transform.Translate(Vector2.up * moving_Block.speed_Down, Space.World);

            if (block.transform.position.x <= -6)
                transform.Translate(Vector2.right * moving_Block.speed_move, Space.World);
            else if (block.transform.position.x >= 2)
                transform.Translate(Vector2.left * moving_Block.speed_move, Space.World);
        }
    }

    // kiểm tra xem nếu quay thì có xảy ta va chạm không
    public bool check_side_block()
    {
        #region sự di chuyển block thay thế 
        block_Test[0].transform.position = transform.position;
        block_Test[0].transform.rotation = transform.rotation;
        block_Test[0].transform.Rotate(0, 0, transform.position.z - 90f, Space.World);

        foreach (GameObject block in block_Test)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right * 0.5f, layerMask_Stop))
                block_Test[0].transform.Translate(Vector2.up * moving_Block.speed_Down, Space.World);
        #endregion

        foreach (GameObject block in block_Test)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right * 0.5f, layerMask_Stop))
                return false;
        return true;
    }

    // Kiểm tra xem bưới tiết theo xuống dưới block có vật thể gì ko
    public bool check_block_Down()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.down, raycastDistance * 3, layerMask_Stop))
                return true;
        return false;
    }

    // Kiểm tra xem bên trái block có vật thể gì ko 
    public bool check_block_L()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.left, raycastDistance_left_right, layerMask_Stop))
                return false;
        return true;
    }

    // Kiểm tra xem bên phải block có vật thể gì ko
    public bool check_block_R()
    {
        foreach (GameObject block in moving_Block.block_junior)
            if (Physics2D.Raycast(block.transform.position, Vector2.right, raycastDistance_left_right, layerMask_Stop))
                return false;
        return true;
    }
    #endregion
}
