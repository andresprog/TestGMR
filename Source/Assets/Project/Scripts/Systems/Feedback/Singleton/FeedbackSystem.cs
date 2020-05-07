/**************************************************************************
 * Copyright: Copyright 2020 Cofradinn, LLC. All Rights reserved.
 * Module: Feedback
 * ScriptType: System
 * Created by: Andrés Romero, andresraulrg@gmail.com
 * Created on: jueves, 13 de febrero de 2020
 * Description: ...Add any description
 * Notes: ...Add any note
 **************************************************************************/

using Cofradinn.Modules.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cofradinn.Modules.Feedback
{
    /// <summary>
    /// The diferents feedback types
    /// </summary>
    public enum FeedbackType
    {
        None,
        Error,
        Warning,
        Success,

    }

    /// <summary>
    /// This system allows you to show notifications in real time on the screen
    /// </summary>
    public class FeedbackSystem : SingletonComponent<FeedbackSystem>
    {
        [Header("Components")]
        [SerializeField] private Text feedbackText;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _imgBackground;

        [Header("Parameters")]
        [SerializeField] private Color _startTextColor;
        [SerializeField] private Color _endTextColor;

        [Header("FeedbackColors")]
        [SerializeField] private Color _noneColor;
        [SerializeField] private Color _errorColor;
        [SerializeField] private Color _warningColor;
        [SerializeField] private Color _successColor;

        /// <summary>
        ///  This tags are used to change sections of the text to white. We write the text by changing its color,
        ///  not by adding letters. This allows the text to be aligned correctly with the panel beforehand and
        ///  avoids weird jumps while writing.
        /// </summary>
        private string START_COLOR_TAG;
        private string END_COLOR_TAG = "</color>";
        /// <summary>
        /// Is the feedback panel currently visible and the feedback coroutine currently active.
        /// </summary>
        private bool systemActive = false;
        /// <summary>
        /// We save the texts we have display in a queue to access them one at a time.
        /// </summary>
        private Queue<string> _currentDialogFeedbackQueue = new Queue<string>();
        private Queue<FeedbackData> _feedbacksdQueue = new Queue<FeedbackData>();

        /// <summary>
        /// Show notification
        /// </summary>
        /// <param name="dialog">Write here the notification message</param>
        /// <param name="feedbackType">Choose here the feedback type</param>
        /// <param name="waitTime">The life time of the notification</param>
        /// <param name="chartTime">Animation delay of each character</param>
        public void __SendFeedback(string dialog, FeedbackType feedbackType, float waitTime = 3f, float chartTime = 0.005f)
        {
            FeedbackData feedbackData = new FeedbackData
            {
                _dialog = dialog,
                _feedbackType = feedbackType,
                _waitTime = waitTime,
                _chartTime = chartTime
            };

            _feedbacksdQueue.Enqueue(feedbackData);
        }
        /// <summary>
        /// Show notification
        /// </summary>
        public void __SendFeedback(FeedbackData feedbackData)
        {
            _feedbacksdQueue.Enqueue(feedbackData);
        }
        /// <summary>
        /// Hide notification instantly
        /// </summary>
        public void __ForceClosedCurrentFeedback()
        {
            StopAllCoroutines();
            _animator.SetBool("FeedbackOpened", false);
            systemActive = false;
        }

        protected override void OnAwake()
        {
            feedbackText.color = _startTextColor;
        }
        private void Update()
        {
            if (_feedbacksdQueue.Count > 0)
                if (!systemActive)
                    StartCoroutine(___WriteFeedback());
        }
        private Color __GetFeedbackColor(FeedbackType feedbackType)
        {
            switch (feedbackType)
            {
                case FeedbackType.None: return _noneColor;
                case FeedbackType.Error: return _errorColor;
                case FeedbackType.Warning: return _warningColor;
                case FeedbackType.Success: return _successColor;
                default: return _noneColor;
            }
        }
        private IEnumerator ___WriteFeedback()
        {
            systemActive = true;
            FeedbackData feedbackData = _feedbacksdQueue.Dequeue();
            _currentDialogFeedbackQueue.Enqueue(feedbackData._dialog);
            _imgBackground.color = __GetFeedbackColor(feedbackData._feedbackType);
            START_COLOR_TAG = "<color=" + Converter.__HexConverter(_endTextColor) + ">";

            _animator.SetBool("FeedbackOpened", true);

            // As long as we have dialogs to display...
            while (_currentDialogFeedbackQueue.Count > 0)
            {
                // We divide the next dialog into paragraphs.
                string[] paragraphs = _currentDialogFeedbackQueue.Dequeue().Split('\n');

                // For each paragraph we write it letter by letter.
                foreach (string paragraph in paragraphs)
                {
                    // indexToStop = AudioSystem.Instance.PlayLooping(SfxType.Text);

                    char[] chars = paragraph.ToCharArray();
                    for (int i = 0; i <= chars.Length; i++)
                    {
                        string feedbackNow = START_COLOR_TAG + paragraph.Insert(i, END_COLOR_TAG);
                        feedbackText.text = feedbackNow;

                        yield return new WaitForSecondsRealtime(feedbackData._chartTime);
                    }

                    //if (indexToStop >= 0)
                    //{
                    //    //AudioSystem.Instance.StopLoop(indexToStop);
                    //    indexToStop = -1;
                    //}

                    // Then we wait X seconds to close the feedback.
                    yield return new WaitForSecondsRealtime(feedbackData._waitTime);
                }
            }

            _animator.SetBool("FeedbackOpened", false);

            systemActive = false;
        }

        public class FeedbackData
        {
            public string _dialog = "Empty";
            public FeedbackType _feedbackType = FeedbackType.Success;
            public float _waitTime = 3f;
            public float _chartTime = 0.005f;
        }
    }
}
