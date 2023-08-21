using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharsTogether : MonoBehaviour
{
    [Header("Animators Link")]
    [SerializeField] private Animator _aliceAnimator;
    [SerializeField] private Animator _borisAnimator;
    [SerializeField] private Animator _mainAnimator;
    [SerializeField] private string _hashAnim;
    public string HashAnim { get => _hashAnim; }
    public void PlayAnim(string animHashName)
    {
        SetTogetherScene();
        _mainAnimator.Play(animHashName);
    }
    private void SetTogetherScene()
    {
        _aliceAnimator.enabled = false;
        _borisAnimator.enabled = false;
        _mainAnimator.enabled = true;
    }
    public void StopPlay()
    {
        _mainAnimator.enabled = false;
        _aliceAnimator.enabled = true;
        _borisAnimator.enabled = true;
    }
}
