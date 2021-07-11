/* *************************************************
* EnemyHPManager �G��HP�����o�I�ɕ\������}�l�[�W���[
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyHPManager : MonoBehaviour
    {
        [SerializeField] private EnemyHPController hpPrefab = null;

        private int nowArriveNum = 0;
        private List<EnemyHPController> controllers = new List<EnemyHPController>();
        private EnemyModel modelData = null;

        public RectTransform CanvasRect { get; private set; } = null;
        public Camera WorldCamera { get; private set; } = null;
        public Camera UiCamera { get; private set; } = null;


        /// <summary>
        /// ������
        /// </summary>
        /// <param name="count"> �G�̐� </param>
        public void Init(EnemyModel data, RectTransform canvasRect, Camera worldCamera, Camera uiCamera)
        {
            this.modelData = data;
            this.CanvasRect = canvasRect;
            this.WorldCamera = worldCamera;
            this.UiCamera = uiCamera;

            // 5�����������Ă���
            for (int i = 0; i < 5; i++)
            {
                this.CreateHPBar(i);
                this.controllers[i].gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// �X�V
        /// </summary>
        public void OnUpdate()
        {
            List<EnemyModel.DataConfig> enemyData = new List<EnemyModel.DataConfig>();

            // �������Ă���G�̐��𐔂���
            for (int i = 0; i < modelData.Data.Length; i++)
            {
                // �������Ă���� HP�o�[ ��\��
                if (modelData.Data[i].State == EnemyModel.StateConfig.Arrive)
                {
                    enemyData.Add(modelData.Data[i]);
                }
            }

            // �o�������� OnUpdate() ������
            for (int i = 0; i < enemyData.Count; i++)
            {
                // �G���A�N�e�B�u�������u�Ԃ� Init ������
                if (modelData.Data.Contains(enemyData[i]) == true)
                {
                    // HP�o�[������Ȃ��Ȃ�ΐ���
                    if (i >= this.controllers.Count)
                    {
                        this.CreateHPBar(i);
                        this.controllers[i].ReUse(modelData.Data[i]);
                    }

                    // �A�N�e�B�u����ĂȂ��I�u�W�F�N�g���A�N�e�B�u������
                    if (this.controllers[i].gameObject.activeSelf == false)
                    {
                        this.controllers[i].gameObject.SetActive(true);
                        this.controllers[i].ReUse(modelData.Data[i]);
                    }
                }
            }

            // �����Ă��� HPController �� Update���Ă�
            for (int i = 0; i < this.controllers.Count; i++)
            {
                if (this.controllers[i].gameObject.activeSelf == true)
                {
                    this.controllers[i].OnUpdate();
                }
            }
        }


        /// <summary>
        /// HP�o�[�𐶐�����
        /// </summary>
        private void CreateHPBar(int i)
        {
            this.controllers.Add(Instantiate(this.hpPrefab, this.transform));
            this.controllers[i].OnInit(this);
        }
    }
}