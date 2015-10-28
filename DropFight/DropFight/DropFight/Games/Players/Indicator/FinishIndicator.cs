using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;

namespace DropFight.Games.Indicator
{
    /// <summary>
    /// ゲームの終了を知らせる
    /// </summary>
    public class FinishIndicator
    {
        private Texture2D texture;

        private Stopwatch stopwatch = new Stopwatch();

        public bool IsRunning
        {
            get
            {
                return stopwatch.IsRunning;
            }
        }

        private static readonly TimeSpan minTime = new TimeSpan(0, 0, 3);

        public FinishIndicator(ContentManager content)
        {
            texture = content.Load<Texture2D>("Scene/Game/Finish");
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public bool Update()
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }

            return stopwatch.Elapsed < minTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsRunning)
            {
                return;
            }

            GraphicsDevice graphicsDevice = spriteBatch.GraphicsDevice;
            Viewport viewport = graphicsDevice.Viewport;
            int screenWidth = viewport.Width;
            int screenHeight = viewport.Height;
            spriteBatch.Draw(texture, new Vector2((screenWidth - texture.Width) / 2, (screenHeight - texture.Height) / 2), Color.White);
        }
    }
}
