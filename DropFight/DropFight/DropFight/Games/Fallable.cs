using System;
using System.Diagnostics;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework;

namespace DropFight.Games
{
    /// <summary>
    /// 落下可能な物
    /// 
    /// ※サブクラスの更新メソッドは、必ずFallableクラスのUpdateメソッドを呼んでください
    /// </summary>
    public abstract class Fallable
    {
        /// <summary>
        /// 落下時間のタイマー
        /// </summary>
        private Stopwatch fallTimer = new Stopwatch();

        /// <summary>
        /// 落下しているかどうか
        /// </summary>
        public bool IsFalling
        {
            get
            {
                return fallTimer.IsRunning;
            }
        }

        /// <summary>
        /// 落下時間
        /// 落下していなければ TimeSpan.Zero
        /// </summary>
        public TimeSpan FallTime
        {
            get
            {
                return fallTimer.Elapsed;
            }
        }

        /// <summary>
        /// 落下を考慮する前のY座標
        /// </summary>
        protected float BasePositionY;

        /// <summary>
        /// モデルの情報
        /// </summary>
        public ModelInfo ModelInfo
        {
            get;
            private set;
        }

        public Fallable()
        {
            ModelInfo = new ModelInfo();

            ResetStatusOfFalling();
        }

        /// <summary>
        /// 落下させる
        /// </summary>
        public void Fall()
        {
            fallTimer.Start();
        }

        /// <summary>
        /// 落下を停止する
        /// </summary>
        public void StopFalling()
        {
            fallTimer.Stop();
        }

        /// <summary>
        /// 落下の状態を初期化する
        /// </summary>
        public void ResetStatusOfFalling()
        {
            fallTimer.Reset();
            ModelInfo.Position.Y = BasePositionY;
        }

        /// <summary>
        /// 座標を更新する
        /// </summary>
        protected void UpdatePosition()
        {
            ModelInfo.Position.Y = BasePositionY + (float)(-0.01 * FallTime.TotalMilliseconds);
        }
    }
}
