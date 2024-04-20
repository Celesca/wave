using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Sprite errorImg;

    public Image image1;
    public Image image2;
    public Image image3;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    private string augText1 = "<b><align=center> Health Boost </align></b>\n\n" +
                              "<align=left>+1 Health Point</align>";
    private string augText2 = "<b><align=center> Armor Boost </align></b>\n\n" +
                              "<align=left>+1 Armor Point</align>";
    private string augText3 = "<b><align=center> Double Jump </align></b>\n\n" +
                              "<align=left>You can double jump by pressing [Space] in air</align>";
    private string augText4 = "<b><align=center> Anti Projectile </align></b>\n\n" +
                              "<align=left>Allows you to be hit by a projectile object once</align>";
    private string augText5 = "<b><align=center> Cold Water Gun </align></b>\n\n" +
                              "<align=left></align>";
    private string augText6 = "<b><align=center> Burning Water Gun </align></b>\n\n" +
                              "<align=left></align>";
    private string augText7 = "<b><align=center> Toxic Water Gun </align></b>\n\n" +
                              "<align=left></align>";
    private string augText8 = "<b><align=center> Cooldown Reset </align></b>\n\n" +
                              "<align=left></align>";
    private string errorText = "No argument has been sent";

    void Start()
    {
        // testing
        //setAllImage(1, 3, 7);
    }

    public void setAllImage(int aug1, int aug2, int aug3)
    {
        Debug.Log($"In AugmentImg receive from GameManagerScript: {aug1}, {aug2}, {aug3}");
        setImage1(aug1);
        setImage2(aug2);
        setImage3(aug3);
    }

    public void setImage1(int aug1)
    {
        switch (aug1)
        {
            case 1:
                image1.sprite = augImg1;
                text1.text = augText1;
                break;
            case 2:
                image1.sprite = augImg2;
                text1.text = augText2;
                break;
            case 3:
                image1.sprite = augImg3;
                text1.text = augText3;
                break;
            case 4:
                image1.sprite = augImg4;
                text1.text = augText4;
                break;
            case 5:
                image1.sprite = augImg5;
                text1.text = augText5;
                break;
            case 6:
                image1.sprite = augImg6;
                text1.text = augText6;
                break;
            case 7:
                image1.sprite = augImg7;
                text1.text = augText7;
                break;
            case 8:
                image1.sprite = augImg8;
                text1.text = augText8;
                break;
            default:
                image1.sprite = errorImg;
                text1.text = errorText;
                break;
        }
    }

    public void setImage2(int aug2)
    {
        switch (aug2)
        {
            case 1:
                image2.sprite = augImg1;
                text2.text = augText1;
                break;
            case 2:
                image2.sprite = augImg2;
                text2.text = augText2;
                break;
            case 3:
                image2.sprite = augImg3;
                text2.text = augText3;
                break;
            case 4:
                image2.sprite = augImg4;
                text2.text = augText4;
                break;
            case 5:
                image2.sprite = augImg5;
                text2.text = augText5;
                break;
            case 6:
                image2.sprite = augImg6;
                text2.text = augText6;
                break;
            case 7:
                image2.sprite = augImg7;
                text2.text = augText7;
                break;
            case 8:
                image2.sprite = augImg8;
                text2.text = augText8;
                break;
            default:
                image2.sprite = errorImg;
                text2.text = errorText;
                break;
        }
    }

    public void setImage3(int aug3)
    {
        switch (aug3)
        {
            case 1:
                image3.sprite = augImg1;
                text3.text = augText1;
                break;
            case 2:
                image3.sprite = augImg2;
                text3.text = augText2;
                break;
            case 3:
                image3.sprite = augImg3;
                text3.text = augText3;
                break;
            case 4:
                image3.sprite = augImg4;
                text3.text = augText4;
                break;
            case 5:
                image3.sprite = augImg5;
                text3.text = augText5;
                break;
            case 6:
                image3.sprite = augImg6;
                text3.text = augText6;
                break;
            case 7:
                image3.sprite = augImg7;
                text3.text = augText7;
                break;
            case 8:
                image3.sprite = augImg8;
                text3.text = augText8;
                break;
            default:
                image3.sprite = errorImg;
                text3.text = errorText;
                break;
        }
    }
}