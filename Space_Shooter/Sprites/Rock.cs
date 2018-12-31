using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceShooterRevamped.Sprites
{
    public class Rock : Sprite
    {

        public Rock(Texture2D texture)
            :base(texture)
        {
            Position = new Vector2(Game1.Random.Next(0, Game1.ScreenWidth-_texture.Width), 
                                   Game1.Random.Next(0, Game1.ScreenHeight));
            LinearVelocity = Game1.Random.Next(3, 6);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach(var sprite in sprites)
            {
                if (sprite == this) continue;
                if (sprite.Rectangle.Intersects(this.Rectangle) && sprite.IsLethal)
                {
                    this.IsRemoved = true;
                }
            }
        }
    }
}
