using System.Collections.Generic;
using DropFight.Games.Players;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DropFight.Games.Indicator
{
    public class IndicatorManager
    {
        private Timer timer;
        private LinkedList<NameDrawer> nameDrawers = new LinkedList<NameDrawer>();
        private LinkedList<StockDrawer> stockDrawers = new LinkedList<StockDrawer>();

        public IndicatorManager(PlayerManager playerManager, ContentManager content)
        {
            foreach (Player player in playerManager.Players)
            {
                nameDrawers.AddFirst(new NameDrawer(player, content));
                Texture2D barTexture = content.Load<Texture2D>("Scene/Game/stockbar1p");
                Texture2D stockTexture = content.Load<Texture2D>("Scene/Game/stock1p");
                if (stockDrawers.Count == 0)
                {
                    barTexture = content.Load<Texture2D>("Scene/Game/stockbar1p");
                    stockTexture = content.Load<Texture2D>("Scene/Game/stock1p");
                }
                if (stockDrawers.Count == 1)
                {
                    barTexture = content.Load<Texture2D>("Scene/Game/stockbar2p");
                    stockTexture = content.Load<Texture2D>("Scene/Game/stock2p");
                }
                if (stockDrawers.Count == 2)
                {
                    barTexture = content.Load<Texture2D>("Scene/Game/stockbar3p");
                    stockTexture = content.Load<Texture2D>("Scene/Game/stock3p");
                }
                if (stockDrawers.Count == 3)
                {
                    barTexture = content.Load<Texture2D>("Scene/Game/stockbar4p");
                    stockTexture = content.Load<Texture2D>("Scene/Game/stock4p");
                }

                stockDrawers.AddFirst(new StockDrawer(player, stockDrawers.Count, barTexture, stockTexture));
            }
            timer = new Timer(content);
        }
        public void Update(GameInfo gameInfo)
        {
            foreach (StockDrawer stackDrawer in stockDrawers)
            {
                stackDrawer.Update();
            }
            timer.Update(gameInfo);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (NameDrawer nameDrawer in nameDrawers)
            {
                nameDrawer.Draw(spriteBatch, camera);
            }

            foreach (StockDrawer stackDrawer in stockDrawers)
            {
                stackDrawer.Draw(spriteBatch);
            }

            timer.Draw(spriteBatch);
        }
    }
}
