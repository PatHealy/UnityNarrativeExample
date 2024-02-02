using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CameraBehavior : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    public static CameraBehavior instance;
    private TextMeshProUGUI _text;
    private Transform _buttonParent, _canvas;

    private void Awake()
    {
        instance = this;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _canvas = GetComponentInChildren<Canvas>().transform;
        _buttonParent = _canvas.Find("ChoicePanel");
        RestoreFollowPlayer();
    }

    public void FocusCamera(Transform target, Vector3 position)
    {
        _camera.enabled = true;
        _canvas.gameObject.SetActive(true);
        _camera.LookAt = target;
        transform.position = position;
    }

    public void RestoreFollowPlayer()
    {
        _canvas.gameObject.SetActive(false);
        _camera.enabled = false;
    }

    public void SetDialogueText(string text)
    {
        _text.text = _text.text + text + "\n";
    }

    public void ClearText()
    {
        _text.text = "";
    }

    public void AddChoice(Button b)
    {
        b.transform.parent = _buttonParent;
    }

    public void ClearChoices()
    {
        int childCount = _buttonParent.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(_buttonParent.GetChild(i).gameObject);
        }
    }
}
