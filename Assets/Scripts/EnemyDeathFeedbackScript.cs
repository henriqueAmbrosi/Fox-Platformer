using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathFeedbackScript : MonoBehaviour
{
    public void DestroyEnemyFeedback()
    {
        //Debug.Log(gameObject);
        Destroy(gameObject);
    }
}
