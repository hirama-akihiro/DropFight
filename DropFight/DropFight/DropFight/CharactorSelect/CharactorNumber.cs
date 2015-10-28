using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.CharactorSelect
{
	class CharactorNumber : CharactirSelectParts
	{
		private readonly Texture2D texture;
		private Texture2D nowTexture;
		private readonly PlayerIndex playerIndex;

		public CharactorNumber(Rectangle drawRect, Texture2D humanTexture, PlayerIndex playerIndex)
            :base(drawRect)
        {
            this.texture = humanTexture;
            this.playerIndex = playerIndex;
        }

		public override void Update(GameTime gameTime, CharactorSelectDatas data)
		{

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, drawRect, Color.White);
		}

		public override void Click(PlayerIndex clickPlayer, CharactorSelectDatas data)
		{

		}
	}
}
