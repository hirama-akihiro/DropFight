using System;
using System.Collections.Generic;
using DropFight.Games.Attacks;
using DropFight.Games.Blocks;
using DropFight.Games.Players;
using DropFight.Scenes;
using Microsoft.Xna.Framework;

namespace DropFight.Games
{
    /// <summary>
    /// ゲームの情報
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// ゲーム中かどうか
        /// </summary>
        public readonly bool IsRunning;

        /// <summary>
        /// ゲームが終了したかどうか
        /// </summary>
        public readonly bool HasFinished;

        /// <summary>
        /// ゲームの制限時間
        /// </summary>
        public readonly TimeSpan LimitTime;

        /// <summary>
        /// ゲームが始まってからの時間
        /// </summary>
        public readonly TimeSpan CurrentTime;

        /// <summary>
        /// ゲームの残り時間
        /// </summary>
        public TimeSpan RestTime
        {
            get
            {
                return LimitTime - CurrentTime;
            }
        }

        /// <summary>
        /// ゲーム内部の時間
        /// </summary>
        public readonly GameTime GameTime;

        /// <summary>
        /// 攻撃の情報の列挙子
        /// </summary>
        public IEnumerable<AttackInfo> AttackInfos
        {
            get;
            private set;
        }

        /// <summary>
        /// フィールドの情報の列挙子
        /// </summary>
        public readonly FieldInfo FieldInfo;

        /// <summary>
        /// プレイヤーの情報の列挙子
        /// </summary>
        public IEnumerable<PlayerInfo> PlayerInfos
        {
            get;
            private set;
        }

        /// <param name="gameScene">ゲーム場面</param>
        /// <param name="gameTime">ゲーム内部の時間</param>
        public GameInfo(GameScene gameScene, GameTime gameTime)
        {
            this.IsRunning = gameScene.IsRunning;
            this.HasFinished = gameScene.HasFinished;
            this.LimitTime = gameScene.LimitTime;
            this.CurrentTime = gameScene.CurrentTime;
            this.GameTime = gameTime;

            ManagerSet managerSet = gameScene.ManagerSet;
            AttackManager attackManager = managerSet.AttackManager;
            AttackInfos = attackManager.Info;

            Field field = managerSet.Field;
            FieldInfo = field.Info;

            PlayerManager playerManager = managerSet.PlayerManager;
            PlayerInfos = playerManager.Info;
        }
    }
}
