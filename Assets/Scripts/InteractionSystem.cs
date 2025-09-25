using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractabieObject : MonoBehaviour
{
    [Header("��ȣ �ۿ� ����")]
    public float interactionRange = 2.0f;          // ��ȣ �ۿ� ����
    public LayerMask interactionLayerMask = 1;    // ��ȣ �ۿ� ���̾� ����ũ
    public KeyCode interactionKey = KeyCode.E;    // ��ȣ �ۿ� Ű

    [Header("UI ����")]
    public Text interactionText;        // ��ȣ �ۿ� �ؽ�Ʈ UI
    public GameObject interactionUI;         // ��ȣ �ۿ� UI �г�

    private Transform playerTransform;
    private InteractableObject currentInteractiable;       // ������ ������Ʈ ��� Ŭ����

    void HandleInteractionInput()
    {
        if(currentInteractiable != null && Input.GetKeyDown(interactionKey)) 
        {
            currentInteractiable.Interact();                //�ൿ�� �Ѵ�.

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
