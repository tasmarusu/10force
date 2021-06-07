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


        public PlayerController player { get; private set; } = null;



        /***************************************************
        * ������
        ************************************************** */
        public void Init()
        {
            // �v���C���[�̐���
            player = Instantiate(playerController, playerParent);

            // �v���C���[�R���g���[���[�̏�����
            player.Init();
        }
    }
}