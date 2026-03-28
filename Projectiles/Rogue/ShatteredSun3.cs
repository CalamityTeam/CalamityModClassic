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
    public class ShatteredSun3 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Shattered Sun");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= 1.57f;
			}
			float centerX = Projectile.Center.X;
			float centerY = Projectile.Center.Y;
			float num474 = 750f;
			bool homeIn = false;
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
				{
					float num476 = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
					float num477 = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
					float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
					if (num478 < num474)
					{
						num474 = num478;
						centerX = num476;
						centerY = num477;
						homeIn = true;
					}
				}
			}
			if (homeIn)
			{
				float num483 = 20f;
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num484 = centerX - vector35.X;
				float num485 = centerY - vector35.Y;
				float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 15f + num484) / 16f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 15f + num485) / 16f;
			}
		}
        
		public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
		
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShatteredExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShatteredExplosion").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			return true;
		}
        
        public override void OnKill(int timeLeft)
        {
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 246, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
    }
}