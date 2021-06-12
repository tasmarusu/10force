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

        public PlayerController Player { get; private set; } = null;
        public PlayerModel Model { get; private set; } = null;



        /***************************************************
        * 初期化
        ************************************************** */
        public void Init()
        {
            // プレイヤーの生成
            this.Player = Instantiate(playerController, this.playerParent);

            // プレイヤー関連の初期化
            this.Model = new PlayerModel();
            this.Player.Init(this.Model);
        }


        /***************************************************
        * プレイヤーの更新
        ************************************************** */
        public void OnUpdate()
        {
            this.Player.OnUpdate();
        }
    }
}