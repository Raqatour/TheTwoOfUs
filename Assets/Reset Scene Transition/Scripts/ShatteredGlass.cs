using Flusk.Extensions;
using Flusk.Utility;
using UnityEngine;

namespace Reset_Scene_Transition
{
    public class ShatteredGlass : MonoBehaviour 
    {
        public float StartTime;
        Rigidbody _Rb;
        ConstantForce _Force;

        private float time = 1.3f;
        private Timer timer;
        private Vector3 originalScale;

        private void Start()
        {
            _Rb = GetComponent<Rigidbody>();
            _Force = GetComponent<ConstantForce>();
            originalScale = transform.localScale;
        }

        protected virtual void Update()
        {
            if (timer != null)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void StartAnimate()
        {
            timer = new Timer(time, TimerComplete);
            timer.Update = AnimateDown;
        }

        private void AnimateDown(float currentTime)
        {
            float ratio = (time - currentTime) / time;
            Vector3 scale = originalScale * ratio;
            transform.localScale = scale;
        }

        private void TimerComplete()
        {
            timer = null;
        }

        public void StartCountForce()
        {
            float a = 0, b = 0;
            //DOTween.To(() => a, x => a = x, 12, 5).OnUpdate(()=> _Force.force = new Vector3(a, _Force.force.y, _Force.force.z));
            //DOTween.To(() => b, x => b = x, -7, 3).OnUpdate(() => _Force.force = new Vector3(_Force.force.x, _Force.force.y, b));
            _Force.torque = new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1));
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "GlassTrigger")
                _Rb.isKinematic = false;
        }

    }
}
