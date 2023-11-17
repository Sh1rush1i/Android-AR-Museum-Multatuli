using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    public enum CRecognitionInterval { None, Once, Repeat }
    public enum CRecognitionBehavior { None, ClearAfterRecognized }

    [Header("Main Settings")]
    public CRecognitionInterval RecognitionInterval = CRecognitionInterval.Repeat;
    public CRecognitionBehavior RecognitionBehavior = CRecognitionBehavior.ClearAfterRecognized;
    public ConfidenceLevel Confidence = ConfidenceLevel.Low;

    [System.Serializable]
    public class CEventSpeech
    {
        [Header("Speech List")]
        public List<string> SpeechRecognized;
        [Header("Speech Event")]
        public UnityEvent SpeechEvent;
    }

    [Header("Speech Recognition Settings")]
    public List<CEventSpeech> Keywords;
    protected PhraseRecognizer recognizer;
    [HideInInspector]
    public string[] dictionary;

    [Header("Variable Settings")]
    public string TargetString;

    // Start is called before the first frame update
    void Start()
    {
        int dictionary_length = 0;
        for (int i = 0; i < Keywords.Count; i++)
        {
            dictionary_length += Keywords[i].SpeechRecognized.Count;
        }

        dictionary = new string[dictionary_length];
        int k = 0;
        for (int i = 0; i < Keywords.Count; i++)
        {
            for (int j = 0; j < Keywords[i].SpeechRecognized.Count; j++)
            {
                dictionary[k] = Keywords[i].SpeechRecognized[j];
                k++;
            }
        }

        /*dictionary = new string[Keywords.Length];
        for (int i = 0; i < Keywords.Length; i++)
        { 
            dictionary[i] = Keywords[i];
        }
        */

        recognizer = new KeywordRecognizer(dictionary, Confidence);
        recognizer.OnPhraseRecognized += Recognizer_OnPhraseRecognized;
        recognizer.Start();
    }

    void FixedUpdate()
    {
        if (this.gameObject.activeSelf)
        {
            foreach (CEventSpeech temp in Keywords)
            {
                foreach (string speechcompare in temp.SpeechRecognized)
                {
                    if (speechcompare == TargetString)
                    {
                        if (RecognitionInterval == CRecognitionInterval.Once)
                        {
                            RecognitionInterval = CRecognitionInterval.None;
                            temp.SpeechEvent.Invoke();
                        }
                        else if (RecognitionInterval == CRecognitionInterval.Repeat)
                        {
                            temp.SpeechEvent.Invoke();
                        }
                        if (RecognitionBehavior == CRecognitionBehavior.ClearAfterRecognized)
                        {
                            TargetString = "";
                        }
                    }
                }
            }
        }
    }

    private void Recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (this.gameObject.activeSelf)
        {
            TargetString = args.text;

            foreach (CEventSpeech temp in Keywords)
            {
                foreach (string speechcompare in temp.SpeechRecognized)
                {
                    if (speechcompare == TargetString)
                    {
                        if (RecognitionInterval == CRecognitionInterval.Once)
                        {
                            RecognitionInterval = CRecognitionInterval.None;
                            temp.SpeechEvent.Invoke();
                        }
                        else if (RecognitionInterval == CRecognitionInterval.Repeat)
                        {
                            temp.SpeechEvent.Invoke();
                        }
                        if (RecognitionBehavior == CRecognitionBehavior.ClearAfterRecognized)
                        {
                            TargetString = "";
                        }
                    }
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= Recognizer_OnPhraseRecognized;
            recognizer.Stop();
        }
    }
}

