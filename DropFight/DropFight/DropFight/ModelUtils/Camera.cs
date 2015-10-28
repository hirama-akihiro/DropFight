using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.ModelUtils
{
    /// <summary>
    /// モデル描画用のカメラ
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// 注視点
        /// </summary>
        public Vector3 Target;

        /// <summary>
        /// y軸方向の角度(ラジアン)
        /// </summary>
        public float UpRadian;

        /// <summary>
        /// ビュー行列
        /// </summary>
        public Matrix View
        {
            get
            {
                Vector3 upVector = Vector3.Transform(Vector3.Up, Matrix.CreateRotationZ(UpRadian));
                return Matrix.CreateLookAt(Position, Target, upVector);
            }
        }

        /// <summary>
        /// y軸方向の視野角(ラジアン)
        /// </summary>
        public float FieldOfViewRadian = float.Epsilon;

        /// <summary>
        /// ビューの幅÷高さ
        /// </summary>
        public float AspectRatio = 1;

        /// <summary>
        /// 前方クリップ位置
        /// 描画するオブジェクトまでの距離の下限
        /// </summary>
        public float NearPlaneDistance = 1e-3f;

        /// <summary>
        /// 後方クリップ位置
        /// 描画するオブジェクトまでの距離の上限
        /// </summary>
        public float FarPlaneDistance = float.MaxValue;

        /// <summary>
        /// 射影行列
        /// </summary>
        public Matrix Projection
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(FieldOfViewRadian, AspectRatio, NearPlaneDistance, FarPlaneDistance);
            }
        }
    }
}
