using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. ������ ������ ������� ������, ������� ����� ��������� ����� � �� ��� ���������� �������� ������
namespace GameControl
{
    /// <summary>
    /// This class open scene and start run game logic.
    /// Realize singltone pattern in base class
    /// </summary>
    public class GameController : AbstractGameController
    {
        // Restruct Singltone
        // Need global links for object like player - LinksHash = done;
        // Need check for exclusive object
        // Need create prefab who will contain script FillScene and instantiate object - (Filler)
        // Need channel between Filler and objects that responsible for spawnPoints
        public override void Awake()
        {
            base.Awake();
            /*
             * FillScene.StartScene() = ���� ����� ����� ��������� ������� �������, ������� ��������� ����� �������� � ��������� �� ����������� ���������;
             * CheckPlayerExist() = ��������� ���������� �� �����
             * FillScene.SetPlayer(Player) = �������� ������ ������ � ����� ���������� ��� �� ������ �����;
             * protected SpawnPoint();
             */
        }
        /// <summary>
        /// On start call for another class method that fills scene with enemy player and other objects
        /// </summary>
        private void FillScene()
        {
            Instantiate(_prefabsHash.Enemy);
        }
    }
}

