using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Platformer.MessageEditor;

namespace Platformer.Editor
{
    public class XmlMessageEditor : EditorWindow
    {
        private string _messageName = "";
        private string _fileName = "MessageData";
        private string _filePath = "Assets/Supporting/PlatformerText/Player/SceneEvent";
        private MessageData _messageData = new MessageData();
        private string _messageText = "";
        private float _readTime = 0;
        private string _readTimeStr = "0";
        private bool _isPrint = true;
        private bool _isLeftSpeakerSide = true;

        #region MessageEditor

        private int _isSelectInt = 0;
        private int _selectionGridInt = 0;
        private List<string> _listMessagesName = new List<string>();

        #endregion

        #region MessageDatasInspector

        private List<MessageData> _messageDatas = new List<MessageData>();
        private List<string> _listFileNames = new List<string>();
        private string _directoryPath = "";
        private int _selectionGridDatasInt = 0;

        #endregion

        [MenuItem("Platformer.Editor/Xml Message Editor %t")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(XmlMessageEditor));
        }

        private void OnGUI()
        {
            #region MessageEditor
            
            GUI.Label(new Rect(300,0,200,25), "Message Property:", EditorStyles.largeLabel);
            GUILayout.BeginArea(new Rect(0, 25, 500, 275));
            GUILayout.Label("Message Name:", EditorStyles.whiteMiniLabel);
            _messageName = GUILayout.TextField(_messageName, 25);
            GUILayout.Label("Message Text:", EditorStyles.whiteMiniLabel);
            _messageText = GUILayout.TextArea(_messageText, 1000);
            GUILayout.Label("Type of print:", EditorStyles.whiteMiniLabel);
            _isPrint = GUILayout.Toggle(_isPrint, "False:Instance True:Typing");
            GUILayout.Label("Side of Speaker:", EditorStyles.whiteMiniLabel);
            _isLeftSpeakerSide = GUILayout.Toggle(_isLeftSpeakerSide, "False:Right True:Left");
            GUILayout.Label("How long showing", EditorStyles.whiteMiniLabel);
            bool success = float.TryParse(GUILayout.TextField(_readTimeStr, 5), out _readTime);
            if (success)
            {
                _readTimeStr = _readTime.ToString();
            }
            else
            {
                GUILayout.Label("Error: Only float type in How Long Showing, your property will not be writted",EditorStyles.whiteMiniLabel);
            }
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(510, 25, 250, 250));
            GUILayout.Label("List of Message",EditorStyles.whiteMiniLabel);
            _selectionGridInt = GUI.SelectionGrid(new Rect(0, 25, 250, 225), _selectionGridInt, _listMessagesName.ToArray(), 1);
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(0, 330, 750, 200));
            if (GUILayout.Button("AddNewMessage"))
            {
                _messageData.messages.Add(AddMessage());
                GetMessages();
                UpdateMessageEditorView();
            }
            EditorGUI.BeginDisabledGroup(_messageData.messages.Count == 0);
            
            if (GUILayout.Button("Set Change To Selected Message"))
            {
                SetChangeToMessage();
            }
            if (GUILayout.Button("RemoveMessage"))
            {
                _messageData.messages.RemoveAt(_selectionGridInt);
                GetMessages();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndArea();
            GUILayout.BeginArea(new Rect(0, 410, 750, 150));
            GUI.Label(new Rect(300, 0, 200, 25), "MessageData Property:");
            GUILayout.Space(15);
            GUILayout.Label("File Name:", EditorStyles.whiteMiniLabel);
            _fileName = GUILayout.TextField(_fileName, 25);
            GUILayout.Label("File Path:", EditorStyles.whiteMiniLabel);
            _filePath = GUILayout.TextField(_filePath, 100);
            GUILayout.Space(10);
            EditorGUI.BeginDisabledGroup(_messageData.messages.Count == 0);
            if(GUILayout.Button("Serialize messages in MessageData File"))
            {
                if (!string.IsNullOrEmpty(_fileName) && !string.IsNullOrEmpty(_filePath))
                {
                    _messageData.SetValue(_fileName, _filePath);
                    WriteLoadText.SerializeFile(_messageData, _messageData.path);
                }
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndArea();
            #endregion

            #region MessageDatasInspector
            GUILayout.BeginArea(new Rect(0, 560, 750, 300));
            GUI.Label(new Rect(300, 0, 200, 25), "MessageDatas Inspector:");
            GUILayout.Space(15);
            GUILayout.Label("Directory Path:", EditorStyles.whiteMiniLabel);
            _directoryPath = GUILayout.TextField(_directoryPath, 290);
            GUILayout.Space(15);
            EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(_directoryPath));
            if(GUILayout.Button("Search Datas in Directory"))
            {
                _messageDatas = WriteLoadText.GetDataFromDirectory(_directoryPath);
                GetDatas();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUI.BeginDisabledGroup(_listFileNames.Count == 0);

            if (GUILayout.Button("Get Selected Data"))
            {
                _messageData = _messageDatas[_selectionGridDatasInt];
                _fileName = _messageData.fileName;
                _isSelectInt = 0;
                GetMessages();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.Label("Directory Inspector:", EditorStyles.whiteMiniLabel);
            _selectionGridDatasInt = GUI.SelectionGrid(new Rect(0, 135, 750, 130), _selectionGridDatasInt, _listFileNames.ToArray(), 1);

            GUILayout.EndArea();
            #endregion


            if (_isSelectInt != _selectionGridInt)
            {
                if(_isSelectInt > _messageData.messages.Count)
                {
                    _selectionGridInt = 0;
                }
                UpdateMessageEditorView();
            }
        }
        private void GetMessages()
        {
            _listMessagesName.Clear();
            foreach (var item in _messageData.messages)
            {
                _listMessagesName.Add(item.MessageName);
            }
            if(!string.IsNullOrEmpty(_messageData.filePath))
            {
                _filePath = _messageData.filePath;
            }
        }
        private void GetDatas()
        {
            _selectionGridInt = 0;
            _listFileNames.Clear();
            if(_messageDatas.Count > 0)
            {
                foreach (var item in _messageDatas)
                {

                    _listFileNames.Add(item.fileName);
                }
            }
        }
        private Message AddMessage() => new Message("MessageName", "Some message text", true, 5, true);
        private void SetChangeToMessage()
        {
            _messageData.messages[_selectionGridInt].SetValue(_messageName, _messageText, _isPrint, _readTime, _isLeftSpeakerSide);
            GetMessages();
        }
        private void UpdateMessageEditorView()
        {
            if(_selectionGridInt <= _messageData.messages.Count)
            {
                _messageName = _messageData.messages[_selectionGridInt].MessageName;
                _messageText = _messageData.messages[_selectionGridInt].messageText;
                _readTime = _messageData.messages[_selectionGridInt].readTime;
                _readTimeStr = _readTime.ToString();
                _isPrint = _messageData.messages[_selectionGridInt].isPrint;
                _isLeftSpeakerSide = _messageData.messages[_selectionGridInt].isLeftSideSpeaker;
                _isSelectInt = _selectionGridInt;
            }
        }
    }
}



