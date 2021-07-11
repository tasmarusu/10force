/* *************************************************
* EnemyHPManager 敵のHPを視覚的に表示するマネージャー
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
        /// 初期化
        /// </summary>
        /// <param name="count"> 敵の数 </param>
        public void Init(EnemyModel data, RectTransform canvasRect, Camera worldCamera, Camera uiCamera)
        {
            this.modelData = data;
            this.CanvasRect = canvasRect;
            this.WorldCamera = worldCamera;
            this.UiCamera = uiCamera;

            // 5個だけ生成しておく
            for (int i = 0; i < 5; i++)
            {
                this.CreateHPBar(i);
                this.controllers[i].gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            List<EnemyModel.DataConfig> enemyData = new List<EnemyModel.DataConfig>();

            // 生存している敵の数を数える
            for (int i = 0; i < modelData.Data.Length; i++)
            {
                // 生存していれば HPバー を表示
                if (modelData.Data[i].State == EnemyModel.StateConfig.Arrive)
                {
                    enemyData.Add(modelData.Data[i]);
                }
            }

            // 出現数だけ OnUpdate() を挟む
            for (int i = 0; i < enemyData.Count; i++)
            {
                // 敵がアクティブ化した瞬間に Init を挟む
                if (modelData.Data.Contains(enemyData[i]) == true)
                {
                    // HPバーが足りなくなれば生成
                    if (i >= this.controllers.Count)
                    {
                        this.CreateHPBar(i);
                        this.controllers[i].ReUse(modelData.Data[i]);
                    }

                    // アクティブされてないオブジェクトをアクティブ化する
                    if (this.controllers[i].gameObject.activeSelf == false)
                    {
                        this.controllers[i].gameObject.SetActive(true);
                        this.controllers[i].ReUse(modelData.Data[i]);
                    }
                }
            }

            // 動いている HPController の Updateを呼ぶ
            for (int i = 0; i < this.controllers.Count; i++)
            {
                if (this.controllers[i].gameObject.activeSelf == true)
                {
                    this.controllers[i].OnUpdate();
                }
            }
        }


        /// <summary>
        /// HPバーを生成する
        /// </summary>
        private void CreateHPBar(int i)
        {
            this.controllers.Add(Instantiate(this.hpPrefab, this.transform));
            this.controllers[i].OnInit(this);
        }
    }
}