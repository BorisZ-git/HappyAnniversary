using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class CheckPointCounter : MonoBehaviour
    {
        private int _checkPointCount;
        private List<Checkpoints.Store> _checkPointList;
        private void Start()
        {
            _checkPointList = new List<Checkpoints.Store>();
            _checkPointList.AddRange(FindObjectsOfType<Checkpoints.Store>());
            for (int i = 0; i < _checkPointList.Count; i++)
            {
                _checkPointCount++;
            }
        }
    }
}

