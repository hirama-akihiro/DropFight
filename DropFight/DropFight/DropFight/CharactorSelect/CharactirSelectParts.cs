using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace DropFight.CharactorSelect
{
    abstract class CharactirSelectParts
    {
        protected Rectangle drawRect;
        protected CharactirSelectParts(Rectangle drawRect)
        {
            this.drawRect = drawRect;
        }
        public abstract void Update(GameTime gameTime,CharactorSelectDatas data);
        public abstract void Draw(SpriteBatch spriteBatch);
        public Boolean IsClick(Point pt)
        {
            return drawRect.Contains(pt);
        }
        public abstract void Click(PlayerIndex Clickplayer, CharactorSelectDatas data);
    }
}
