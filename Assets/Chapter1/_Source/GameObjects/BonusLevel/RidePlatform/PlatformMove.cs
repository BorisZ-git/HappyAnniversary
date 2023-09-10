using UnityEngine;
namespace BonusLevel.RidePlatform
{
    public class PlatformMove
    {
        private Transform _transform;
        private LevelBorders _levelBorders;
        private BlockZones _block;

        public PlatformMove(Transform transform, float x, float y)
        {
            _transform = transform;
            _levelBorders = new LevelBorders(-x,x,y,-y);
        }
        public PlatformMove(Transform transform, float left, float right, float top, float bottom, BlockZones block)
        {
            _transform = transform;
            _levelBorders = new LevelBorders(left, right, top, bottom);
            _block = block;
        }
        public void Move(Vector2 vector)
        {
            _transform.Translate(vector);
            if (_transform.position.x > _levelBorders.borderRight || _transform.position.x < _levelBorders.borderLeft
                || _transform.position.y > _levelBorders.borderUp || _transform.position.y < _levelBorders.borderDown)
            {
                _transform.Translate(-vector);
            }
            if (_block)
            {
                foreach (var item in _block._zones)
                {
                    if(_transform.position.x < item.AreaX[1] && _transform.position.x > item.AreaX[0] && _transform.position.y < item.AreaY[1] && _transform.position.y > item.AreaY[0] ||                        
                        _transform.position.y < item.AreaY[1] && _transform.position.y > item.AreaY[0] && _transform.position.x < item.AreaX[0] && _transform.position.x > item.AreaX[1])
                    {
                        _transform.Translate(-vector);
                    }
                }
            }
        }
        public void MoveTo(float speed, float x,float y)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, new Vector2(x, y), speed);
        }
        public void OffsetBorders(float offsetLeft, float offsetRight)
        {
            _levelBorders.borderLeft = offsetLeft;
            _levelBorders.borderRight = offsetRight;
        }
    }
    public struct LevelBorders
    {
        public float borderUp;
        public float borderDown;
        public float borderLeft;
        public float borderRight;
        public LevelBorders(float xLeft, float xRight, float yUp, float yDown )
        {
            borderUp = yUp;
            borderDown = yDown;
            borderLeft = xLeft;
            borderRight = xRight;
        }
    }
}

