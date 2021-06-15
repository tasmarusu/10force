/* *************************************************
* StageManager �X�e�[�W���Ď�����X�N���v�g
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class StageManager : MonoBehaviour
    {
        public StageController[] StageController { get; private set; } = null;


        /***************************************************
        * ������
        ************************************************* */
        public void Init()
        {

        }


        /// <summary>
        /// �v���C���[�̍��W��߂�
        /// </summary>
        /// <returns> �v���C���[�̖߂�␳�l </returns>
        public Vector2 Coodinate(Vector2 playerPos)
        {
            int stageCount = this.StageController.Length;
            bool[] isOutStagePos = Enumerable.Repeat<bool>(false, stageCount).ToArray();

            // �S�ẴX�e�[�W�������ĂȂ����ǂ������ׂ�
            for (int i = 0; i < this.StageController.Length; i++)
            {
                isOutStagePos[i] = this.StageController[i].IsOutPlayerPos(playerPos);

                // false �Ȃ��͓����Ă�
                if (isOutStagePos[i] == false)
                {
                    return Vector3.zero;
                }
            }

            // �����ĂȂ��̂ŋ߂����͂ǂ̃X�e�[�W���𒲂ׂ�
            int nearNum = 0;
            float nearDistance = this.StageController[0].GetStageToPlayerDistance(playerPos);
            for (int i = 1; i < this.StageController.Length; i++)
            {
                float dis = this.StageController[i].GetStageToPlayerDistance(playerPos);
                if (dis < nearDistance)
                {
                    nearDistance = dis;
                    nearNum = i;
                }
            }

            // ���ׂ����ʂ̃X�e�[�W������
            StageController nearStage = this.StageController[nearNum];

            // �O�ɏo�Ȃ��悤�ɖ߂�
            Vector2 backValue = Vector2.zero;
            switch (nearStage.UseType)
            {
                // �~�`
                // 1.�v���C���[���狅�̂̃x�N�g�������
                // 2.�v���C���[�Ƌ��̒��S�̋��� - ���̂̔��a = �v���C���[���߂��
                case ColliderType.Circle:
                    Vector2 vec = nearStage.GetStageToPlayerVec(playerPos);
                    float dis = nearStage.GetStageToPlayerDistance(playerPos);
                    float radius = nearStage.Circle.Radius;
                    backValue = (dis - radius) * vec;

                    break;

                // �l�p�`
                // 1.��ɏo�Ă��牺�� �v���C���[�̏c���W - �l�p�`�̍��� = �v���C���[�����ɖ߂��
                // 2 ���ɏo�Ă����� �v���C���[�̏c���W - �l�p�`�̍��� = �v���C���[����ɖ߂��
                // 3 �E�ɏo�Ă����獶�� �v���C���[�̉����W - �l�p�`�̒��� = �v���C���[�����ɖ߂��
                // 4 ���ɏo�Ă�����E�� �v���C���[�̉����W - �l�p�`�̒��� = �v���C���[�����ɖ߂��
                case ColliderType.Box:



                    break;
            }

            return backValue;
        }
    }
}