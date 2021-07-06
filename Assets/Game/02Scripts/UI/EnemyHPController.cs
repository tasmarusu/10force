/* *************************************************
* EnemyHPController “G‚ÌHP‚ðŽ‹Šo“I‚É•\Ž¦
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EnemyHPController : MonoBehaviour
    {
        [SerializeField] private Image hpImage = null;

        private EnemyModel.DataConfig model;

        public void Init(EnemyModel.DataConfig model)
        {
            this.model = model;
        }

        
        public void OnUpdate()
        {
            float now = this.model.NowHP;
            float max = this.model.MaxHP;
            Debug.Log($"now {now} max {max}");

            hpImage.fillAmount = now / max;
        }


        public void HP()
        {
           
        }
    }
}