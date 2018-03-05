﻿using System.Collections;
using UnityEngine;

namespace Reset_Scene_Transition
{
    public class Screenshot : MonoBehaviour {
    
        bool _HasTakennScreenShot;

        private void Start()
        {
            Invoke("TakeSnapShot", 2f);
        }

        public void TakeSnapShot()
        {
            if (!_HasTakennScreenShot)
            {
                StartCoroutine(ScreenShot());
                _HasTakennScreenShot = true;
            }
        }

        IEnumerator ScreenShot()
        {
            RenderTexture render = new RenderTexture(1920, 1080, 100);
            render.Create();
            yield return new WaitForEndOfFrame();
            Texture2D texture = new Texture2D (Screen.width, Screen.height, TextureFormat.ARGB32, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply ();

            ShatterController SController = Instantiate(Resources.Load<ShatterController>("Shattered Object"));
            SController.AnimateEvent(texture);
        }
    }
}
