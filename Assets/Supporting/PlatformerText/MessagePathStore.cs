using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace Supporting.MessagePath
{
    public static class MessagePathStore
    {
        //FilePath
        #region Assets/Supporting/PlatformerText/Player/SceneEvent
        private static string _checkStore = "Assets/Supporting/PlatformerText/Player/SceneEvent/CheckPoint/StoreCheckIn.xml";
        private static string _storeIsCheck = "Assets/Supporting/PlatformerText/Player/SceneEvent/CheckPoint/StoreIsCheck.xml";
        private static string _playerFindStore = "Assets/Supporting/PlatformerText/Player/SceneEvent/CheckPoint/StoreIsFinish.xml";

        private static string _buttonActivate = "Assets/Supporting/PlatformerText/Player/SceneEvent/Button/ButtonActivate.xml";
        private static string _buttonActive = "Assets/Supporting/PlatformerText/Player/SceneEvent/Button/ButtonActive.xml";

        private static string _exitActivate = "Assets/Supporting/PlatformerText/Player/SceneEvent/ExitPoint/ExitActivate.xml";
        private static string _exitIsFalse = "Assets/Supporting/PlatformerText/Player/SceneEvent/ExitPoint/ExitIsFalse.xml";

        private static string _playerHurt = "Assets/Supporting/PlatformerText/Player/SceneEvent/Enemy/PlayerHurt.xml";
        private static string _playerLooseHp = "Assets/Supporting/PlatformerText/Player/SceneEvent/RestartLevel/PlayerLooseHP.xml";


        //private static string _time = "Assets/Supporting/PlatformerText/Player/SceneEvent/Time";




        public static string CheckStore { get => _checkStore; }
        public static string StoreIsCheck { get => _storeIsCheck; }
        public static string PlayerFindStore { get => _playerFindStore; }

        public static string ButtonActivate { get => _buttonActivate; }
        public static string ButtonActive { get => _buttonActive; }


        public static string ExitActivate { get => _exitActivate; }
        public static string ExitIsFalse { get => _exitIsFalse; }

        public static string PlayerHurt { get => _playerHurt; }
        public static string PlayerLooseHp { get => _playerLooseHp; }

        #endregion
        #region Assets/Supporting/PlatformerText/PlotClipText
        private static List<string> _plotClip = new List<string>();
        public static List<string> PlotClip { get => _plotClip; }
        public static void SetPlotClipStrings(string directoryPath)
        {
            _plotClip.Clear();
            List<string> tmp = Directory.GetFiles(directoryPath, "*.xml").ToList();
            foreach (var item in tmp)
            {
                _plotClip.Add(item);
            }
            foreach (var item in _plotClip)
            {
                Debug.Log(item.ToString());
            }
        }
        #endregion
    }
}

