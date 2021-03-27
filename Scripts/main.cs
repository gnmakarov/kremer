using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System;


public class main : level
{
    static int level = 0;
    public static int winner = 1; // 0 -looser, 1 - exit from level, 2 - winner
    public void laod_level(string InputString)
    {
        //InputString format: level_levelber total min_take ... max_take
        string[] inputs = InputString.Split(' ');
        if (level < Convert.ToInt32(inputs[0])) { GameObject.Find("Button (" + level.ToString() + ")").GetComponent<Animation>().Play(); return; };
        //level = Convert.ToInt32(inputs[0]); //remove
        TOTAL = Convert.ToInt32(inputs[1]);
        take = new int[inputs.Length - 2];
        for (int i = 2; i < inputs.Length; i++) { take[i - 2] = Convert.ToInt32(inputs[i]); };
        min_take = take[0];
        max_take = take[take.Length-1];
        if (Convert.ToInt32(inputs[0]) < 4) { SceneManager.LoadScene("Scenes/0to4level"); };
        if (6 >= Convert.ToInt32(inputs[0]) && Convert.ToInt32(inputs[0]) >= 4) { SceneManager.LoadScene("Scenes/" + "5level"); };
        if (Convert.ToInt32(inputs[0]) == 7) { SceneManager.LoadScene("Scenes/" + "12level"); };
        if (Convert.ToInt32(inputs[0]) == 8) { SceneManager.LoadScene("Scenes/" + "12level_2"); };

    }
    // Start is called before the first frame update
    void Start()
    {
        if (winner == 2) { for (int i = 0; i < level + 1; i++) { GameObject.Find("Button (" + i.ToString() + ")").GetComponent<Image>().sprite = Resources.Load<Sprite>(i.ToString() + "done"); }; level++; winner = 1; }
        else if (winner == 1) { for (int i = 0; i < level; i++) { GameObject.Find("Button (" + i.ToString() + ")").GetComponent<Image>().sprite = Resources.Load<Sprite>(i.ToString() + "done"); }; winner = 1; }
        else { for (int i = 0; i < level - 1; i++) { GameObject.Find("Button (" + i.ToString() + ")").GetComponent<Image>().sprite = Resources.Load<Sprite>(i.ToString() + "done"); }; level--; if (level < 0) { level = 0; }; winner = 1; };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
