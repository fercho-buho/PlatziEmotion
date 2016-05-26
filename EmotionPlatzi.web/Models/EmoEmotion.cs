using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionPlatzi.web.Models
{
    public class EmoEmotion
    {
        public int Id { get; set; }
        public float Score { get; set; }
        //A que rostro pertenece
        public int EmoFaceId { get; set; }
        public EmoEmotionEnum EmotionType { get; set; }

        //Guardar el rostro y no volver a consultar el EmoFaceId
        //Navegacion
        public virtual EmoFace Face { get; set; }
    }
}
