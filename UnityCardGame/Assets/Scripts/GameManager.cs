using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region KID
    [Header("卡片陣列")]
    public GameObject[] cards;
    [Header("發牌按鈕")]
    public Button btnGetCard;
    
    private int player, pc;     // 玩家、電腦卡片編號

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        btnReplay.onClick.AddListener(Replay);
    }

    /// <summary>
    /// 玩家取得卡片
    /// </summary>
    public void PlayerGetCard()
    {
        btnGetCard.interactable = false;
        player = GetCard(new Vector3(0, -3, 0));

        Invoke("PcGetCard", 1.5f);
        Invoke("GameWinner", 2.5f);
    }

    /// <summary>
    /// 電腦取得卡片
    /// </summary>
    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="pos">卡片座標</param>
    /// <returns>取得的卡片編號</returns>
    private int GetCard(Vector3 pos)
    {
        aud.PlayOneShot(soundGetCard);

        int r = Random.Range(0, cards.Length);

        Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

        return r + 1;
    }
    #endregion

    #region 練習區域
    [Header("音效區域")]
    public AudioClip soundGetCard;  // 發牌
    public AudioClip soundWin;      // 獲勝
    public AudioClip soundLose;     // 失敗
    public AudioClip soundTie;      // 平手

    private AudioSource aud;        // 音效來源：喇叭
    public GameObject EndScene;
    [Header("結束標題")]
    public Text textFinalTitle;
    [Header("重來按鈕")]
    public Button btnReplay;

    /// <summary>
    /// 勝負顯示：使用玩家與電腦取得卡片判斷獲勝、平手或失敗
    /// 玩家卡片編號：player
    /// 電腦卡片編號：pc
    /// 顯示結算畫面
    /// </summary>
    private void GameWinner()
    {
        if (player > pc)
        {
            EndScene.SetActive(true);
            aud.PlayOneShot(soundWin);
            textFinalTitle.text = "恭喜你獲勝啦！";
            print("你贏了");
        }
        else if (player == pc)
        {
            EndScene.SetActive(true);
            aud.PlayOneShot(soundTie);
            textFinalTitle.text = "居然平手惹！！！";
            print("平手");
        }
        else
        {
            EndScene.SetActive(true);
            aud.PlayOneShot(soundLose);
            textFinalTitle.text = "輸惹......";
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("練習場景");
    }
    #endregion
}
