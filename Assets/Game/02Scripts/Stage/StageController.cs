/* *************************************************
* StageController �X�e�[�W�̌`�Ȃǂ�ێ�
************************************************* */
namespace MainForce
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StageController : MonoBehaviour
    {
        [SerializeField] private ColliderType useType = ColliderType.Circle;
        private Circle circle;
        private Box box;

        private enum ColliderType
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


        /***************************************************
        * Collider �ɂ���Č��܂����\���̂̒��g�����߂�
        ************************************************** */
        // �~�^
        struct Circle
        {
            public Circle(Vector2 pos, float radius, CircleCollider2D collider)
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
        struct Box
        {
            public Box(Vector2 pos, float width, float height, BoxCollider2D collider)
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
        * ���C���Q�[����UI�����[�h����
        ************************************************** */
        public void Init()
        {
            Vector2 pos = this.transform.position;
            switch (this.useType)
            {
                case ColliderType.Circle:
                    CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
                    float radius = circleCollider.radius;
                    this.circle = new Circle(pos, radius, circleCollider);

                    break;

                case ColliderType.Box:
                    BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
                    float width = boxCollider.bounds.size.x;
                    float height = boxCollider.bounds.size.y;
                    this.box = new Box(pos, width, height, boxCollider);

                    break;
            }
        }

        /***************************************************
        * ���C���Q�[����UI�����[�h����
        ************************************************** */
        public void OutgoingPosCoordinates(Vector2 playerPos)
        {
            Vector2 pos = this.transform.position;

            // �͈͊O�ɂ��邩�ǂ����̔���
            switch (this.useType)
            {
                case ColliderType.Circle:
                    // �v���C���[�Ƌ��̋����Ƌ��̔��a����͈͊O�����߂�
                    float dis = Vector2.Distance(playerPos, pos);
                    if (dis > this.circle.Radius)
                    {
                        // �͈͊O
                    }

                    break;

                case ColliderType.Box:
                    // ���c�̃v���C���[�Ƃ̋��������
                    float height = Mathf.Abs(pos.y - playerPos.y);  // �c
                    float width = Mathf.Abs(pos.x - playerPos.x);   // ��

                    // �͈͊O�Ȃ�ŒZ�����̈ʒu�֗����߂�
                    // �c
                    if (height > this.box.Height)
                    {
                        // �͈͊O
                        // float value = height - this.box.Height;
                    }
                    // ��
                    if (width > this.box.Width)
                    {
                        // �͈͊O
                        // float value = width - this.box.Width;
                    }

                    break;
            }

            // �S�ẴX�e�[�W�������ĂȂ����͍��W��߂�
            // ���� StageManager �Ŏ��ׂ�

            // 1.�߂����̓����蔻��̍��W���m�F

            // �~�`
            // 1.�v���C���[���狅�̂̃x�N�g�������
            // 2.�v���C���[�Ƌ��̂̋��� - ���̂̔��a = �v���C���[���߂��

            // �l�p�`
            // 1.��ɏo�Ă��牺�� �v���C���[�̏c���W - �l�p�`�̍��� = �v���C���[�����ɖ߂��
            // 2 ���ɏo�Ă����� �v���C���[�̏c���W - �l�p�`�̍��� = �v���C���[����ɖ߂��
            // 3 �E�ɏo�Ă����獶�� �v���C���[�̉����W - �l�p�`�̒��� = �v���C���[�����ɖ߂��
            // 4 ���ɏo�Ă�����E�� �v���C���[�̉����W - �l�p�`�̒��� = �v���C���[�����ɖ߂��
        }
    }
}