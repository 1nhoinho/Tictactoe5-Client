using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]

public class PanelController : MonoBehaviour
{
    // 팝업창 RectTransform
    [SerializeField] private RectTransform panelRectTransform;

    // 멤버변수와 함수 내의 변수를 구분하기 위한 언더바 _ 따로 기능은 x
    private CanvasGroup _backgroundCanvasGroup;

    // Pamel 이 Hide 될 때 해야 할 동작

    public delegate void PanelControllerHideDelegate();


    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    // Panel 표시
    public void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    // Panel 숨기기
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack)
            .OnComplete(() =>
        {
            hideDelegate?.Invoke();
            Destroy(gameObject);
        });
    }

}
