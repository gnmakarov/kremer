using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;


public class level5 : level 
{
    GameObject circle;
    GameObject[] rects;
    bool MouseDown = false;
    int current;
    
    public void Pointer_Down()
    {
        if (player != 1) { return; };
        MouseDown = true;
    }
    public void Pointer_Up()
    {
        if (player != 1) { return; };

        if (rectangle.NUMBER < current && rectangle.NUMBER >= current - max_take) { circle.transform.position = rects[rectangle.NUMBER].transform.position; current = rectangle.NUMBER; }                                             
        else if (rectangle.NUMBER < current - max_take) { current = current - max_take; circle.transform.position = rects[current].transform.position;}
        else { circle.transform.position = rects[current].transform.position; MouseDown = false; return; };

        if (current == 0) { END_GAME(true); MouseDown = false; return; };
        MouseDown = false; text2.SetActive(false); text3.SetActive(true);
        player = 2; Invoke("computer_random_take", 2.0f);
    }

    public void computer_random_take()
    {
        for (int i=0; i < take.Length; i++) {
            if (current % (min_take + max_take) == take[i]) {current -= current % (min_take + max_take); circle.transform.position = rects[current].transform.position;
                                                             text1.SetActive(false); text2.SetActive(true); text3.SetActive(false); player = 1;
                                                             if (current == 0) { END_GAME(false); MouseDown = false; return; }; return; }; };
        // Random.Range(int,int): max is exclusive 
        int c = UnityEngine.Random.Range(0, take.Length);
        current -= take[c]; circle.transform.position = rects[current].transform.position;
        text1.SetActive(false); text2.SetActive(true); text3.SetActive(false); player = 1;
        if (current == 0) { END_GAME(false); MouseDown = false; return; };
    }

    public void restart()
    {
        player = 0; current = TOTAL - 1; 
        circle.transform.position = rects[current].transform.position;
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(true); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        background.SetActive(false); text1.SetActive(false); text2.SetActive(false);
    }
    public void clue()
    {
        int c = max_take + min_take;
        for (int i=0; i < TOTAL; i++)
        {          
            if (i / c % c == 1) { rects[i].GetComponent<Image>().color = new Color(0.9f,0.8f,0.8f,0.5f); };
            if (i / c % c == 2) { rects[i].GetComponent<Image>().color = new Color(0.7f, 0.9f, 0.8f, 0.5f); };
            if (i / c % c == 0) { rects[i].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.9f, 0.5f); };
        };
    }
    public void no_clue()
    {
        for (int i = 0; i < TOTAL; i++) { rects[i].GetComponent<Image>().color = Color.white;};
    }
    // Start is called before the first frame update
    void Start()
    {
        start();
        text2.GetComponent<Text>().text = "                     Ваш ход,\nпередвиньте фишку на " + min_take.ToString() + " или " + max_take.ToString() + " шага: ";
        rects = new GameObject[TOTAL];
        current = TOTAL - 1;
        switch (TOTAL)
        {
            case 10:
                Instantiate(Resources.Load("10steps") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                break;
            case 12:
                Instantiate(Resources.Load("12steps") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                break;
            case 13:
                Instantiate(Resources.Load("13steps") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                break;
            case 40:
                Instantiate(Resources.Load("40steps") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                break;
        }        
        
        for (int i = 0; i < TOTAL; i++) { rects[i] = GameObject.Find(i.ToString()); };
        Vector3 pos = rects[TOTAL - 1].transform.position;
        circle = Instantiate(GameObject.Find("circle"), new Vector3(pos.x, pos.y, 0), Quaternion.identity, GameObject.Find("Canvas").transform); ; 
        GameObject.Find("circle").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        update();
        if (MouseDown) { Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        circle.transform.position = new Vector3(pos.x,pos.y,0); };
    }
}
