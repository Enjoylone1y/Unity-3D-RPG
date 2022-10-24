using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Texture2D[] cursors;

    private enum CursorIndex
    {
        Normal,
        Collectible
    };

    private Dictionary<CursorIndex, Texture2D> cursorMap;
    private RaycastHit hitInfo;
    private Ray ray;

    void Awake()
    {
        cursorMap = new Dictionary<CursorIndex, Texture2D>();
        for (int i = 0; i < cursors.Length; i++)
        {
            string name = cursors[i].name.Split('_')[1];
            if ("Normal".Equals(name))
            {
                cursorMap.Add(CursorIndex.Normal, cursors[i]);
            }
            else if("Hand".Equals(name))
            {
                cursorMap.Add(CursorIndex.Collectible, cursors[i]);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.Log(hitInfo.collider.gameObject.tag);
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.red, 1000);

            switch (hitInfo.collider.gameObject.tag)
            {
                case "Collectible":
                    Cursor.SetCursor(cursorMap[CursorIndex.Collectible], new Vector2(16, 16), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(cursorMap[CursorIndex.Normal], new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
        }

    }
}
