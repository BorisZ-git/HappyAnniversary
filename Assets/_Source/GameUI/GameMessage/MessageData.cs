using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Xml;

namespace Platformer.MessageEditor
{
    [Serializable]
    public class MessageData
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string path { get; set; }
        public List<Message> messages { get; set; } = new List<Message>();
        public MessageData() { }
        public void SetValue(string FileName,string FilePath)
        {
            fileName = FileName;
            filePath = FilePath;
            path = filePath + "/" + fileName;
            if (!path.Contains("*.xml"))
            {
                path += ".xml";
            }
        }        
    }
    [Serializable]
    public class Message
    {
        private string _messageName;
        private string _messageText;
        private bool _isPrint;
        private float _readTime;
        private bool _isLeftSideSpeaker;
        public string messageText { get => _messageText; set => _messageText = value; }
        [XmlIgnore]
        public bool isPrint { get => _isPrint; set => _isPrint = value; }
        [XmlIgnore]
        public bool isLeftSideSpeaker { get => _isLeftSideSpeaker; set => _isLeftSideSpeaker = value; }

        [XmlElement("isPrint")]
        public string isPrintSerialize
        {
            get { return this.isPrint ? "1" : "0"; }
            set { this.isPrint = XmlConvert.ToBoolean(value); }
        }
        [XmlElement("isLeftSideSpeaker")]
        public string isLeftSideSpeakerSerialize
        {
            get { return this.isLeftSideSpeaker ? "1" : "0"; }
            set { this.isLeftSideSpeaker = XmlConvert.ToBoolean(value); }
        }
        public float readTime { get => _readTime; set => _readTime = value ; }
        public string MessageName { get => _messageName; set => _messageName = value; }

        public Message() { }
        public Message(string messageName, string messageText, bool isPrint, float readTime, bool isLeftSpeaker)
        {
            SetValue(messageName, messageText, isPrint, readTime, isLeftSpeaker);
        }
        public void SetValue(string messageName,string messageText, bool isPrint, float readTime, bool isLeftSpeaker)
        {
            _messageName = messageName;
            _messageText = messageText;
            _isPrint = isPrint;
            _readTime = readTime;
            _isLeftSideSpeaker = isLeftSpeaker;
        }
        public override string ToString()
        {
            string tmp = $"Name: {_messageName}, {Environment.NewLine}" +
                $"Text: {messageText} {Environment.NewLine}" +
                $"Print: {isPrint} , Speaker: {_isLeftSideSpeaker} {Environment.NewLine}" +
                $"ReadTime: {_readTime} {Environment.NewLine}";
            return tmp;
        }
    }
}

