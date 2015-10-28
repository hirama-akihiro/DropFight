using System;
using DropFight.Games.Players;
using DropFight.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games.Indicator
{
    class StockDrawer
    {
        private Boolean isDead = false;
        private uint stock;
        private Player player;
        private Texture2D backTexture;
        private Texture2D stockTexture;
        private readonly Vector2 drawPosition;


        public StockDrawer(Player player, int drawNum, Texture2D backTexture, Texture2D stockTexture)
        {
            this.player = player;
            this.backTexture = backTexture;
            this.stockTexture = stockTexture;
            drawPosition = new Vector2((drawNum * 1600/4), 0.0f);
        }

        public void Update()
        {
            stock = player.Info.Stock;
            isDead = player.IsDead;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isDead == true) return;

            spriteBatch.ScalingDraw(backTexture, new Vector2(drawPosition.X, drawPosition.Y), 1.0f, Color.White);
            for (int i = 0; i < stock; i++)
            {
                spriteBatch.ScalingDraw(stockTexture, new Vector2(drawPosition.X + i * (stockTexture.Width) + 150, drawPosition.Y + 25), 1.0f, Color.White);
            }
        }

    }
}
