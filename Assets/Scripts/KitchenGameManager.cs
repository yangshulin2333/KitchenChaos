using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
   private enum State
   {
       WaitingToStart,//等待开始（尚未进入倒计时）
        CountdownToStart,//开始倒计时（游戏正式开始前的倒计时阶段）
        GamePlaying,
       GameOver
    }
}
