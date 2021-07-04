/* *************************************************
* MainSceneUI InGame �� UI�\��
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainSceneUI : SingletonMono<MainSceneUI>
    {
        [SerializeField] private PlayerInput playerInput = null;

        public PlayerInput PlayerInput { get { return this.playerInput; } }


        /***************************************************
        * ������
        ************************************************** */
        public void Init()
        {
            this.playerInput.Init();
        }
    }
}