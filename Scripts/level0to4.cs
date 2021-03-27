using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System;


public class level0to4 : level
{
    GameObject bracket1, bracket2;
    GameObject[] candies;
    int[] massive; // -1 - chosen, 0 - absent, 1 - available
    int choose = 0;

    public void on_click(int id)
    {
        if (player != 1) { return; };

        if (choose == max_take) { for (int j = 0; j < TOTAL; j++) { if (massive[j] == -1) { massive[j] = 1; candies[j].GetComponent<Image>().color = Color.white; }; };
                                  massive[id] = -1; choose = 1; highlighte(id); return;};
        massive[id] = -1; choose++; highlighte(id);
    }
    public void human_take()
    {
        if (player != 1) { return; };
        if (!Array.Exists(take, element => element == choose)) { prefab.GetComponent<Animation>().Play("1"); return; };

        for (int j = 0; j < TOTAL; j++) { if (massive[j] == -1) { massive[j] = 0; candies[j].SetActive(false); }; }; choose = 0;

        if (massive.Sum() == 0) { END_GAME(true); return; }
        text2.SetActive(false); text3.SetActive(true);
        player = 2; Invoke("computer_random_take", 1.5f);
    }

    public void computer_random_take()
    {
        for (int i = 0; i < take.Length; i++) {
            if (massive.Sum() % (min_take + max_take) == take[i]) { for (int j=0; j < take[i]; j++) { int a = Array.IndexOf(massive,1);  massive[a] = 0; candies[a].SetActive(false); };
                                                                    text1.SetActive(false); text2.SetActive(true); text3.SetActive(false); player = 1;
                                                                    if (massive.Sum() == 0) { END_GAME(false); return; }; return; };};
        // Random.Range(int,int): max is exclusive 
        int c = UnityEngine.Random.Range(0, take.Length);
        for (int j = 0; j < take[c]; j++) { int a = Array.IndexOf(massive, 1); massive[a] = 0; candies[a].SetActive(false); };
        text1.SetActive(false); text2.SetActive(true); text3.SetActive(false); player = 1;
        if (massive.Sum() == 0) { END_GAME(false); return; };
    }
    void highlighte(int id)
    { 
        if (id % 3 == 1) { candies[id].GetComponent<Image>().color = Color.red; };
        if (id % 3 == 2) { candies[id].GetComponent<Image>().color = Color.blue; };
        if (id % 3 == 0) { candies[id].GetComponent<Image>().color = Color.green; };
    }

    public void restart()
    {
        player = 0;
        for (int i = 1; i < 3; i++) { buttons[i].SetActive(true); };
        for (int i = 3; i < 5; i++) { buttons[i].SetActive(false); };
        background.SetActive(false); text1.SetActive(false); text2.SetActive(false);
        for (int i = 0; i < TOTAL; i++) { candies[i].SetActive(true);  candies[i].GetComponent<Image>().color = Color.white; massive[i] = 1; };
    }
    public void clue()
    {
        bracket1.SetActive(true); bracket2.SetActive(true);
    }
    public void no_clue()
    {
        bracket1.SetActive(false); bracket2.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        start();
        text2.GetComponent<Text>().text = "              Ваш ход,\nвозьмите " + min_take.ToString() + " или " + max_take.ToString() + " палочки: ";
        candies = new GameObject[TOTAL + 1]; massive = new int[TOTAL];
        if (TOTAL == 3 || TOTAL == 9) { buttons[5].SetActive(false); };
        switch (TOTAL)
        {
            case 3: prefab = Instantiate(Resources.Load("3sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                bracket1 = GameObject.Find("bracket1"); bracket1.SetActive(false);
                bracket2 = GameObject.Find("bracket2"); bracket2.SetActive(false);
                break;
            case 6: prefab = Instantiate(Resources.Load("6sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                bracket1 = GameObject.Find("bracket1"); bracket1.SetActive(false);
                bracket2 = GameObject.Find("bracket2"); bracket2.SetActive(false);
                break;
            case 8: prefab = Instantiate(Resources.Load("8sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                bracket1 = GameObject.Find("bracket1"); bracket1.SetActive(false);
                bracket2 = GameObject.Find("bracket2"); bracket2.SetActive(false);
                break;
            case 9: prefab = Instantiate(Resources.Load("9sticks") as GameObject, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                    break;
        }
        for (int i=0; i< TOTAL; i++) { candies[i] = GameObject.Find("Candy" + (i+1).ToString()); 
                                           int j = i; //sth magic, we create own variable for each step
                                           candies[i].GetComponent<Button>().onClick.AddListener(() => on_click(j)); massive[i] = 1;};

        add_animation(new string[]{ "Candy1", "Candy2"});
    }
    private void Update()
    {
        update();
        if (Time.time - time > 7 && player == 1 && massive.Sum() == TOTAL) { time = Time.time; prefab.GetComponent<Animation>().Play("1"); };
    }
}
