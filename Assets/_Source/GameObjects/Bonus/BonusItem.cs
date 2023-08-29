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
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
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
            player.GetComponent<AudioSource>().PlayOneShot(_audioSource.clip);
            gameObject.SetActive(false);
        }
    }
}


