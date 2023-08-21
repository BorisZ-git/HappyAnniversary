using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Supporting.MessagePath;
using Platformer.MessageEventUI;
using GameUI;

namespace Platformer.Plot
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MessageLvlController))]
    [RequireComponent(typeof(TalkerSpritesData))]
    public class ClipManager : MonoBehaviour
    {
        [Header("Numeric Values")]
        [SerializeField] private float _timeToFinishClip;
        [Header("Animator Hash")]
        [SerializeField] private string _meleeAttackHash;
        [SerializeField] private string _distanceAttackHash;
        [SerializeField] private string _walkHash;
        [SerializeField] private string _backHash;
        [SerializeField] private string _danceHash;
        [SerializeField] private string _clipNameHash;
        [SerializeField] private int _clipId;
        [Header("Links")]
        [SerializeField] private MessageLvlController _msgLvlController;
        [SerializeField] private LevelEffectsManager _levelEffectsManager;
        [SerializeField] private Animator _clipAnimator;
        [SerializeField] private GameObject _alice;
        [SerializeField] private GameObject _boris;
        [SerializeField] private GameObject _cameraMoveTo;
        [SerializeField] private TimerCount _timer;
        [SerializeField] private Enemy.EnemyAnimation[] _enemies;
        [SerializeField] private CharsTogether _charsTogether;
        [Header("Directory Path Hash")]
        [SerializeField] private string _dialogPath;
        [SerializeField] private string _anotherMsgPath;
        [SerializeField] private string _anotherMsgNameHash;

        private Animator _aliceAnimator;
        private Animator _borisAnimator;
        private TalkerSpritesData _spritesData;
        private bool _nextClipLoaded;
        //EnterVilla
        private void Awake()
        {
            _spritesData = GetComponent<TalkerSpritesData>();
            _aliceAnimator = _alice.GetComponent<Animator>();
            _borisAnimator = _boris.GetComponent<Animator>();
            MessagePathStore.SetPlotClipStrings(_dialogPath);
            _clipAnimator.Play(_clipNameHash + _clipId);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_nextClipLoaded)
            {
                FinishClip();
            }
        }
        public void TogetherAnimPlay()
        {
            _charsTogether.PlayAnim(_charsTogether.HashAnim);
        }
        public void SetAliceAttack1()
        {
            _aliceAnimator.Play(_meleeAttackHash);
        }
        public void SetAliceAttack2()
        {
            _aliceAnimator.Play(_distanceAttackHash);
        }
        public void SetAliceDance(float value)
        {
            _aliceAnimator.SetBool(_danceHash, value > 0 ? true : false);
        }
        public void SetAliceWalk(float value)
        {
            _aliceAnimator.SetFloat(_walkHash, value);
        }
        public void SetBorisWalk(float value)
        {
            _borisAnimator.SetFloat(_walkHash, value);
        }
        public void SetAliceBack(int value)
        {
            _aliceAnimator.SetBool(_backHash, value > 0 ? true : false);
        }
        public void SetBorisBack(int value)
        {
            _borisAnimator.SetBool(_backHash, value > 0 ? true : false);
        }
        public void SetTimer(float value)
        {
            _timer.ActivateTimer(value);
            _timer.IsLevelStarted = true;
        }
        public void SetEnemyDead(int ind)
        {
            _enemies[ind].Dead(true);
        }
        public void SetDialog(int index)
        {
            _msgLvlController.TakePlotMessageData(MessagePathStore.PlotClip[index]);
            _msgLvlController.leftTalker = _spritesData.TalkerAlice;
            _msgLvlController.rightTalker = _spritesData.TalkerBoris;
            _msgLvlController.ShowPlotDialog();
        }
        public void SetAnotherMessage(int idElement)
        {
            _msgLvlController.ShowMessage(_spritesData.AnotherTalker, _anotherMsgPath + "/" + _anotherMsgNameHash, idElement);
        }
        /// <summary>
        /// FadeIn Screen and Load Next Scene
        /// </summary>
        public void FinishClip()
        {
            _levelEffectsManager.FadeINScreen(_timeToFinishClip);
            _nextClipLoaded = true;
            StartCoroutine(LoadNextScene());
        }
        IEnumerator LoadNextScene()
        {
            yield return new WaitForSeconds(_timeToFinishClip);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}


