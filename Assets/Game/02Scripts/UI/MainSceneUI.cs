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
        [SerializeField] private Camera uiCamera = null;
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private PlayerInput playerInput = null;
        [SerializeField] private EnemyHPManager enemyHP = null;

        public PlayerInput PlayerInput { get { return this.playerInput; } }


        /***************************************************
        * èâä˙âª
        ************************************************** */
        public void Init(EnemyModel data, Camera worldCamera)
        {
            this.playerInput.Init();
            this.enemyHP.Init(data, this.canvas.GetComponent<RectTransform>(), worldCamera, this.uiCamera);
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