/* *************************************************
* StageGroup 1�̃X�e�[�W�̌`�S�Ă��Ǘ�
*            ���̎q���� StageController ���t���Ă���
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
        * ������
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