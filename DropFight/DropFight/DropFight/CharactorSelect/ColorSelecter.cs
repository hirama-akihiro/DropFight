using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using DropFight.ModelUtils;

namespace DropFight.CharactorSelect
{
    class ColorSelecter : CharactirSelectParts
    {
        private ModelColor color;
        private Texture2D previewColor;
        private PlayerIndex playerID;
        public ColorSelecter(Rectangle drawRect, Texture2D previewColor, ModelColor modelColor, PlayerIndex playerID) 
            :base(drawRect)
        {
            this.previewColor = previewColor;
            color = modelColor;
            this.playerID = playerID;
        }

        public override void Update(GameTime gameTime,CharactorSelectDatas data)
        {
            return ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(previewColor, drawRect, Color.White);
        }

        public override void Click(PlayerIndex clickPlayer, CharactorSelectDatas data)
        {
            data.setModelColor(playerID, color);
        }
    }
}
