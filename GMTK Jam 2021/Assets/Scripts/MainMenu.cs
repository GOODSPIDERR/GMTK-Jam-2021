using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Transform mainCamera;
    public GameObject main, credits;
    public void CreditsShow()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(main.transform.DOLocalMoveX(-1200, 0.5f, false));
        mySequence.Append(credits.transform.DOLocalMoveX(0, 0.5f, false));
    }

    public void CreditsHide()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(credits.transform.DOLocalMoveX(-1567f, 0.5f, false));
        mySequence.Append(main.transform.DOLocalMoveX(-710f, 0.5f, false));
    }

    private void Update() //Tilts the UI according to the mouse position
    {
        Vector2 mouseOffset = new Vector2(Screen.width / 2 - Input.mousePosition.x, Screen.height / 2 - Input.mousePosition.y);

        mainCamera.localRotation = Quaternion.Euler(mouseOffset.y * 0.005f, -mouseOffset.x * 0.005f, 0);
    }
}
