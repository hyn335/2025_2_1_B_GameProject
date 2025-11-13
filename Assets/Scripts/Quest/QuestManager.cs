using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;   // 매니저 싱글톤

    [Header("UI 요소들")]
    public GameObject questUI;             // 퀘스트 패널 UI
    public Text questTitleText;            // 퀘스트 제목 텍스트
    public Text questDescriptionText;      // 퀘스트 내용 텍스트
    public Text questProgressText;         // 퀘스트 진행도 텍스트
    public Button completeButton;          // 완료 버튼

    [Header("퀘스트 목록")]
    public QuestData[] availableQuests;    // 가지고 있는 퀘스트 목록

    private QuestData currentQuest;        // 현재 진행중인 퀘스트
    private int currentQuestIndex = 0;     // 퀘스트 순서 번호


    // 싱글톤 생성
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
    // 시작 시 최초 퀘스트 자동 시작
    
    void Start()
    {
     
        if (availableQuests.Length > 0)
        {
            StartQuest(availableQuests[0]);
        }

        
        if (completeButton != null)
        {
            completeButton.onClick.AddListener(CompleteCurrentQuest);
        }
    }

  
    // 프레임마다 퀘스트 진행 체크
    
    void Update()
    {
        if (currentQuest != null && currentQuest.isActive)
        {
            CheckQuestProgress();  // 퀘스트 종류에 따라 자동 체크
            UpdateQuestUI();       // UI 갱신
        }
    }

    
    
    void UpdateQuestUI()
    {
        if (currentQuest == null) return;

        if (questTitleText != null)
        {
            questTitleText.text = currentQuest.questTitle;
        }
        if (questDescriptionText != null)
        {
            questDescriptionText.text = currentQuest.description;
        }
        if (questProgressText != null)
        {
            questProgressText.text = currentQuest.GetProgressText();
        }
    }

    
    // 퀘스트 시작
    
    public void StartQuest(QuestData quest)
    {
        if (quest == null) return;

        currentQuest = quest;
        currentQuest.Initalize();
        currentQuest.isActive = true;

        Debug.Log("퀘스트 시작: " + questTitleText);

        UpdateQuestUI();

        if (questUI != null)
        {
            questUI.SetActive(true);
        }

       
    }

    // 현재 퀘스트 완료
    
    public void CompleteCurrentQuest()
    {
        if (currentQuest == null || !currentQuest.isCompleted) return;

        Debug.Log("퀘스트 완료! " + currentQuest.rewardMessage);

        if (completeButton != null)
        {
            completeButton.gameObject.SetActive(false);
        }
        // 다음 퀘스트로 넘어가기
        currentQuestIndex++;

        if (currentQuestIndex < availableQuests.Length)
        {
            StartQuest(availableQuests[currentQuestIndex]);
        }
        else
        {
            currentQuest = null;

            if (questUI != null)
            {
                questUI.gameObject.SetActive(false);
            }
        }
    }
    // 퀘스트 종류에 따라 자동 체크

    void CheckQuestProgress()
    {
        if (currentQuest.questType == QuestType.Delivery)  
        { 
                CheckDeliveryProgress();
        }

        // 완료 조건 달성 시 완료 버튼 활성화
        if (currentQuest.IsComplete() && !currentQuest.isCompleted)
        {
            currentQuest.isCompleted = true;

            if (completeButton != null)
            {
                completeButton.gameObject.SetActive(true);
            }
        }
    }

    
    // 배달형 퀘스트 체크
    
    void CheckDeliveryProgress()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null) return;

        float distance = Vector3.Distance(player.position, currentQuest.deliveryPosition);

        if (distance <= currentQuest.deliveryRedius)
        {
            if (currentQuest.currentProgresss == 0)
            {
                currentQuest.currentProgresss = 1;
            }
        }
        else
        {
            currentQuest.currentProgresss = 0;
        }
    }

   
    // 수집 퀘스트 외부 호출
    
    public void AddCollectProgress(string itemTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Collect && currentQuest.targetTag == itemTag)
            
        {
            currentQuest.currentProgresss++;
            Debug.Log("아이템 수집: " + itemTag);
        }
    }

    
    // 상호작용 퀘스트 외부 호출
    
    public void AddInteractProgress(string objectTag)
    {
        if (currentQuest == null || !currentQuest.isActive) return;

        if (currentQuest.questType == QuestType.Interect && currentQuest.targetTag == objectTag)
        {
            currentQuest.currentProgresss++;
            Debug.Log("상호작용 완료: " + objectTag);
        }
    }
}
