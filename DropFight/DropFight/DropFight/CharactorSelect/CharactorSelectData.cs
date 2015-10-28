using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using DropFight.ModelUtils;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace DropFight.CharactorSelect
{

    public class CharactorSelectDatas
    {
        private ContentManager content;
        private Dictionary<PlayerIndex, PlayerModel> models;
        private Dictionary<PlayerIndex, PlayerType> types;
        private Dictionary<PlayerIndex, ModelColor> modelColors;
        private Dictionary<PlayerIndex, ModelType> modelTypes;
        private Dictionary<PlayerIndex, Boolean> charactorConflict;

        /// <summary>
        /// 初期設定を適当に作る
        /// </summary>
        public CharactorSelectDatas(ContentManager content)
        {
            models = new Dictionary<PlayerIndex, PlayerModel>();
            types = new Dictionary<PlayerIndex, PlayerType>();
            modelColors = new Dictionary<PlayerIndex, ModelColor>();
            modelTypes = new Dictionary<PlayerIndex, ModelType>();
            charactorConflict = new Dictionary<PlayerIndex, bool>();
            this.content = content;

            setModelTypeAndColor(PlayerIndex.One, ModelType.SnowMan, ModelColor.RED);
            setModelTypeAndColor(PlayerIndex.Two, ModelType.Inoshishi, ModelColor.BLUE);
            setModelTypeAndColor(PlayerIndex.Three, ModelType.Legend, ModelColor.GREEN);
            setModelTypeAndColor(PlayerIndex.Four, ModelType.Metall, ModelColor.ORANGE);

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                types[index] = PlayerType.CPU;
            }
        }
        
        /// <summary>
        /// 初期設定用。
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="color"></param>
        private void setModelTypeAndColor(PlayerIndex index,ModelType type,ModelColor color){

            modelTypes[index] = type;
            setModelColor(index, color);
        }

        /// <summary>
        /// プレイヤーの色を変える
        /// 保持しているモデルも変わります
        /// </summary>
        /// <param name="index"></param>
        public void setModelColor(PlayerIndex index, ModelColor modelcolor)
        {
            modelColors[index] = modelcolor;
            ModelType type;
            modelTypes.TryGetValue(index, out type);
            models[index] = PlayerModelGenerator.getModel(content, type, modelcolor);
            foreach (PlayerIndex playerIndex in Enum.GetValues(typeof(PlayerIndex)))
            {
                charactorConflict[playerIndex] = isConflict(playerIndex);
            }
        }

        public bool IsConflict
        {
            get
            {
                foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
                {
                    if(isConflict(index))return true;
                }
                return false;
            }
        }

        /// <summary>
        /// プレイヤーのモデルタイプを変える(イノシシ、レジェントetc)
        /// 保持しているモデルも変わります
        /// </summary>
        /// <param name="index"></param>
        public void setModelType(PlayerIndex index, ModelType modeltype)
        {
            modelTypes[index] = modeltype;
            ModelColor color;
            modelColors.TryGetValue(index, out color);
            models[index] = PlayerModelGenerator.getModel(content, modeltype, color);

            foreach (PlayerIndex playerIndex in Enum.GetValues(typeof(PlayerIndex)))
            {
                charactorConflict[playerIndex] = isConflict(playerIndex);
            }
        
        }

        /// <summary>
        /// Animationモデル自体を取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PlayerModel getModel(PlayerIndex index)
        {
            PlayerModel model;
            models.TryGetValue(index, out model);
            return model;
        }

        /// <summary>
        /// モデルの種類を取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ModelType getModelType(PlayerIndex index)
        {
            ModelType modelType;
            modelTypes.TryGetValue(index, out modelType);
            return modelType;
        }

        /// <summary>
        /// プレイヤーのモデルが被っているか
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool getConflict(PlayerIndex index)
        {
            Boolean result;
            charactorConflict.TryGetValue(index, out result);
            return result;
        }

        /// <summary>
        /// モデルの色を取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ModelColor getModelColor(PlayerIndex index)
        {
            ModelColor modelColor;
            modelColors.TryGetValue(index, out modelColor);
            return modelColor;
        }

        /// <summary>
        /// プレイヤーの種類（Human、CPU）を取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PlayerType getPlayerType(PlayerIndex index)
        {
            PlayerType type;
            types.TryGetValue(index, out type);
            return type;
        }
        
        /// <summary>
        /// プレイヤーの種類（Human、CPU）をセット
        /// </summary>
        /// <param name="index"></param>
        /// <param name="type"></param>
        public void setPlayerType(PlayerIndex index, PlayerType type)
        {
            types[index]= type;
        }

        /// <summary>
        /// Modelをセット
        /// </summary>
        /// <param name="index"></param>
        /// <param name="model"></param>
        private void setModel(PlayerIndex index, PlayerModel model)
        {
            models[index]= model;
        }


        /// <summary>
        /// 設定は終わっているか
        /// </summary>
        public bool SettingEnd
        {
            get;
            set;
        }
        
        /// <summary>
        /// 色とキャラクターがかぶっているか
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool isConflict(PlayerIndex index){
            foreach (PlayerIndex playerIndex in Enum.GetValues(typeof(PlayerIndex)))
            {
                if (index != playerIndex && getModelType(index) == getModelType(playerIndex) && getModelColor(index) == getModelColor(playerIndex))
                {
                    //Boolean conflict;
                    //charactorConflict.TryGetValue(playerIndex, out conflict);
                    //if (!conflict)
                    //{
                    return true;
                    //}
                }
            }
            return false;
        }
    }
    public enum PlayerType
    {
        HUMAN, CPU
    }

}
