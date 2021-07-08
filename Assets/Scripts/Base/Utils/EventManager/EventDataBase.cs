using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YX
{
    public abstract class EventDataBase
    {
        private float _timeStamp;

        public EventDataBase(float timeStamp = 0)
        {
            _timeStamp = timeStamp;
        }

        public abstract string GetEventType();
        public virtual float GetTimeStamp() { return _timeStamp; }
        public abstract EventDataBase Clone();
        public abstract string GetName();
    }
}
