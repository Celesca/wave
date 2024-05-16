using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AugmentImg : MonoBehaviour
{
    public Sprite[] augImages;
    public Image[] images;
    public TextMeshProUGUI[] texts;
    public string[] augTexts;

    /*
    { "<b><align=center> Health Boost </align></b>\n\n<align=left>+1 Health Point</align>",
      "<b><align=center> Armor Boost </align></b>\n\n<align=left>+1 Armor Point</align>",
      "<b><align=center> Double Jump </align></b>\n\n<align=left>You can double jump by pressing [Space] in air</align>",
      "<b><align=center> Anti Projectile </align></b>\n\n<align=left>Allows you to be hit by a projectile object once</align>",
      "<b><align=center> Cold Water Gun </align></b>\n\n<align=left></align>",
      "<b><align=center> Burning Water Gun </align></b>\n\n<align=left></align>",
      "<b><align=center> Toxic Water Gun </align></b>\n\n<align=left></align>",
      "<b><align=center> Cooldown Reset </align></b>\n\n<align=left></align>"
    };
    */
    
    public Sprite errorImg;
    private string errorText = "No argument has been sent";

    void Start()
    {
        // testing
        //setAllImage(5, 3, 8);
    }

    public void setAllImage(int aug1, int aug2, int aug3)
    {
        setImage(0, aug1);
        setImage(1, aug2);
        setImage(2, aug3);
    }

    private void setImage(int index, int aug)
    {
        if (aug >= 1 && aug <= augImages.Length)
        {
            images[index].sprite = augImages[aug - 1];
            texts[index].text = augTexts[aug - 1];
        }
        else
        {
            images[index].sprite = errorImg;
            texts[index].text = errorText;
        }
    }
}