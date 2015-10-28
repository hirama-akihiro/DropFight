using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DropFight.CharactorSelect
{
    class OKButton : CharactirSelectParts
    {
        private Texture2D texture;
        public OKButton(Rectangle drawRect, Texture2D texture)
            : base(drawRect)
        {
            this.texture = texture;
        }
        
        public override void Update(GameTime gameTime, CharactorSelectDatas data)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawRect, Color.White);
        }

        public override void Click(PlayerIndex Clickplayer, CharactorSelectDatas data)
        {
            //モデルと色がかぶってなければ
            if(!data.IsConflict)data.SettingEnd = true;
        }
    }
}
