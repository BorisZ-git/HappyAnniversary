using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class FireWork : MonoBehaviour
{
    [Header("Anim Logic")]
    [SerializeField] private bool _isRepeatable;
    [SerializeField] private float _delayTime;
    [SerializeField] private string _hashName;
    private Animator _anim;
    private float _count;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if(_anim != null && !string.IsNullOrEmpty(_hashName))
        {
            _anim.Play(_hashName);
            if (!_isRepeatable)
            {
                StartCoroutine(DisableObj());
            }
        }
    }
    private void Update()
    {
        if (_isRepeatable)
        {
            _count += Time.deltaTime;
            if(_count > _delayTime)
            {
                _anim.Play(_hashName);
                _count = 0;
            }
        }
    }
    IEnumerator DisableObj()
    {
        yield return new WaitForSeconds(_delayTime);
        gameObject.SetActive(false);
    }
}
