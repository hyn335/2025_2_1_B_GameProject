using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractabieObject : MonoBehaviour
{
    [Header("상호 작용 설정")]
    public float interactionRange = 2.0f;          // 상호 작용 범위
    public LayerMask interactionLayerMask = 1;    // 상호 작용 레이어 마스크
    public KeyCode interactionKey = KeyCode.E;    // 상호 작용 키

    [Header("UI 설정")]
    public Text interactionText;        // 상호 작용 텍스트 UI
    public GameObject interactionUI;         // 상호 작용 UI 패널

    private Transform playerTransform;
    private InteractableObject currentInteractiable;       // 감지된 오브젝트 담는 클래스

    void HandleInteractionInput()
    {
        if(currentInteractiable != null && Input.GetKeyDown(interactionKey)) 
        {
            currentInteractiable.Interact();                //행동을 한다.

        }
    }

    void ShowInteractionUI(string text)
    {
        if(interactionUI != null)
        {
            interactionUI.SetActive(true);

        }

        if (interactionText != null)
        {
            interactionText.text = text;
        }
    }  


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
