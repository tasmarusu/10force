/* *************************************************
* EnemyHPController �G��HP�����o�I�ɕ\��
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyHPController : MonoBehaviour
    {
        [SerializeField] private Image hpImage= null;

        public EnemyModel.DataConfig model { get; private set; } = null;

        private EnemyHPManager hpManager = null;
        private RectTransform rectTransform = null;


        /// <summary>
        /// ������
        /// </summary>
        public void OnInit(EnemyHPManager hpManager)
        {
            this.hpManager = hpManager;
            this.rectTransform = GetComponent<RectTransform>();
        }


        /// <summary>
        /// �Ďg�p
        /// </summary>
        public void ReUse(EnemyModel.DataConfig model)
        {
            this.model = model;
        }


        /// <summary>
        /// �X�V
        /// </summary>
        public void OnUpdate()
        {
            // HP�o�[�̒���
            float now = this.model.NowHP;
            float max = this.model.MaxHP;
            this.hpImage.fillAmount = now / max;

            // HP�o�[�̍��W�X�V
            this.MoveEnemyPos();

            // �ǐՂ��Ă��� model �����S����Δ�A�N�e�B�u�ɂ���
            if (this.model.State == EnemyModel.StateConfig.Des)
            {
                this.model = null;
                this.gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// �G�̍��W�̏ꏊ��HP�o�[���ړ�������
        /// </summary>
        private void MoveEnemyPos()
        {
            Vector2 modelPos = this.model.Pos;
            Vector2 pos = Vector2.zero;

            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(this.hpManager.WorldCamera, modelPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(this.hpManager.CanvasRect, screenPos, this.hpManager.UiCamera, out pos);
            this.rectTransform.localPosition = pos;
        }
    }
}