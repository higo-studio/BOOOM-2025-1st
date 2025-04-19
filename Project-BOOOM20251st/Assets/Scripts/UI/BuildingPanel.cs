using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuildingPanel : MonoBehaviour
{
    public Vector2 size = new Vector2(340, 455);
    public Vector2 btnSize = new Vector2(160, 85);
    CanvasGroup panel;
    public CanvasGroup btnList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel = GetComponent<CanvasGroup>();
        panel.alpha = 0;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPenal()
    {
        panel.alpha = 1;
        panel.interactable = true;
        panel.blocksRaycasts = true;
        panel.transform.GetComponent<RectTransform>().DOSizeDelta(size, 0.2f);
        DOTween.To(() => btnList.alpha, x => btnList.alpha = x, 1, 0.1f).SetDelay(0.2f);
    }

    public void ClosePanel()
    {
        btnList.alpha = 0;
        panel.transform.GetComponent<RectTransform>().DOSizeDelta(btnSize, 0.2f).OnComplete(() =>
        {
            panel.alpha = 0;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        });
    }
}
