using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingAnimation : MonoBehaviour
{
    public TextMeshProUGUI textDisplay_tmp;

    public float moveSpeed_f;
    public float alphaValue_f;

    public void Constructor(string _value)
    {
        textDisplay_tmp.text = _value;
    }

    private void Start()
    {
        textDisplay_tmp = GetComponent<TextMeshProUGUI>();
        this.name = "Floating Animation";
    }

    private void Update()
    {
        if (alphaValue_f <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.transform.Translate(new Vector2(0, moveSpeed_f) * Time.deltaTime);
            alphaValue_f -= Time.deltaTime;
            textDisplay_tmp.color = new Color(255f, 255f, 255f, alphaValue_f);
        }
    }
}
