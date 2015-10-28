using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DropFight.Games.Players;

namespace DropFight.Games.Controllers
{
#warning AI作成時のひな形に使ってください。最終的には消します。
    class HinagataAI : Controller
    {
        public HinagataAI(PlayerIndex playerIndex)
            : base(playerIndex)
        {

        }
        /// <summary>
        /// これがAIの動作の処理です。
        /// gameInfoを用いて1フレームに与える指示を出してください。
        /// HumanControllerも全く同じ仕組です
        /// </summary>
        /// <param name="gameInfo"></param>
        /// <param name="input"></param>
        public override void Update(GameInfo gameInfo, Input input)
        {
            //最初に必ずbaseのUpdateを呼び出してください
            base.Update(gameInfo, input);

            //攻撃する。
            Attack();
            //移動する(同じフレームでAttackが呼ばれているときはAttackが無条件で優先されます)
            //複数回呼んだ場合最後のMoveが使われます。
            Move(Direction.UP);

            //gameInfoから自分のPlayerInfoを取得
            PlayerInfo myInfo;
            foreach (PlayerInfo info in gameInfo.PlayerInfos)
            {
                if (info.Index == PlayerIndex)
                {
                    myInfo = info;
                }
            }


            //おまけ：PlayerIndexをintにしたい！(キャラごとにユニークな値が取れるので乱数のシードなどに使えます)
            int index = PlayerIndex - PlayerIndex.One;
        }
    }
}
