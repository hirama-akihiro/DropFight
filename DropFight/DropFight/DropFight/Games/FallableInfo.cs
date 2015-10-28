using System;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;

namespace DropFight.Games
{
    /// <summary>
    /// 外部から見える落下可能な物の情報
    /// </summary>
    public abstract class FallableInfo
    {
        /// <summary>
        /// 落ちているかどうか
        /// </summary>
        public readonly bool IsFalling;

        /// <summary>
        /// 落下時間
        /// 落下していなければ TimeSpan.Zero
        /// </summary>
        public readonly TimeSpan FallTime;

        /// <summary>
        /// 位置
        /// </summary>
        public readonly Vector3 Position;

        /// <param name="fallable">落下可能な物</param>
        public FallableInfo(Fallable fallable)
        {
            this.IsFalling = fallable.IsFalling;
            this.FallTime = fallable.FallTime;

            ModelInfo modelInfo = fallable.ModelInfo;
            this.Position = modelInfo.Position;
        }
    }
}
