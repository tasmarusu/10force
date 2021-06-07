/* *************************************************
* PlayerManager �v���C���[���Ď�����X�N���v�g
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



        /***************************************************
        * ������
        ************************************************** */
        public void Init(PlayerInput input)
        {
            // �v���C���[�̐���
            this.Player = Instantiate(playerController, this.playerParent);

            // �v���C���[�R���g���[���[�̏�����
            this.Player.Init(input);
        }


        /***************************************************
        * �v���C���[�̍X�V
        ************************************************** */
        public void OnUpdate()
        {
            this.Player.OnUpdate();
        }
    }
}