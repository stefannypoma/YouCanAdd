using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public Button leftButton;
    public Button middleButton;
    public Button rightButton;
    public Text text;
    public System.Random rnd;
    public AudioSource audioUno;
    public AudioSource audioDos;
    public AudioSource audioTres;
    public AudioSource audioCuatro;
    public AudioSource audioCinco;
    public AudioSource audioSeis;
    public AudioSource audioSiete;
    public AudioSource audioOcho;
    public AudioSource audioNueve;
    public AudioSource audioDiez;
    public AudioSource audioCorrecto;
    public AudioSource audioFallo;

    private Answer[] availableAnswers;
    private Button[] buttons;
    private int correctPosition;
    private AudioSource source;

    //trying
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    // Use this for initialization
    void Start()
    {
        rnd = new System.Random();
        buttons = new Button[3] { leftButton, middleButton, rightButton };

        availableAnswers = new Answer[10] {
            new Answer("unapelotita", "1"),
            new Answer("dospatitos", "2"),
            new Answer("trespanditas", "3"),
            new Answer("cuatromariposas", "4"),
            new Answer("cincoestrellitas", "5"),
            new Answer("seisperritos", "6"),
            new Answer("sietebuhos", "7"),
            new Answer("ochoabejitas", "8"),
            new Answer("nueveoreos", "9"),
            new Answer("diezcandy", "10")};

        this.Next();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Next()
    {
        List<Answer> answers = getRandomUnreapeatedAnswers();
        for (int i = 0; i < 3; i++)
        {
            buttons[i].image.sprite = Resources.Load<Sprite>(answers[i].image);
        }
        correctPosition = rnd.Next(3);
        text.text = answers[correctPosition].name;
    }

    private List<Answer> getRandomUnreapeatedAnswers()
    {
        List<Answer> result = new List<Answer>();
        do
        {
            int r = rnd.Next(10);
            Answer answer = availableAnswers[r];
            if (!result.Contains(answer)) result.Add(answer);

        } while (result.Count < 3);
        return result;
    }

    public void OnLeftClick()
    {
        if (correctPosition == 0)
        {
            audioCorrecto.Play();
            StartCoroutine(DoNextAfterDelay(1.488f));
        }
    }

    IEnumerator DoNextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Next();
    }

    public void OnMiddleClick()
    {
        if (correctPosition == 1)
        {
            audioCorrecto.Play();
            StartCoroutine(DoNextAfterDelay(1.488f));
        }
    }

    public void OnRightClick()
    {
        if (correctPosition == 2)
        {
            audioCorrecto.Play();
            StartCoroutine(DoNextAfterDelay(1.488f));
        }
    }

    private class Answer
    {
        public string image;
        public string name;

        public Answer(string image, string name)
        {
            this.image = image;
            this.name = name;
        }
    }
}