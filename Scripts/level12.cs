using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;


public class level12 : level
{
    int total;
    public int current1 = -1, current2 = -1;
    public GameObject[] leaf = new GameObject[13];
    public Color[] native_colors = new Color[13];
    Vector3[] initial_position = new Vector3[13];
    int[] massive;

    public void human_take()
    {
        if (current1 < 0) { return; };        
        leaf[current1].GetComponent<leaf>().take_leaf = true; total--; massive[current1]= 0;
        leaf[current1].GetComponent<SpriteRenderer>().color = native_colors[current1];
        if (current2 > 0) { leaf[current2].GetComponent<leaf>().take_leaf = true; total--; massive[current2] = 0;
                            leaf[current2].GetComponent<SpriteRenderer>().color = native_colors[current2];};
        if (total == 0) { END_GAME(true); return; };
        text2.SetActive(false); text3.SetActive(true);
        if (player == 1) { Invoke("comp_take", 1.5f); } else { Invoke("computer_random_take", 1.5f); };

    }
    public void comp_take()
    {
        current1 = (current1 + 6) % 12; if (current1 == 0) { current1 = 12; };
        leaf[current1].GetComponent<leaf>().take_leaf = true; total--; 
        leaf[current1].GetComponent<SpriteRenderer>().color = native_colors[current1];
        if (current2 > 0)
        {   current2 = (current2 + 6) % 12; if (current2 == 0) { current2 = 12;};
            leaf[current2].GetComponent<leaf>().take_leaf = true;total--;
            leaf[current2].GetComponent<SpriteRenderer>().color = native_colors[current2];};
        current1 = -1; current2 = -1;
        if (total == 0) { END_GAME(false); return; };
        text1.SetActive(false); text2.SetActive(true); text3.SetActive(false);
    }

    public void computer_random_take()
    {   // Random.Range(int,int): max is exclusive 
        while (massive[current1] != 1 && total>0) { int c = Random.Range(1, 3); Debug.Log(c);
            if (c > 1)
            {   current1 = Random.Range(2, 12);
                if (massive[current1 + 1] != 0) { current2 = current1 + 1; }
                else if (massive[current1 - 1] != 0) { current2 = current1 - 1; }
                else { current2 = -1; }
            }
            else { current1 = Random.Range(2, 12); current2 = -1; };
            Debug.Log(current1);
            Debug.Log(current2);
        };

        leaf[current1].GetComponent<leaf>().take_leaf = true; total--; massive[current1] = 0;
        leaf[current1].GetComponent<SpriteRenderer>().color = native_colors[current1];
        if (current2 > 0)
        {   leaf[current2].GetComponent<leaf>().take_leaf = true; total--; massive[current2] = 0;
            leaf[current2].GetComponent<SpriteRenderer>().color = native_colors[current2]; };
        current1 = -1; current2 = -1;
        if (total == 0) { END_GAME(false); return; };
        text1.SetActive(false); text2.SetActive(true); text3.SetActive(false);
    }

    public void restart()
    {
        player = 0; current1 = -1; current2 = -1; massive[0] = 0; total = 12;
        for (int i = 0; i < 13; i++) { leaf[i].transform.position = initial_position[i];
            leaf[i].GetComponent<leaf>().take_leaf = false; leaf[i].GetComponent<SpriteRenderer>().color = native_colors[i]; 
            massive[i] = 1; };
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(true); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        background.SetActive(false); text1.SetActive(false); text2.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        start(); total = TOTAL; massive = new int[TOTAL + 1]; 
        for (int i=0; i<TOTAL+1; i++) {leaf[i] = GameObject.Find(i.ToString()); massive[i] = 1;
            leaf[i].GetComponent<leaf>().id = i; native_colors[i] = leaf[i].GetComponent<SpriteRenderer>().color;
            initial_position[i] = leaf[i].transform.position;};
        massive[0] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        update();
    }
}
