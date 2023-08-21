using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
namespace Bonus
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(AudioSource))]
    abstract public class BonusItem : MonoBehaviour
    {
        [Header("Layers")]
        [SerializeField] private LayerMask _player;
        private AudioSource _audioSource;
        private SpriteRenderer _spriteRender;
        private Collider2D _collider;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRender = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _player))
            {
                Taked(collision.gameObject);
            }
        }
        virtual protected void Taked(GameObject player)
        {
            _audioSource.Play();
            _spriteRender.enabled = false;
            _collider.enabled = false;
            StartCoroutine(WaitAudio());
            //Destroy(gameObject);
        } 
        IEnumerator WaitAudio()
        {
            yield return new WaitForSeconds(.6f);
            _collider.enabled = true;
            _spriteRender.enabled = true;
            gameObject.SetActive(false);
        }
    }
}


