using Flusk.DataHelp;
using Flusk.Extensions;
using Flusk.Structures;
using UnityEngine;

namespace Flusk.Effect
{
    public class Pulse : MonoBehaviour
    {       
        [SerializeField]
        protected Range range;

        [SerializeField]
        protected AnimationCurve curve;

        [SerializeField]
        protected float speed = 1;

        private float curveTime, curveMaxTime;
        private float minCurve, maxCurve;
        
        protected virtual void Update()
        {
            curveTime += (Time.deltaTime * speed);
            curveTime %= curveMaxTime;
            float currentPoint = curve.Evaluate(curveTime);
            float remap = currentPoint.Map(minCurve, maxCurve, range.Min, range.Max);
            SetScale(remap);
        }
           
        protected virtual void Awake()
        {
            curveMaxTime = curve.GetFinalTime();
            minCurve = curve.GetMin();
            maxCurve = curve.GetMax();
        }


        private void SetScale(float number)
        {
            transform.localScale = Vector3.one * number;
        }
    }
}