using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgStatSlider : MonoBehaviour
{
    public Image image;
    public float currentValue;
    // Start is called before the first frame update
    public void SetSlider(float value){
        //Debug.Log("SetSlider with "+value);
        currentValue = value;
        image.fillAmount = currentValue;
    }

    public void AddSlider(float value){
        //Debug.Log("AddSlider with "+value+" | current: "+currentValue);
        if((currentValue + value) <= 1){currentValue = currentValue + value;}
        else{currentValue = 1;}
        image.fillAmount = currentValue;
    }

    public void DecSlider(float value){
        //Debug.Log("DecSlider with "+value);
        if((currentValue - value) >= 0){Debug.Log((currentValue-value));currentValue = currentValue - value;}
        else{currentValue = 0;}
        image.fillAmount = currentValue;
    }
}
