/* *************************************************
* EnemyHPController 敵のHPを視覚的に表示
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
        /// 初期化
        /// </summary>
        public void OnInit(EnemyHPManager hpManager)
        {
            this.hpManager = hpManager;
            this.rectTransform = GetComponent<RectTransform>();
        }


        /// <summary>
        /// 再使用
        /// </summary>
        public void ReUse(EnemyModel.DataConfig model)
        {
            this.model = model;
        }


        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            // HPバーの長さ
            float now = this.model.NowHP;
            float max = this.model.MaxHP;
            this.hpImage.fillAmount = now / max;

            // HPバーの座標更新
            this.MoveEnemyPos();

            // 追跡している model が死亡すれば非アクティブにする
            if (this.model.State == EnemyModel.StateConfig.Des)
            {
                this.model = null;
                this.gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// 敵の座標の場所にHPバーを移動させる
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