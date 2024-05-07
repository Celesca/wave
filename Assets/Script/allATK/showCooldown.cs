using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class showCooldown : MonoBehaviour
{
    public playerATK plATK;
    public float leftTime = 5f;

    Text showTime;

    // Start is called before the first frame update
    void Start()
    {
        showTime = GameObject.Find("/All_UI/showTime").GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (plATK.isSkillCooldown && leftTime > 0)
        {
            showTime.gameObject.SetActive(true);
            leftTime -= Time.deltaTime;
            showTime.text = leftTime.ToString("F2");
        }
        else if(leftTime < 0)
        {
            showTime.gameObject.SetActive(false);
            leftTime = 5;
            

        }
    }
}
