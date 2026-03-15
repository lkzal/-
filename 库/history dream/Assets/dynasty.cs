using UnityEngine;
using UnityEngine.UI;

public class dynasty : MonoBehaviour
{
    // 朝代面板
    public GameObject 画面秦;
    public GameObject 画面汉;
    public GameObject 画面唐;
    public GameObject 画面宋;
    public GameObject 画面明;

    // 统一隐藏所有
    void HideAll()
    {
        画面秦.SetActive(false);
        画面汉.SetActive(false);
        画面唐.SetActive(false);
        画面宋.SetActive(false);
        画面明.SetActive(false);
    }

    // 按钮调用的方法
    public void show秦() { HideAll(); 画面秦.SetActive(true); }
    public void Show汉() { HideAll(); 画面汉.SetActive(true); }
    public void Show唐() { HideAll(); 画面唐.SetActive(true); }
    public void Show宋() { HideAll(); 画面宋.SetActive(true); }
    public void Show明() { HideAll(); 画面明.SetActive(true); }
}
