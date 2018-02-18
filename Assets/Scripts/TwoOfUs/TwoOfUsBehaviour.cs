using UnityEngine;

namespace TwoOfUs
{
    public class TwoOfUsBehaviour : MonoBehaviour
    {
        public virtual void Deactivate()
        {
            SetActive(false);
        }

        public virtual void Activate()
        {
            SetActive(true);
        }

        public virtual void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public static void Destroy(TwoOfUsBehaviour behaviour)
        {
            behaviour.Destroy();
        }
        
        public static void Destroy(GameObject behaviour)
        {
            TwoOfUsBehaviour b = behaviour.GetComponent<TwoOfUsBehaviour>();
            if (b == null)
            {
                return;
            }
            b.Destroy();
        }

        public T GetOrAddComponent<T>() where T : Component
        {
            T component = GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}