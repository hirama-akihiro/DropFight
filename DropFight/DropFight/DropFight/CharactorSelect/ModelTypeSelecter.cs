using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using DropFight.ModelUtils;

namespace DropFight.CharactorSelect
{
    class ModelTypeSelecter : CharactirSelectParts
    {
        private Stopwatch stopwatch = new Stopwatch(); 
        private Texture2D texture;
        private ModelSelectType modelSelctType;
        private PlayerIndex playerID;
        public ModelTypeSelecter(Rectangle drawRect, Texture2D texture, ModelSelectType modelSelctType, PlayerIndex playerID) 
            :base(drawRect)
        {
            this.texture = texture;
            this.modelSelctType = modelSelctType;
            this.playerID = playerID;
            stopwatch.Start();
        }

        public override void Update(GameTime gameTime,CharactorSelectDatas data)
        {
            return;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawRect, Color.White);
        }

        public override void Click(PlayerIndex clickPlayer, CharactorSelectDatas data)
        {
            if (stopwatch.ElapsedMilliseconds < 500)
            {
                return;
            }
            stopwatch.Restart();
#warning playerIDの判別処理をそのうち追加
            
            switch (modelSelctType)
            {
                case ModelSelectType.RIGHT:
                    switch(data.getModelType(playerID)){
                        case ModelType.Inoshishi:
                            data.setModelType(playerID, ModelType.Legend);
                            break;
                        case ModelType.Legend:
                            data.setModelType(playerID, ModelType.Metall);
                            break;
                        case ModelType.Metall:
                            data.setModelType(playerID, ModelType.SnowMan);
                            break;
                        case ModelType.SnowMan:
                            data.setModelType(playerID, ModelType.Inoshishi);
                            break;
                    }
                    break;
                case ModelSelectType.LEFT: 
                    switch (data.getModelType(playerID))
                    {
                        case ModelType.Inoshishi:
                            data.setModelType(playerID, ModelType.SnowMan);
                            break;
                        case ModelType.Legend:
                            data.setModelType(playerID, ModelType.Inoshishi);
                            break;
                        case ModelType.Metall:
                            data.setModelType(playerID, ModelType.Legend);
                            break;
                        case ModelType.SnowMan:
                            data.setModelType(playerID, ModelType.Metall);
                            break;
                    }
                    break;
            }
        }
        public enum ModelSelectType
        {
            RIGHT,LEFT
        }
    }
}
