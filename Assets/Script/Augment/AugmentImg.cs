using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmentImg : MonoBehaviour
{
    public Sprite augImg1;
    public Sprite augImg2;
    public Sprite augImg3;
    public Sprite augImg4;
    public Sprite augImg5;
    public Sprite augImg6;
    public Sprite augImg7;
    public Sprite augImg8;

    public void matchImages(int aug1, int aug2, int aug3)
    {

        Image imageComponent = GetComponent<Image>();

        switch (aug1)
        {
            case 1:
                imageComponent.sprite = augImg1;
                break;
            case 2:
                imageComponent.sprite = augImg2;
                break;
            case 3:
                imageComponent.sprite = augImg3;
                break;
            case 4:
                imageComponent.sprite = augImg4;
                break;
            case 5:
                imageComponent.sprite = augImg5;
                break;
            case 6:
                imageComponent.sprite = augImg6;
                break;
            case 7:
                imageComponent.sprite = augImg7;
                break;
            case 8:
                imageComponent.sprite = augImg8;
                break;
        }

        switch (aug2)
        {
            case 1:
                imageComponent.sprite = augImg1;
                break;
            case 2:
                imageComponent.sprite = augImg2;
                break;
            case 3:
                imageComponent.sprite = augImg3;
                break;
            case 4:
                imageComponent.sprite = augImg4;
                break;
            case 5:
                imageComponent.sprite = augImg5;
                break;
            case 6:
                imageComponent.sprite = augImg6;
                break;
            case 7:
                imageComponent.sprite = augImg7;
                break;
            case 8:
                imageComponent.sprite = augImg8;
                break;
        }

        switch (aug3)
        {
            case 1:
                imageComponent.sprite = augImg1;
                break;
            case 2:
                imageComponent.sprite = augImg2;
                break;
            case 3:
                imageComponent.sprite = augImg3;
                break;
            case 4:
                imageComponent.sprite = augImg4;
                break;
            case 5:
                imageComponent.sprite = augImg5;
                break;
            case 6:
                imageComponent.sprite = augImg6;
                break;
            case 7:
                imageComponent.sprite = augImg7;
                break;
            case 8:
                imageComponent.sprite = augImg8;
                break;
        }
    }
}
