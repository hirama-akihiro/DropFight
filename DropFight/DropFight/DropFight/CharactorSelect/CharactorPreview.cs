using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using DropFight.ModelUtils;

namespace DropFight.CharactorSelect
{
    class CharactorPreview : CharactirSelectParts
    {
        private PlayerIndex playerID;
        private PlayerModel model;
        private Camera camera;
        private Texture2D background;
        private Texture2D ngTexture;
        private ModelInfo info;
        private Rectangle rectangle;
        private PlayerIndex playerIndex;
        private bool isConflict;
        
        public CharactorPreview(Rectangle drawRect, Texture2D background, Texture2D ngTexture, PlayerIndex playerID)
            : base(drawRect)
        {
            this.drawRect = drawRect;
            this.playerID = playerID;
            this.camera = new Camera();

            camera.FieldOfViewRadian = MathHelper.PiOver4;
            camera.Target = new Vector3(0, 0, -10);
            camera.Position = new Vector3(0.0f, 3, 6);
            this.background = background;
            this.ngTexture = ngTexture;
            info = new ModelInfo();
            isConflict = false;
        }

        public override void Update(GameTime gameTime, CharactorSelectDatas data)
        {
            if (!data.getModel(playerID).Equals(model))
            {
                model = data.getModel(playerID);
            }
            model.ChangeMotion(DropFight.ModelUtils.PlayerModel.ModelAnimation.WALK);
            model.Update(gameTime);
            isConflict = data.getConflict(playerID);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, drawRect, Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            GraphicsDevice g = spriteBatch.GraphicsDevice;
            Viewport defaultViewport = g.Viewport;
            Viewport drawViewport = defaultViewport;
            drawViewport.X = drawRect.X;
            drawViewport.Y = drawRect.Y;
            drawViewport.Width = drawRect.Width;
            drawViewport.Height = drawRect.Height;
            g.Viewport = drawViewport;

            GraphicsDevice graphics = spriteBatch.GraphicsDevice;
            graphics.DepthStencilState = DepthStencilState.Default;

            if (model != null)
            {
                model.Draw(camera, info);
            }
            g.Viewport = defaultViewport;
            if (isConflict)
            {
                spriteBatch.Draw(ngTexture, drawRect, Color.White);
            }
        }

        public override void Click(PlayerIndex Clickplayer, CharactorSelectDatas data)
        {
        }
    }
}
