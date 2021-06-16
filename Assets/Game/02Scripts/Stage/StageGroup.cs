/* *************************************************
* StageGroup 1つのステージの形全てを管理
*            この子供に StageController が付いている
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;



    public class StageGroup : MonoBehaviour
    {
        [field: SerializeField] public StageController[] Controllers = null;


        /***************************************************
        * 初期化
        ************************************************** */
        public void Init()
        {
            for (int i = 0; i < this.Controllers.Length; i++)
            {
                this.Controllers[i].Init();
            }
        }
    }
}