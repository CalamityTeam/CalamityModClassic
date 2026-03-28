using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class OrichalcumSpikedGemstoneProjectile : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Gemstone");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.aiStyle = 2;
            Projectile.penetrate = 6;
            Projectile.timeLeft = 600;
            AIType = 48;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	Vector2 velocity = Projectile.velocity;
            if (Projectile.velocity.Y != velocity.Y && (velocity.Y < -3f || velocity.Y > 3f))
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
			}
        	if (Projectile.velocity.X != velocity.X)
			{
				Projectile.velocity.X = velocity.X * -0.5f;
			}
			if (Projectile.velocity.Y != velocity.Y && velocity.Y > 1f)
			{
				Projectile.velocity.Y = velocity.Y * -0.5f;
			}
            return false;
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem(Projectile.GetSource_FromThis(null), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("OrichalcumSpikedGemstone").Type);
        	}
        }
    }
}