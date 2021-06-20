/* *************************************************
* StageController �X�e�[�W�̌`�Ȃǂ�ێ�
*                 ���̐e�� StageGroup ���t���Ă���
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public enum ColliderType
    {
        /// <summary>
        /// �~�^ �̓����蔻��
        /// </summary>
        Circle = 0,
        /// <summary>
        /// ���^ �̓����蔻��
        /// </summary>
        Box = 1,
    }

    public class StageController : MonoBehaviour
    {
        [field: SerializeField] public ColliderType UseType { get; private set; } = ColliderType.Circle;
        public CircleStruct Circle { get; private set; }
        public BoxStruct Box { get; private set; }


        /***************************************************
        * Collider �ɂ���Č��܂����\���̂̒��g�����߂�
        ************************************************** */
        // �~�^
        public struct CircleStruct
        {
            public CircleStruct(Vector2 pos, float radius, CircleCollider2D collider)
            {
                this.Pos = pos;
                this.Radius = radius;
                this.Collider = collider;
            }
            public Vector2 Pos { get; }    // ���S���W
            public float Radius { get; }   // ���a
            public CircleCollider2D Collider { get; }  // �����蔻��
        }
        // ���^
        public struct BoxStruct
        {
            public BoxStruct(Vector2 pos, float width, float height, BoxCollider2D collider)
            {
                this.Pos = pos;
                this.Width = width;
                this.Height = height;
                this.Collider = collider;
            }
            public Vector2 Pos { get; }    // ���S���W
            public float Width { get; }    // ���̒���
            public float Height { get; }   // �c�̒���
            public BoxCollider2D Collider { get; } // �����蔻��
        }

        /***************************************************
        * ������
        ************************************************** */
        public void Init()
        {
            Vector2 pos = this.transform.position;
            switch (this.UseType)
            {
                case ColliderType.Circle:
                    CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
                    float radius = circleCollider.bounds.size.x * 0.5f;
                    this.Circle = new CircleStruct(pos, radius, circleCollider);

                    break;

                case ColliderType.Box:
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    float width = boxCollider.bounds.size.x;
                    float height = boxCollider.bounds.size.y;
                    this.Box = new BoxStruct(pos, width, height, boxCollider);

                    break;
            }
        }

        /***************************************************
        * ���̃X�e�[�W�Ńv���C���[���O�ɏo�Ă邩�ǂ���
        ************************************************** */
        public bool IsOutPlayerPos(Vector2 playerPos)
        {
            Vector2 pos = this.transform.position;

            // �͈͊O�ɂ��邩�ǂ����̔���
            switch (this.UseType)
            {
                case ColliderType.Circle:
                    // �v���C���[�Ƌ��̋����Ƌ��̔��a����͈͊O�����߂�
                    float dis = Vector2.Distance(playerPos, pos);
                    Debug.Log($"dis {dis} radius { this.Circle.Radius}");
                    if (dis > this.Circle.Radius)
                    {
                        // �͈͊O
                        return true;
                    }

                    break;

                case ColliderType.Box:
                    // ���c�̃v���C���[�Ƃ̋��������
                    float height = Mathf.Abs(pos.y - playerPos.y);  // �c
                    float width = Mathf.Abs(pos.x - playerPos.x);   // ��

                    // �͈͊O�Ȃ�ŒZ�����̈ʒu�֗����߂�
                    // �c
                    if (height > this.Box.Height)
                    {
                        // �͈͊O
                        // float value = height - this.box.Height;
                        return true;
                    }
                    // ��
                    if (width > this.Box.Width)
                    {
                        // �͈͊O
                        // float value = width - this.box.Width;
                        return true;
                    }

                    break;
            }

            // �͈͊O�ɏo�Ă��Ȃ��̂� false ��Ԃ�
            return false;
        }


        /***************************************************
        * ���̃X�e�[�W�̒��S�ƃv���C���[�Ƃ̋���
        ************************************************** */
        public float GetStageToPlayerDistance(Vector2 playerPos)
        {
            return Vector2.Distance(this.transform.position, playerPos);
        }


        /***************************************************
        * ���̃X�e�[�W�̒��S�ƃv���C���[�Ƃ̃x�N�g��
        ************************************************** */
        public Vector2 GetStageToPlayerVec(Vector2 playerPos)
        {
            return ((Vector2)this.transform.position - playerPos).normalized;
        }
    }
}