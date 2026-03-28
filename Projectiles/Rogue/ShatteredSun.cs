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
    public class ShatteredSun : ModProjectile
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
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.tileCollide = false;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 15;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}

		public override void AI()
		{
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= 1.57f;
			}
			Projectile.ai[1] += 1f;
			if (Projectile.ai[1] < 5f)
			{
				Projectile.alpha -= 50;
			}
			if (Projectile.ai[1] == 5f)
			{
				Projectile.alpha = 0;
				Projectile.tileCollide = false;
			}

			if (Projectile.ai[1] == 12f)
			{
				int numProj = 2;
				float rotation = MathHelper.ToRadians(10);
				if (Projectile.owner == Main.myPlayer)
				{
					for (int i = 0; i < numProj; i++)
					{
						Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProj - 1)));
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("ShatteredSun2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					}
					Projectile.active = false;
				}
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
        	SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        	for (int k = 0; k < 5; k++)
            {
            	Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 246, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
    }
}