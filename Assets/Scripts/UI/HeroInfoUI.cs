using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 用于在游戏画面中展示英雄信息（血条、名字等）
/// </summary>
public class HeroInfoUI : MonoBehaviour
{
    public Canvas canvas;
    public Transform infoPanelTF;
    public Transform hpBarLoc;
    /// <summary>
    /// 血条的缩放比例
    /// </summary>
    public float hpBarScale = 1;
    /// <summary>
    /// 血条缩放的最小比例
    /// </summary>
    public float minScale = 0.2f;
    /// <summary>
    /// 最大缩放比例
    /// </summary>
    public float maxScale = 1f;
    /// <summary>
    /// 最远缩放距离，当距离大于等于最远时，缩放比例为最小比例
    /// </summary>
    private float maxDistance = 10;
    /// <summary>
    /// 最近缩放距离，当距离小于等于最近时，缩放比例为最大比例
    /// </summary>
    private float minDistance = 1;
    private Camera playerCamera;
    public GameObject hpBar;
    private Slider hpBarSlider;
    private HeroInfo heroInfo;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        heroInfo = GetComponent<HeroInfo>();
        infoPanelTF = canvas.transform.Find("InfoPanel");
        playerCamera = Camera.main;
        hpBarLoc = this.transform.Find("HPBarLoc");
        hpBar = Instantiate(ResourceManager.GetUIPrefab("HPBar"), infoPanelTF as Transform);
        hpBar.name = this.name + "HPBar";
        hpBarSlider = hpBar.GetComponent<Slider>();
        Init();
    }
    private void Init()
    {
        //血条
        hpBarSlider.maxValue = heroInfo.maxHp;
    }
    private void FixedUpdate()
    {
        UpdateInfo();
        UpdateHPSlider();
    }
    private void UpdateInfo()
    {
        //更新位置
        hpBar.transform.localPosition = UIHelper.ScreenToLocalPoint(playerCamera.WorldToScreenPoint(hpBarLoc.position), Screen.width, Screen.height);
        if(hpBar.transform.localPosition.z == 0 || heroInfo.transform.position.y < 0)
        {
            if (hpBar.activeSelf)
            {
                hpBar.SetActive(false);
            }
            
        }
        else
        {
            if (!hpBar.activeSelf)
            {
                hpBar.SetActive(true);
            }
        }
        //更新缩放
        float dis = (playerCamera.transform.position - this.transform.position).magnitude;
        if(dis < minDistance)
        {
            hpBarScale = maxScale;
        }
        else if(dis > maxDistance)
        {
            hpBarScale = minScale;
        }
        else
        {
            hpBarScale = minScale + (maxDistance - dis) / (maxDistance - minDistance) * (maxScale - minScale);
        }
        hpBar.transform.localScale = Vector3.one * hpBarScale;
    }
    private void UpdateHPSlider()
    {
        hpBarSlider.value = heroInfo.currentHp;
    }
}
