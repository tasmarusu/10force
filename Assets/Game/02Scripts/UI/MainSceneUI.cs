/* *************************************************
* MainSceneUI InGame ÇÃ UIï\é¶
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
        * èâä˙âª
        ************************************************** */
        public void Init(EnemyModel enemy)
        {
            this.playerInput.Init();
            this.enemyHP.Init(enemy.Data[0]);
        }



        /***************************************************
        * UpdateçXêV
        ************************************************** */
        public void OnUpdate()
        {
            this.enemyHP.OnUpdate();
        }
    }
}