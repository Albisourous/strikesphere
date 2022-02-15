using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    // Based on https://www.youtube.com/watch?v=Q-_J9S6NaC0&t=381s&ab_channel=MuddyWolf
    public float power = .2f;
    public float maxDrag = 5f;
    public float maxDistance = 1f;
    public Rigidbody2D rb;
    public LineRenderer lr;
    
    Vector3 dragStartPos;
    Vector3 objectStartPos;
    Touch touch;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && rb.velocity.magnitude == 0f)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
        }
        else if (rb.velocity.magnitude <= .5)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void DragStart()
    {
        objectStartPos = gameObject.transform.position;
        objectStartPos.z = 0f;
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, objectStartPos);
    }

    void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;

        Vector3 offset = draggingPos - dragStartPos;
        offset.z = 0f;
        // Normalize pos
        if (offset.magnitude > maxDistance)
        {
            offset = offset.normalized * maxDistance;
        }
        Vector3 endPoint = offset + gameObject.transform.position;
        endPoint.z = 0;
        lr.SetPosition(1, endPoint);
        if(offset.magnitude > .5f)
        {
            lr.startColor = Color.red;
        }
        else
        {
            lr.startColor = Color.white;
        }
    }

    void DragRelease()
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag * power);

        Debug.Log(clampedForce.magnitude);
        // Make sure that there is a min threshold, so the user can change their decision
        if(clampedForce.magnitude > .5f)
        {
            rb.AddForce(clampedForce, ForceMode2D.Impulse);
        }
    }
}
