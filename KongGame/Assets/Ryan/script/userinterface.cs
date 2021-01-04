using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UserInterface : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay_tmp;

    public GameObject timer_go;
    public GameObject comboAnim_go;

    //  DEBUGGING.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FloatingAnimation("x1", new Vector2(Screen.width / 2, Screen.height / 2));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Score(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Timer(120.0f, new Vector2(Screen.width / 2, Screen.height / 2 - 200));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            try
            {
                GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().Pause();
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e);
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            try
            {
                GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().Resume();
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e);
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            try
            {
                GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().Delete();
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e);
            }
        }
    }
    //  DEBUGGING.

    public void Score(int _score)
    {
        scoreDisplay_tmp.text = "Score: " + _score.ToString();
    }

    public void Timer(float _time, Vector2 _pos)
    {
        Timer item = timer_go.GetComponent<Timer>();
        item.Constructor(_time);

        Vector2 timerDimensions = new Vector2(
            item.GetComponent<RectTransform>().rect.width,
            item.GetComponent<RectTransform>().rect.height
            );

        if (ValidPosition(_pos, timerDimensions))
        {
            Instantiate(item, _pos, Quaternion.identity, this.transform);    
        }
    }

    public void FloatingAnimation(string _text, Vector2 _pos)
    {
        FloatingAnimation item = comboAnim_go.GetComponent<FloatingAnimation>();
        item.Constructor( _text);
        
        Vector2 floatingAnimationDimensions = new Vector2(
            item.GetComponent<RectTransform>().rect.width,
            item.GetComponent<RectTransform>().rect.height
            );

        if (ValidPosition(_pos, floatingAnimationDimensions))
        {
            Instantiate(item, _pos, Quaternion.identity, this.transform);
        }
    }

    private bool ValidPosition(Vector2 _pos, Vector2 _dimensions)
    {
        if ((_pos.x > Screen.width - _dimensions.x/2 || _pos.x < _dimensions.x/2) ||
            (_pos.y > Screen.height - _dimensions.y/2 || _pos.y < _dimensions.y/2))
        {
            Debug.Log("This is an invalid positon!");
            return false;
        }
        return true;
    }
}