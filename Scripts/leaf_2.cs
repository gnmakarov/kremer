using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class leaf_2 : MonoBehaviour
{

    public int id;
    public bool take_leaf = false;
    level12_2 canvas;
    public void OnMouseDown()
    {
        if (canvas.player == 0) return;
        int current1 = canvas.GetComponent<level12_2>().current1;
        int current2 = canvas.GetComponent<level12_2>().current2;
        if (current1 < 0) { current1 = id; GameObject.Find(id.ToString()).GetComponent<SpriteRenderer>().color = Color.green; }
        else if (current2 < 0)
        {
            if (Math.Abs(id - current1) % 9 != 1) { }
            else { current2 = id; GameObject.Find(id.ToString()).GetComponent<SpriteRenderer>().color = Color.green; };
        }
        else
        {
            GameObject.Find(canvas.current1.ToString()).GetComponent<SpriteRenderer>().color = canvas.native_colors[current1];
            GameObject.Find(canvas.current2.ToString()).GetComponent<SpriteRenderer>().color = canvas.native_colors[current2];
            GameObject.Find(id.ToString()).GetComponent<SpriteRenderer>().color = Color.green;
            current1 = id; current2 = -1;
        };
        canvas.GetComponent<level12_2>().current1 = current1;
        canvas.GetComponent<level12_2>().current2 = current2;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<level12_2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (take_leaf)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-1, -4, 0), 0.4f);
        };
    }
}
