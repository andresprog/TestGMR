using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cettic.Utilities
{
    public class QuestionJsonGenerator : MonoBehaviour
    {
        [Header("Commands")]
        public bool _generateJson;
        [Header("File")]
        public string _fileName;
        [Header("Questions")]
        public JsonQuestions _jsonQuestions;

        private void Awake()
        {
            _generateJson = false;
        }
        private void Update()
        {
            if (_generateJson)
            {
                _generateJson = false;
                string jsonData = JsonUtility.ToJson(_jsonQuestions, true);
                DateTime t = System.DateTime.Now;
                string time = t.Year.ToString() + t.Month.ToString() + t.Day.ToString() + t.Hour.ToString() + t.Minute.ToString() + t.Second.ToString();
                string fileName = time + "_" + _fileName + ".json";
                string path = Path.Combine(Application.dataPath, fileName);
                File.WriteAllText(path, jsonData);

                Debug.Log("json file ready: ", this);
                Debug.Log("path: " + path);
            }
        }
    }

    [System.Serializable]
    public class JsonQuestions
    {
        /// <summary>
        /// Warning: Cettic.Utilities.TriviaQuestion class is not equal to TriviaQuestion class
        /// </summary>
        public List<TriviaQuestion> _questions;
    }

    [System.Serializable]
    public class TriviaQuestion
    {
        public int sodimacId;
        public string question;
        public QuestionType questionType;
        public string imagePath;
        [NonSerialized] public Sprite sprite;
        public string errorKey;
        public TriviaAnswer[] answers = new TriviaAnswer[4];
        public bool IsReady => questionType == QuestionType.Normal || sprite != null;
    }
    [System.Serializable]
    public class TriviaAnswer
    {
        public string answer;
        public bool correct;
        [NonSerialized] public Sprite sprite;
        public string spritePath;
    }

    public enum QuestionType
    {
        Normal = 0,
        Cellphone = 1,
        Image = 2,
        AnswersWihtImages = 3
    }
}