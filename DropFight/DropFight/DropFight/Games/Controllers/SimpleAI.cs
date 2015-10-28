using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropFight.Games.Players;
using Microsoft.Xna.Framework;

namespace DropFight.Games.Controllers
{
    class SimpleAI : Controller
    {
        private Direction nowDirection;
        private PlayerInfo me;
        private RandomFactory randomFactory = new RandomFactory();
        private double preActionTime;
        public SimpleAI(PlayerIndex playerIndex)
            : base(playerIndex)
        {
            this.randomFactory = new RandomFactory();
            this.preActionTime = 0;
        }

        public override void Update(Games.GameInfo gameInfo, Input input)
        {
            base.Update(gameInfo, input);

            //--- 自分のプレイヤー情報を更新 ---//
            foreach (PlayerInfo player in gameInfo.PlayerInfos)
            {
                if (PlayerIndex == player.Index)
                {
                    me = player;
                    break;
                }
            }

            //--- ルールベースで命令を記述 ---//
            foreach (PlayerInfo player in gameInfo.PlayerInfos)
            {
                // 自分の情報はスキップ
                if (player.Equals(me)) continue;
                // 敵の進行方向に自分がいるか
                if (isLookingThisWay(player))
                {
                    runAwayFrom(player);
                    break;
                }
                else
                {
                    // 自分の進行方向に敵がいるか
                    if (isInADirection(player))
                    {
                        Attack();
                        break;
                    }
                    else
                    {
                        idle(gameInfo);
                    }
                }
            }
        }

        /// <summary>
        /// プレイヤーの進行方向に自分がいるか
        /// </summary>
        /// <param name="player">プレイヤー情報</param>
        /// <returns></returns>
        Boolean isLookingThisWay(PlayerInfo player)
        {
            // 相手が死んでいたらスキップ
            if (player.IsDead) return false;
            // 敵から見た自分のいる方向
            Vector3 dir = me.Position - player.Position;
            dir = new Vector3((float)Math.Round(dir.X), (float)Math.Round(dir.Y), (float)Math.Round(dir.Z));
            dir.Normalize();
            if (dir == player.Direction) return true;
            return false;
        }

        /// <summary>
        /// 自分の進行方向にプレイヤーがいるか
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        Boolean isInADirection(PlayerInfo player)
        {
            // 相手が死んでいたらスキップ
            if (player.IsDead) return false;
            // 自分から見た敵のいる方向
            Vector3 dir = player.Position - me.Position;
            dir = new Vector3((float)Math.Round(dir.X), (float)Math.Round(dir.Y), (float)Math.Round(dir.Z));
            dir.Normalize();
            if (dir == me.Direction) return true;
            return false;
        }

        // プレイヤーの攻撃圏内から逃げる
        void runAwayFrom(PlayerInfo player)
        {
            //--- 敵の向きに対して時計回りの方向へ垂直に逃げる ---//
            // 敵の向き:上
            if (player.Direction == new Vector3(0, 0, -1)) Move(Direction.RIGHT);
            // 敵の向き:下
            else if (player.Direction == new Vector3(0, 0, 1)) Move(Direction.LEFT);
            // 敵の向き:左
            if (player.Direction == new Vector3(-1, 0, 0)) Move(Direction.UP);
            // 敵の向き:右
            if (player.Direction == new Vector3(1, 0, 0)) Move(Direction.DOWN);
        }

        // ランダムに移動
        void idle(GameInfo gameInfo)
        {
            if (gameInfo.GameTime.TotalGameTime.TotalMilliseconds - preActionTime > 200)
            {
                Random rnd = new Random((PlayerIndex - PlayerIndex.One)*100 + Environment.TickCount);
                int act = rnd.Next(100);
                act /= 25;
                switch (act)
                {
                    case 0:
                        nowDirection=Direction.UP;
                        break;
                    case 1:
                        nowDirection=Direction.RIGHT;
                        break;
                    case 2:
                        nowDirection=Direction.LEFT;
                        break;
                    case 3:
                        nowDirection=Direction.DOWN;
                        break;
                }
                preActionTime = gameInfo.GameTime.TotalGameTime.TotalMilliseconds;
            }

            Move(nowDirection);
            return;
        }

    }
}
