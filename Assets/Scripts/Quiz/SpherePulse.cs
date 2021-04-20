using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpherePulse : MonoBehaviour
{
    Tween pulse;

    private void OnEnable()
    {
        Vector3 targetScale = transform.localScale;
        transform.localScale = transform.localScale * 1.1f;        
        pulse = transform.DOScale(targetScale, .6f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void OnDisable()
    {
        pulse.Kill(true);
    }
    public void OnMouseEnter() {
        InfoUnderCursor.setText("The location of the correct region");
    }

}
