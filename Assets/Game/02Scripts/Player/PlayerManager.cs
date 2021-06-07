/* *************************************************
* PlayerManager プレイヤーを監視するスクリプト
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController = null;
        [SerializeField] private Transform playerParent = null;


        public PlayerController player { get; private set; } = null;



        /***************************************************
        * 初期化
        ************************************************** */
        public void Init()
        {
            // プレイヤーの生成
            player = Instantiate(playerController, playerParent);

            // プレイヤーコントローラーの初期化
            player.Init();
        }
    }
}