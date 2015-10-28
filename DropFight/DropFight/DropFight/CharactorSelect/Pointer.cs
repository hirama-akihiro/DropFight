using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DropFight.CharactorSelect
{
    class Pointer
    {

        PlayerIndex playerIndex;
        Texture2D texture;
        Rectangle rect;
        public bool IsEnable
        {
            private get;
            set;
        }

        public Pointer(Rectangle rect, PlayerIndex playerIndex, Texture2D texture)
        {
            IsEnable = true;
            this.rect = rect;
            this.playerIndex = playerIndex;
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsEnable) return;
            spriteBatch.Draw(texture, rect, Color.White);
        }

        public Point Position
        {
            get { return new Point(rect.X,rect.Y); }
            set { 
                rect.X = value.X;
                rect.Y = value.Y;
            }
        }
        public PlayerIndex PointerPlayerIndex
        {
            get { return this.playerIndex; }
        }
    }
    
}
