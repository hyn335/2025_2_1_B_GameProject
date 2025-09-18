
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("���� ����")]
    public int playerScore = 0;
    public int itemsColledted = 0;

    [Header("UI ����")]
    public Text scoreText;
    public Text itemCountText;
    public Text gameStatusText;

    public static GameManager instance;     //�̱��� ����

    private void Awake()
    {
         if(instance == null)
         {
             instance = this;
             DontDestroyOnLoad(gameObject); 
         }
         else
         {
             Destroy(gameObject); 
        }

    }

    public void Collectltem()
    {
        itemsColledted++;
        Debug.Log($"������ ����! (�� : {itemsColledted} ��");

    }

    public void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "���� : " + playerScore;
        }

        if(itemCountText !=null)
        { 
            itemCountText.text = "������ : " + itemsColledted + " ��"; 
        }

    }

    }
    // Start is called before the first frame update
    

