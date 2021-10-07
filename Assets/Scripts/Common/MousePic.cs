using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
/*

林哲

更换鼠标指针图标控制脚本

12月4日

*/
public class MousePic : MonoBehaviour
{
    private Texture2D mouse;

    private Texture2D mouse_hit;

    void Start()
    {
        mouse = Resources.Load<Texture2D>("UI/Common/mouse");

        mouse_hit= Resources.Load<Texture2D>("UI/Common/mouse_hit");

        Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(mouse_hit, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(mouse_hit, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
        }
    }
}
