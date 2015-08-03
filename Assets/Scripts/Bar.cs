using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Bar : MonoBehaviour {


    private Image barImage;

	// Use this for initialization
	void Start () {

        barImage = GetComponentInChildren<Image>();
	}
	
	public void Display(int currentHealth, int maxHealth) {
        float ratio = ((float)currentHealth) / ((float)maxHealth);


        Vector3 scale = barImage.rectTransform.localScale;
        scale.x = ratio;
        barImage.rectTransform.transform.localScale = scale;

    }
}
