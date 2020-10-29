using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneTimer : MonoBehaviour
{
    bool Dot;
    [SerializeField] Text dot;
    [SerializeField] Text hours;
    [SerializeField] Text minutes;
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        hours.text = System.DateTime.Now.ToShortTimeString().Split(':')[0].Length==1?("0"+ System.DateTime.Now.ToShortTimeString().Split(':')[0]) : System.DateTime.Now.ToShortTimeString().Split(':')[0];
        minutes.text = System.DateTime.Now.ToShortTimeString().Split(':')[1].Length == 1 ? ("0" + System.DateTime.Now.ToShortTimeString().Split(':')[1]) : System.DateTime.Now.ToShortTimeString().Split(':')[1];
        dot.text = (Dot ? ":" : " ");
    }
    IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            Dot = !Dot;
        }
    }

}
