using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class level : MonoBehaviour
{
    public static int TOTAL, max_take, min_take;
    public static int[] take;
    public int player = 0; // 1 - human, 2 - computer
    public GameObject[] buttons = new GameObject[7];
    public GameObject canvas, background, background_winner, background_looser, text1, text2, text3, prefab;
    public double time;
    int din_don = 0;
    public void menu()
    {
        SceneManager.LoadScene("Scenes/Menu"); 
    }
    public void choose_player(int num)
    {
        buttons[1].SetActive(false); buttons[2].SetActive(false);
        buttons[3].SetActive(true); buttons[4].SetActive(true);
        background.SetActive(true); player = num;
        if (player == 1) { text2.SetActive(true); }
        else { text1.SetActive(true); Invoke("computer_random_take", 2.0f); };
        time = Time.time;
    }
    public void END_GAME(bool winner)
    {
        text1.SetActive(false); text2.SetActive(false); text3.SetActive(false);
        canvas.SetActive(true); player = 0; 
        if (winner) { background_winner.SetActive(true); main.winner = 2; } else { background_looser.SetActive(true); main.winner = 0; };
        
    }
    // Use this for initialization
    protected void start()
    {
        for (int i = 1; i < 7; i++) { buttons[i] = GameObject.Find("Button" + i.ToString()); };
        buttons[3].SetActive(false);buttons[4].SetActive(false);
        background = GameObject.Find("background2"); background.SetActive(false);
        background_winner = GameObject.Find("background3"); background_winner.SetActive(false);
        background_looser = GameObject.Find("background4"); background_looser.SetActive(false);
        canvas = GameObject.Find("Canvas2"); canvas.SetActive(false);
        text1 = GameObject.Find("Text (1)"); text1.SetActive(false);
        text2 = GameObject.Find("Text (2)"); text2.SetActive(false);
        text3 = GameObject.Find("Text (3)"); text3.SetActive(false);
        time = Time.time;
    }
    public void add_animation(string[] s)
    {
        prefab.AddComponent(typeof(Animation));
        prefab.GetComponent<Animation>().playAutomatically = false;
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        Keyframe[] keys = new Keyframe[9];
        keys[0] = new Keyframe(0.0f, GameObject.Find(s[0]).transform.localScale.x);
        keys[1] = new Keyframe(0.5f, 1.2f * GameObject.Find(s[0]).transform.localScale.x);
        keys[2] = new Keyframe(1.0f, GameObject.Find(s[0]).transform.localScale.x);
        keys[3] = new Keyframe(1.5f, 1.2f * GameObject.Find(s[0]).transform.localScale.x);
        keys[4] = new Keyframe(2.0f, GameObject.Find(s[0]).transform.localScale.x);
        keys[5] = new Keyframe(2.5f, 1.2f * GameObject.Find(s[0]).transform.localScale.x);
        keys[6] = new Keyframe(3.0f, GameObject.Find(s[0]).transform.localScale.x);
        keys[7] = new Keyframe(3.5f, 1.2f * GameObject.Find(s[0]).transform.localScale.x);
        keys[8] = new Keyframe(4.0f, GameObject.Find(s[0]).transform.localScale.x);
        AnimationCurve curve = new AnimationCurve(keys);
        clip.SetCurve(s[0], typeof(Transform), "localScale.x", curve);
        clip.SetCurve(s[0], typeof(Transform), "localScale.y", curve);
        for (int i = 1; i < s.Length; i++)
        {
            keys = new Keyframe[5]; Debug.Log(i);
            keys[0] = new Keyframe(2.0f, GameObject.Find(s[i]).transform.localScale.x);
            keys[1] = new Keyframe(2.5f, 1.2f * GameObject.Find(s[i]).transform.localScale.x);
            keys[2] = new Keyframe(3.0f, GameObject.Find(s[i]).transform.localScale.x);
            keys[3] = new Keyframe(3.5f, 1.2f * GameObject.Find(s[i]).transform.localScale.x);
            keys[4] = new Keyframe(4.0f, GameObject.Find(s[i]).transform.localScale.x);
            curve = new AnimationCurve(keys);
            clip.SetCurve(s[i], typeof(Transform), "localScale.x", curve);
            clip.SetCurve(s[i], typeof(Transform), "localScale.y", curve);
        }
        prefab.GetComponent<Animation>().AddClip(clip, "1");
    }
    public void music(int i)
    {
        if (i == 0) { GetComponent<AudioSource>().Play(); }
        else {buttons[i].GetComponent<AudioSource>().Play();};
    }

    // Update is called once per frame
    protected void update()
    {
        //https://ru.stackoverflow.com/questions/763876/unity-%D0%92%D1%80%D0%B5%D0%BC%D1%8F-%D0%B1%D0%B5%D0%B7%D0%B4%D0%B5%D0%B9%D1%81%D1%82%D0%B2%D0%B8%D1%8F
        //if (Input.anyKeyDown) { time = Time.time; };
        if (Time.time - time > 7+din_don*15 && player == 0) { time = Time.time; GetComponent<Animation>().Play();din_don++; };
    }
}
