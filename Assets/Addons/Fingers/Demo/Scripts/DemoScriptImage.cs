//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DigitalRubyShared
{
   
    public class DemoScriptImage : MonoBehaviour
    {
        public static bool TypeBeginCast;
        public FingersImageGestureHelperComponentScript ImageScript;
        public ParticleSystem MatchParticleSystem;
        public AudioSource AudioSourceOnMatch;

        private void LinesUpdated(object sender, System.EventArgs args)
        {
            Managers.fightmanager.GestureNumer++;
          // Debug.LogFormat("Lines updated, new point: {0},{1}", ImageScript.Gesture.FocusX, ImageScript.Gesture.FocusY);
        }

        private void LinesCleared(object sender, System.EventArgs args)
        {
            Debug.LogFormat("Lines cleared!");
        }

        private void Start()
        {
            ImageScript.LinesUpdated += LinesUpdated;
            ImageScript.LinesCleared += LinesCleared;
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            
                ImageScript.Reset();
            }
            else if (TypeBeginCast)
            {
                
                    TypeBeginCast = false;
                if (Managers.fightmanager.GestureNumer > 4)
                {
                    ImageGestureImage match = ImageScript.CheckForImageMatch();
                    if (match != null)
                    {
                        if (Managers.fightmanager != null)
                            Managers.fightmanager.speelcast += match.Name;
                        ImageScript.Reset();

                    }
                    else
                    {
                        Debug.Log("Количество точек:" +Managers.fightmanager.GestureNumer);
                        ImageScript.Reset();
                        GameObject.FindGameObjectWithTag("WrongSpell").GetComponent<Animation>().Play();
                        Debug.Log("Ошибка ввода");
                        
                    }
                }
                else
                {
                    ImageScript.Reset();
                    Debug.Log("Мало точек");
                }
                

                // TODO: Do something with the match
                // You could get a texture from it:
                // Texture2D texture = FingersImageAutomationScript.CreateTextureFromImageGestureImage(match);
            }
        }
    }
}
