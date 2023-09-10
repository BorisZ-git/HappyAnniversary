using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InteractibleObj
{
    public class Platform : MonoBehaviour
    {
        private MovingPlatform _controler;
        private void Awake()
        {
            _controler = GetComponentInParent<MovingPlatform>();
        }
        //Задать проверку на игрока под платформой, делать игрока не сталкиваемым с этим объектом
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Supporting.Utils.IsInLayer(collision.gameObject.layer, _controler.MovingObjMask))
            {
                collision.gameObject.transform.SetParent(gameObject.transform);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (Supporting.Utils.IsInLayer(collision.gameObject.layer, _controler.MovingObjMask))
            {
                collision.gameObject.transform.parent = null;
            }
        }
    }
}

