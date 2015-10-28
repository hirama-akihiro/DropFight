using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.CharactorSelect
{
    class CharactorTypeSelecter : CharactirSelectParts
    {
        private readonly Texture2D humanTexture;
        private readonly Texture2D cpuTexture;
        private Texture2D nowTexture;
        private readonly PlayerIndex playerIndex;
        public CharactorTypeSelecter(Rectangle drawRect, Texture2D humanTexture, Texture2D cpuTexture, PlayerIndex playerIndex)
            :base(drawRect)
        {
            this.humanTexture = humanTexture;
            this.cpuTexture = cpuTexture;
            this.playerIndex = playerIndex;
            nowTexture = cpuTexture;
        }
        public override void Update(GameTime gameTime, CharactorSelectDatas data)
        {
            //現在の状態をテクスチャに反映
            switch(data.getPlayerType(playerIndex)){
                case PlayerType.HUMAN:
                    nowTexture=humanTexture;
                    break;
                case PlayerType.CPU:
                    nowTexture=cpuTexture;
                    break;
            }
            return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(nowTexture, drawRect, Color.White);
        }

        public override void Click(PlayerIndex clickPlayer, CharactorSelectDatas data)
        {
            switch (data.getPlayerType(playerIndex))
            {
                case PlayerType.HUMAN:
                    data.setPlayerType(playerIndex, PlayerType.CPU);
                    break;
                case PlayerType.CPU:
                    data.setPlayerType(playerIndex, PlayerType.HUMAN);
                    break;
            }
        }
    }

}
