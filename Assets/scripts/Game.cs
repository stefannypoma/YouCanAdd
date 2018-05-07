using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Game : MonoBehaviour {

    public enum Mode { DIEZ, VIENTE }

    public Image leftImage;
    public Image middleImage;
    public Image rightImage;
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

    public Mode modo;

    private Answer[] availableAnswers;
    private Image[] images;
    private int correctPosition;
    private AudioSource source;
    private Boolean isAudioPlaying = false;

    private float time;
    private string timerString;

    private DatabaseReference currentGameReference;

    //trying
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    // Use this for initialization
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://youcanadd-ffbe4.firebaseio.com");

        rnd = new System.Random();
        this.images = new Image[3] { leftImage, middleImage, rightImage };

        if (this.modo == Mode.DIEZ)
        {
            availableAnswers = new Answer[10] {
                new Answer("unapelotita", "1",  audioUno),
                new Answer("dospatitos", "2", audioDos),
                new Answer("trespanditas", "3", audioTres),
                new Answer("cuatromariposas", "4", audioCuatro),
                new Answer("cincoestrellitas", "5", audioCinco),
                new Answer("seisperritos", "6", audioSeis),
                new Answer("sietebuhos", "7", audioSiete),
                new Answer("ochoabejitas", "8", audioOcho),
                new Answer("nueveoreos", "9", audioNueve),
                new Answer("diezcandy", "10", audioDiez)
            };
        }
        else if (this.modo == Mode.VIENTE)
        {
            availableAnswers = new Answer[10] {
                new Answer("onceositos", "11",  audioUno),
                new Answer("docepeces", "12", audioDos),
                new Answer("trecesapitos", "13", audioTres),
                new Answer("catorcecerditos", "14", audioCuatro),
                new Answer("quincecarritos", "15", audioCinco),
                new Answer("dieciseisnaranjas", "16", audioSeis),
                new Answer("diecisietetortugas", "17", audioSiete),
                new Answer("dieciochofresas", "18", audioOcho),
                new Answer("diecinuevecupcake", "19", audioNueve),
                new Answer("veintecorazones", "20", audioDiez)
            };
        }
        

        this.Next();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int miliseconds = (int)(time * 1000) % 1000;
        timerString = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
    }

    public void Next()
    {
        List<Answer> answers = getRandomUnreapeatedAnswers();
        for (int i = 0; i < 3; i++)
        {
            images[i].sprite = Resources.Load<Sprite>(answers[i].image);
        }
        correctPosition = rnd.Next(3);
        text.text = answers[correctPosition].name;
        answers[correctPosition].audio.Play();
        time = 0;

        currentGameReference = FirebaseDatabase.DefaultInstance.RootReference.Child("games").Push();
        currentGameReference.Child("fecha").SetValueAsync(DateTime.Now.ToString());
        currentGameReference.Child("numero").SetValueAsync(answers[correctPosition].name);
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
        if (!this.isAudioPlaying)
        {
            if (correctPosition == 0)
                this.HandleCorrectAnswer();
            else
                this.HandleIncorrectAnswer();
        }        
    }

    public void OnMiddleClick()
    {
        if (!this.isAudioPlaying)
        {
            if (correctPosition == 1)
                this.HandleCorrectAnswer();
            else
                this.HandleIncorrectAnswer();
        }
    }

    public void OnRightClick()
    {
        if (!this.isAudioPlaying)
        {
            if (correctPosition == 2)
                this.HandleCorrectAnswer();
            else
                this.HandleIncorrectAnswer();
        }        
    }

    private void HandleCorrectAnswer()
    {
        this.isAudioPlaying = true;
        audioCorrecto.Play();
        StartCoroutine(DoNextAfterDelay(1.488f));

        DatabaseReference reference = currentGameReference.Child("intento").Push();
        reference.Child("tiempo").SetValueAsync(timerString);
        reference.Child("error").SetValueAsync(false);
    }

    private void HandleIncorrectAnswer()
    {
        this.audioFallo.Play();
        DatabaseReference reference =  currentGameReference.Child("intento").Push();
        reference.Child("tiempo").SetValueAsync(timerString);
        reference.Child("error").SetValueAsync(true);
    }

    IEnumerator DoNextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.Next();
        this.isAudioPlaying = false;
    }

    private class Answer
    {
        public string image;
        public string name;
        public AudioSource audio;

        public Answer(string image, string name, AudioSource audio)
        {
            this.image = image;
            this.name = name;
            this.audio = audio;
        }
    }
}