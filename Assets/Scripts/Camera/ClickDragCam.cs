﻿using UnityEngine;

public class ClickDragCam : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0.0f, pos.y * dragSpeed);

        transform.Translate(move, Space.World);
    }
}