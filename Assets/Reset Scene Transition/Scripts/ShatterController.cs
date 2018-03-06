using UnityEngine;

namespace Reset_Scene_Transition
{
    public class ShatterController : MonoBehaviour 
    {
        public Material ScreenshotMaterial;

        ShatteredGlass[] _Childrens;

        bool _Init;

        [SerializeField]
        Light _PointLight;
        [SerializeField]
        Transform GlassTrigger;

        [SerializeField]
        protected ParticleSystem meshParticleSystem;

        [SerializeField]
        protected Transform meshParent;

        // Use this for initialization
        void Start () 
        {
            // some grossness

            foreach (Transform trans in meshParent)
            {
                Instantiate(meshParticleSystem, trans);
            }
            
            Init();
        
        }

        private void OnEnable()
        {
            Init();
            for (int i = 0; i < _Childrens.Length; i++)
            {
                _Childrens[i].gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            Reset();
        }

        void Init()
        {
            if (!_Init)
            {
                _Childrens = GetComponentsInChildren<ShatteredGlass>();
            }
        }

        public void Reset()
        {
            for(int i =0; i< _Childrens.Length;i++)
            {
                _Childrens[i].transform.eulerAngles = Vector3.zero;
            }
        }

        public void AnimateEvent(Texture2D t)
        {
            ScreenshotMaterial.mainTexture = t;
            for (int i = 0; i < _Childrens.Length; i++)
            {
                _Childrens[i].gameObject.SetActive(true);
                _Childrens[i].StartAnimate();
            }

            _PointLight.intensity = 10;
            //_PointLight.DOIntensity(0, 1f).OnComplete(StartGlassMove);
        }

        void StartGlassMove()
        {
            for (int i = 0; i < _Childrens.Length; i++)
            {
                _Childrens[i].StartCountForce();
            }
        }

    }
}
