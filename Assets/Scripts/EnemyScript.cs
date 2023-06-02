using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 6;
    public GameObject edgeCheck;

    RaycastHit2D groundInfo;
    public float rayDistance = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        groundInfo = Physics2D.Raycast(edgeCheck.transform.position, Vector2.down, rayDistance);

        if (!groundInfo.collider)
        {
            // Inverte a direção do inimigo
            speed = speed * -1;

            // Inverte o sprite do inimigo
            Vector3 tempScale = transform.localScale;
            tempScale.x = -tempScale.x;
            transform.localScale = tempScale;
        }
    }
}
