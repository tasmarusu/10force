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
        [SerializeField] private StageGroup[] Groups = null;

        public StageGroup useStage { get; private set; } = null;


        /***************************************************
        * ������
        ************************************************* */
        public void Init()
        {
            // �g���X�e�[�W�������_������ TODO ��Ō��ߑł�����悤�ɕύX
            int num = Random.Range(0, this.Groups.Length);
            this.useStage = Instantiate(this.Groups[num], this.transform);

            // ������
            for (int i = 0; i < this.useStage.Controllers.Length; i++)
            {
                this.useStage.Init();
            }
        }


        /// <summary>
        /// �v���C���[�̍��W������ɖ߂�
        /// </summary>
        /// <returns> �v���C���[�̕␳���ꂽ���W </returns>
        public Vector2 ReplaceOutPlayerPos(Vector2 playerPos)
        {
            StageController[] controllers = this.useStage.Controllers;
            int stageCount = controllers.Length;
            bool[] isOutStagePos = Enumerable.Repeat<bool>(false, stageCount).ToArray();

            // �S�ẴX�e�[�W�������ĂȂ����ǂ������ׂ�
            for (int i = 0; i < controllers.Length; i++)
            {
                isOutStagePos[i] = controllers[i].IsOutPlayerPos(playerPos);

                // false �Ȃ��͓����Ă�
                if (isOutStagePos[i] == false)
                {
                    return playerPos;
                }
            }

            // �����ĂȂ��̂ŋ߂����͂ǂ̃X�e�[�W���𒲂ׂ�
            //int nearNum = 0;
            //float nearDistance = controllers[0].GetStageToPlayerDistance(playerPos);
            //for (int i = 1; i < controllers.Length; i++)
            //{
            //    float dis = controllers[i].GetStageToPlayerDistance(playerPos);
            //    if (dis < nearDistance)
            //    {
            //        nearDistance = dis;
            //        nearNum = i;
            //    }
            //}

            Vector2[] backVecs = new Vector2[controllers.Length];
            float[] distances = new float[controllers.Length];
            for (int i = 0; i < controllers.Length; i++)
            {
                StageController stage = controllers[i];

                switch (stage.UseType)
                {
                    // �~�`
                    // 1.�v���C���[���狅�̂̃x�N�g�������
                    // 2.�v���C���[�Ƌ��̒��S�̋��� - ���̂̔��a = �v���C���[���߂��
                    case ColliderType.Circle:
                        float dis = stage.GetStageToPlayerDistance(playerPos);  // ���̃X�e�[�W�̒��S�ƃv���C���[�Ƃ̋���
                        float radius = stage.Circle.Radius;                     // ���̃X�e�[�W�̔��a
                        backVecs[i] = stage.GetStageToPlayerVec(playerPos);     // ���̃X�e�[�W�̒��S�ƃv���C���[�Ƃ̃x�N�g��
                        distances[i] = Vector2.Distance(playerPos, playerPos + ((dis - radius) * backVecs[i]));
                        break;

                    // �l�p�`
                    case ColliderType.Box:
                        Vector2 stagePos = stage.Box.Pos;
                        Vector2 backDis = Vector2.zero;

                        // �c�̖߂�ʂ����߂�
                        float maxHeight = stagePos.y + stage.Box.Height;
                        float minHeight = stagePos.y - stage.Box.Height;
                        if (playerPos.y > maxHeight)
                        {
                            backDis.y = maxHeight - playerPos.y;
                        }
                        else if (playerPos.y < minHeight)
                        {
                            backDis.y = minHeight - playerPos.y;
                        }

                        // ���̖߂�ʂ����߂�
                        float maxWidth = stagePos.x + stage.Box.Width;
                        float minWidth = stagePos.x - stage.Box.Width;
                        if (playerPos.x > maxWidth)
                        {
                            backDis.x = maxWidth - playerPos.x;
                        }
                        else if (playerPos.x < minWidth)
                        {
                            backDis.x = minWidth - playerPos.x;
                        }

                        backVecs[i] = ((playerPos + backDis) - playerPos).normalized;

                        // �߂�Ȃ���΂Ȃ�Ȃ����������
                        distances[i] = Vector2.Distance(playerPos, playerPos + backDis);

                        break;
                }
            }


            // ��Ԍ��̃X�e�[�W�ɖ߂鋗�������Ȃ��X�e�[�W��ݒ肷��
            int nearNum = 0;
            float nearDistance = distances[nearNum];
            for (int i = 1; i < controllers.Length; i++)
            {
                if (distances[i] < nearDistance)
                {
                    nearDistance = distances[i];
                    nearNum = i;
                }
            }

            // �ݒ肵�� Num �̃X�e�[�W�̖߂��ʂ����Z����
            Vector2 replacePlayerPos = playerPos + (nearDistance * backVecs[nearNum]);

            // ���ׂ����ʋ߂��X�e�[�W������
            //StageController nearStage = controllers[nearNum];

            //// �O�ɏo�Ȃ��悤�ɖ߂�
            //Vector2 replacePlayerPos = Vector2.zero;
            //switch (nearStage.UseType)
            //{
            //    // �~�`
            //    // 1.�v���C���[���狅�̂̃x�N�g�������
            //    // 2.�v���C���[�Ƌ��̒��S�̋��� - ���̂̔��a = �v���C���[���߂��
            //    case ColliderType.Circle:
            //        Vector2 vec = nearStage.GetStageToPlayerVec(playerPos);
            //        float dis = nearStage.GetStageToPlayerDistance(playerPos);
            //        float radius = nearStage.Circle.Radius;
            //        replacePlayerPos = playerPos + ((dis - radius) * vec);

            //        break;

            //    // �l�p�`
            //    // 1.��ɏo�Ă��牺�� �v���C���[�̏c���W - �l�p�`�̍��� = �v���C���[�����ɖ߂��
            //    // 2 ���ɏo�Ă����� �v���C���[�̏c���W - �l�p�`�̍��� = �v���C���[����ɖ߂��
            //    // 3 �E�ɏo�Ă����獶�� �v���C���[�̉����W - �l�p�`�̒��� = �v���C���[�����ɖ߂��
            //    // 4 ���ɏo�Ă�����E�� �v���C���[�̉����W - �l�p�`�̒��� = �v���C���[�����ɖ߂��
            //    case ColliderType.Box:
            //        Vector2 stagePos = nearStage.Box.Pos;
            //        float min = stagePos.x - nearStage.Box.Width;
            //        float max = stagePos.x + nearStage.Box.Width;
            //        replacePlayerPos.x = Mathf.Clamp(playerPos.x, min, max);
            //        min = stagePos.y - nearStage.Box.Height;
            //        max = stagePos.y + nearStage.Box.Height;
            //        replacePlayerPos.y = Mathf.Clamp(playerPos.y, min, max);

            //        break;
            //}

            return replacePlayerPos;
        }
    }
}