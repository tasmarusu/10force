/* *************************************************
* BulletController 全ての玉の親
*                  ***BulletController.csはこれが継承されてる
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BulletController : MonoBehaviour
    {
        // 敵に入るダメージ
        public float Damage { get; private set; } = 10;
    }
}