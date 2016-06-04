using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class PopupContinue : MonoBehaviour
{
	public Image imageFillAmount;

	public Button button;

	public Transform toBounce;

	bool onClikedYes = false;

	void Awake()
	{
		toBounce.localScale = new Vector3(0,0,1);

		onClikedYes = false;

		button.interactable = false;
	}

	public void OpenPopupContinue(Action<bool> success)
	{
		onClikedYes = false;

		toBounce.localScale = new Vector3(0,0,1);

		button.interactable = false;

		toBounce.DOScale(Vector3.one,1).OnComplete(() => {

			button.interactable = true;

			imageFillAmount.DOFillAmount(0,2)
				.OnComplete(() => {

					button.interactable = false;

					if(success!=null)
						success(false);

					Destroy(gameObject);
				})
				.OnKill(() => {

					button.interactable = false;

					if(success!=null)
						success(true);

					Destroy(gameObject);
				});
		});
	}

	public void OnClickedYes()
	{
		button.interactable = false;

		onClikedYes = true;
		DOTween.Kill(imageFillAmount);
	}

}
