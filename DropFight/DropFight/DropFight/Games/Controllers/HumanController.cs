using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DropFight.Games.Controllers
{
    public class HumanController : Controller
    {
        public HumanController(PlayerIndex playerIndex)
            :base(playerIndex)
        {
        }

        public override void Update(GameInfo gameInfo, Input input)
        {
            base.Update(gameInfo, input);
            if (input.PushABXY(PlayerIndex))
            {
                Attack();
            }
            Vector2 leftStick = input.LeftStick(PlayerIndex);
            if (Math.Abs(leftStick.X) < 0.5 && leftStick.Y > 0.5)
            {
                Move(Direction.UP);
            }
            else if (Math.Abs(leftStick.X) < 0.5 && leftStick.Y < -0.5)
            {
                Move(Direction.DOWN);
            }
            else if (leftStick.X > 0.5 && Math.Abs(leftStick.Y) < 0.5)
            {
                Move(Direction.RIGHT);
            }
            else if (leftStick.X < -0.5 && Math.Abs(leftStick.Y) < 0.5)
            {
                Move(Direction.LEFT);
            }

#warning デバッグ用 1Pだけキーボードで操作できる
            #region キーボード操作
            if (PlayerIndex == PlayerIndex.One && !input.GamePadConnect(PlayerIndex.One))
            {
                if (input.PushKey(Keys.Z))
                {
                    Attack();
                }
                if (input.PushKey(Keys.Down))
                {
                    Move(Direction.DOWN);
                }
                if (input.PushKey(Keys.Right))
                {
                    Move(Direction.RIGHT);
                }
                if (input.PushKey(Keys.Up))
                {
                    Move(Direction.UP);
                }
                if (input.PushKey(Keys.Left))
                {
                    Move(Direction.LEFT);
                }
            }
            #endregion
        }
    }
}
