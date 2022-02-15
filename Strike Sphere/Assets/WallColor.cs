using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }
}
