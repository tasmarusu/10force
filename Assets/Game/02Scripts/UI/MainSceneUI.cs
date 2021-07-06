/* *************************************************
* MainSceneUI InGame の UI表示
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainSceneUI : SingletonMono<MainSceneUI>
    {
        [SerializeField] private PlayerInput playerInput = null;
        [SerializeField] private EnemyHPController enemyHP = null;

        public PlayerInput PlayerInput { get { return this.playerInput; } }


        /***************************************************
        * 初期化
        ************************************************** */
        public void Init(EnemyModel enemy)
        {
            this.playerInput.Init();
            this.enemyHP.Init(enemy.Data[0]);
        }



        /***************************************************
        * Update更新
        ************************************************** */
        public void OnUpdate()
        {
            this.enemyHP.OnUpdate();
        }
    }
}