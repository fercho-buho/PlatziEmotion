using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace EmotionPlatzi.web.Models
{
    public class EmotionPlatziwebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        //public EmotionPlatziwebContext() : base("name=EmotionPlatziwebContext")
        public EmotionPlatziwebContext() : base("name=EmotionPlatziAzure")            
        {
            //Especialmente en etapa de desarrollo
            Database.SetInitializer<EmotionPlatziwebContext>(new DropCreateDatabaseIfModelChanges<EmotionPlatziwebContext >());
            //Verificar activando las migraciones( contiene Configuration.cs)
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmotionPlatziwebContext, Configuration>());

        }

        public DbSet<EmotionPlatzi.web.Models.EmoPicture> EmoPictures { get; set; }
        public DbSet<EmotionPlatzi.web.Models.EmoFace> EmoFaces { get; set; }
        public DbSet<EmotionPlatzi.web.Models.EmoEmotion> EmoEmotions { get; set; }
        public DbSet<EmotionPlatzi.web.Models.Home> Homes { get; set; }        
    }
}
