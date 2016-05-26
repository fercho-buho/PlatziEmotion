using EmotionPlatzi.web.Models;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;

namespace EmotionPlatzi.web.Util
{
    public class EmotionHelper
    {
        public EmotionServiceClient emoClient;
        public EmotionHelper(string key)
        {
            emoClient = new EmotionServiceClient(key); 
        }
        //Detectar rostros y estraer rostros
        public async Task<EmoPicture> DetectAndExtractFacesAsync(Stream imageStream) {

            Emotion[] emotions = await emoClient.RecognizeAsync(imageStream);
            var emoPicture = new EmoPicture();
            emoPicture.Faces = ExtractFaces(emotions, emoPicture);
            return emoPicture; 
        }
        //emite notificaciones cada vez que se alteran sus miembros
        private ObservableCollection<EmoFace> ExtractFaces(Emotion[] emotions, EmoPicture emoPicture)
        {
            var listaFaces = new ObservableCollection<EmoFace>();
            foreach (var emotion in emotions)
            {
                var emoface = new EmoFace()
                {
                    X=emotion.FaceRectangle.Left,
                    Y=emotion.FaceRectangle.Top,
                    Width=emotion.FaceRectangle.Width,
                    Height=emotion.FaceRectangle.Height,
                    Picture=emoPicture      
                };
                emoface.Emotions = ProcessEmotions(emotion.Scores,emoface );
                listaFaces.Add(emoface); 
            }
            return listaFaces; 
        }

        private ObservableCollection<EmoEmotion> ProcessEmotions(Scores scores, EmoFace emoface)
        {
            var emotionList = new ObservableCollection<EmoEmotion>();
                                                            //Prop publicas o instanciables (no estaticos)    
            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance );
            //Sólo propiedades de tipo float
            //Cualquier propiedad p
            var filterProperties = properties.Where(p => p.PropertyType==typeof(float));
            //   = from p in properties where p.PropertyType==typeof(float) select p; 
            var emoType = EmoEmotionEnum.Undetermined;
            foreach (var prop in filterProperties ) {
                //
                if (!Enum.TryParse<EmoEmotionEnum>(prop.Name, out emoType))
                    emoType = EmoEmotionEnum.Undetermined;
                var emoEmotion = new EmoEmotion();
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EmotionType = emoType;
                emoEmotion.Face = emoface;
                emotionList.Add(emoEmotion); 
            }
            return emotionList;
        }
    }
}
