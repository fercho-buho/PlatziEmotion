using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionPlatzi.web.Models
{
    public class EmoFace
    {
        public int Id { get; set; }
        //Este rostro a quien pertenece
        public int EmoPictureId { get; set; }
        #region
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        //Cual es la foto que pertenece este rostro
        //Para no volver hacer la consulta por medio del EmoPictureId
        public virtual EmoPicture Picture { get; set; }
        // Este rostro puede tener muchas emosiones

        public virtual ObservableCollection<EmoEmotion> Emotions
        {
            get; set;
        }
    }
}