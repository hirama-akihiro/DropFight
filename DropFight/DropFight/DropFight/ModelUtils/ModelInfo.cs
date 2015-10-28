using Microsoft.Xna.Framework;

namespace DropFight.ModelUtils
{
    /// <summary>
    /// モデルの情報
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position = Vector3.Zero;

        /// <summary>
        /// 角度(ラジアン)
        /// </summary>
        public Vector3 Radian = Vector3.Zero;

        /// <summary>
        /// 拡大率
        /// </summary>
        public Vector3 Scale = Vector3.One;

        /// <summary>
        /// 座標変換行列
        /// </summary>
        public Matrix World
        {
            get
            {
                // 拡大、回転、移動の順に変換する
                Matrix value = Matrix.CreateScale(Scale);
                value *= Matrix.CreateRotationX(Radian.X);
                value *= Matrix.CreateRotationY(Radian.Y);
                value *= Matrix.CreateRotationZ(Radian.Z);
                return value * Matrix.CreateTranslation(Position);
            }
        }
    }
}
