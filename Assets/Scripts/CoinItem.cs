using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : InteractableObject
{
    [Header("동전 설정")]
    public int coinValue = 10;      // 동전 값
    public string questTag = "Coin"; // 퀘스트에서 사용할 태그

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        objectName = "동전";
        interactionText = "[E] 동전 획득";
        interactionType = InteractionType.Item;
    }

    // 수집 시 호출
    protected override void CollectItem()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }

        Destroy(gameObject);
    }
}
