using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndButton : MonoBehaviour
{
    public Button endButton;

    // Start is called before the first frame update
    void Start()
    {
        // 添加按钮点击事件的监听器
        endButton.onClick.AddListener(EndGameAction);
    }

    // 在点击按钮时调用的方法
    void EndGameAction()
    {
        Debug.Log("游戏结束");
        Application.Quit(); // 仅在构建后的应用程序中有效
    }
}