﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Visualizer : MonoBehaviour {
    float duration = 1;
    float alpha = 0;
    Color ColorTrans;

    bool animationFinished = false;
    public Scene nextScene;

    public GameObject scaleObject;
    public GameObject ball;

    public GameObject building;
    private GameObject[] comps;
    private Color[] origColors;
    public GameObject zoomie;

    private List<GameObject> positions = new List<GameObject>();
    private List<GameObject> boxes = new List<GameObject>();
    private List<float> scales = new List<float>();
    // Use this for initialization
    void Start () {
        
        
        int children = building.transform.childCount;
        comps = new GameObject[children];
        origColors = new Color[children];
        for (int i = 0; i < children; ++i)
        {
            try
            {
                //print("For loop: " + transform.GetChild(i));
                comps[i] = building.transform.GetChild(i).gameObject;
                origColors[i] = building.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;
            }
            catch { };
        }
        
    }
    
    // Update is called once per frame
    // when  you press teh bue buttons, it hides everything that isnt blue, when you press the red it hides everything that isnt red, when you press green it hides evertyhing that wasnt green
    void Update () {

        if (Input.GetKeyDown(KeyCode.B))
        {
            RestoreColors();
            FadeoutEverythingOtherThan("blue");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            RestoreColors();
            FadeoutEverythingOtherThan("green");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestoreColors();
            FadeoutEverythingOtherThan("red");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            RestoreColors();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            print("called i");
            StartCoroutine(ScaleUp(building, zoomie));
           // StartCoroutine(waitforNewScne());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(ScaleDown(building, zoomie));
        }
        Vector3 speed = new Vector3(2, 2, 2);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ball.transform.position = ball.transform.position + new Vector3(0, 0,-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ball.transform.position = ball.transform.position + new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ball.transform.position = ball.transform.position + new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ball.transform.position = ball.transform.position + new Vector3(0, 0, 1);
        }

    }
    private void RestoreColors()
    {
        for (int i = 0; i < comps.Length; ++i)
        {
            StartCoroutine(FadeIn(comps[i].gameObject));

        }
    }
    IEnumerator waitforNewScne()
    {
        for (float f = 1f; f <= 5f; f += 0.1f)
        {
            yield return new WaitForSeconds(.000000005f);
        }
        ChangeScene();
        
    }
    IEnumerator ScaleUp(GameObject g, GameObject position)
    {
        g.transform.parent = position.transform;
        print(g.transform.parent);

        for (float f = 1f; f <= 5f; f += 0.1f)
        {
            position.transform.localScale = new Vector3(f,f,f);
            yield return new WaitForSeconds(.000000005f);
            
        }
        g.transform.parent = null;
    }
    IEnumerator ScaleDown(GameObject g, GameObject position)
    {
        g.transform.parent = position.transform;
        print(g.transform.parent);
        for (float f = 5f; f >= 1f; f -= 0.1f)
        {
            position.transform.localScale = new Vector3(f, f, f);
            yield return new WaitForSeconds(.000000005f);
        }
        g.transform.parent = null;
    }
    private void FadeoutEverythingOtherThan(string colorTag)
    {
        foreach (GameObject g in comps)
        {
            if (g.tag != colorTag)
            {
                StartCoroutine(FadeOut(g));
            }
        }
    }
    IEnumerator FadeOut(GameObject g)
    {

        if(g.GetComponent<Renderer>().material.color != null)
        {
            for (float f = 1f; f >= .33; f -= 0.01f)
            {

                //print("called " + f);
                Color c = g.GetComponent<Renderer>().material.color;
                c.a = f;
                g.GetComponent<Renderer>().material.color = c;
                yield return new WaitForSeconds(.0005f);
            }
        }
        
    }
    IEnumerator FadeIn(GameObject g)
    {

        if (g.GetComponent<Renderer>().material.color != null)
        {

            for (float f = .33f; f <= 1f; f += 0.01f)
            {

                //print("called " + f);
                Color c = g.GetComponent<Renderer>().material.color;
                c.a = f;
                g.GetComponent<Renderer>().material.color = c;
                yield return new WaitForSeconds(.0005f);
            }
        }
    }

    void ChangeScene()
    {
        Application.LoadLevel("staircase");
    }

}



