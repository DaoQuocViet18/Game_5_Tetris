using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Delete_block : MonoBehaviour
{
    [Header("block detection")]
    public float raycastDistance = 0.125f;
    public LayerMask layerMask_Stop;

    [Header("Move down")]
    public float speed_Down = 0.5f;

    [Header("Score")]
    private Display_blocks display_Blocks;

    private void Start()
    {
        display_Blocks = GameObject.Find("Display_board").GetComponent<Display_blocks>();
    }

    public  bool enough_Counting()
    {
        int collisionCount = 0;
        for (int i = 1; i <= 15; i++)
        {
            Vector2 tra = new Vector2(transform.position.x + i * 0.5f, transform.position.y);
            if (Physics2D.Raycast(tra, Vector2.left, raycastDistance, layerMask_Stop))
                collisionCount++;   
            else
                return false;
        }
        return true;
    }

    public void destroy_Block()
    {
        for (int i = 1; i <= 15; i++)
        {
            Vector2 tra = new Vector2(transform.position.x + i * 0.5f, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(tra, Vector2.left, raycastDistance, layerMask_Stop);
            if (hit.collider != null)
                Destroy(hit.collider.gameObject);
        }
        display_Blocks.IncreaseScore(1);

    }
    public void move_Block_after_destroy(float posY)
    {     
        do
        {
            posY += 0.5f;
            transform.position = new Vector2(transform.position.x, posY);
            for (int i = 1; i <= 15; i++)
            {
                Vector2 tra = new Vector2(transform.position.x + i * 0.5f, transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(tra, Vector2.left, raycastDistance, layerMask_Stop);
                if (hit.collider != null)
                    hit.collider.gameObject.transform.Translate(Vector2.down * speed_Down, Space.World);
            }
        } while (transform.position.y < 5);
    }    
}
