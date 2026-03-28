using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class TriactisOPHammer : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hammer");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
        
        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.05f) / 255f, ((255 - Projectile.alpha) * 0.5f) / 255f, ((255 - Projectile.alpha) * 0.75f) / 255f);
        	if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 8;
				SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
			}
        	if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[1] += 1f;
				if (Projectile.ai[1] >= 20f)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
					Projectile.netUpdate = true;
				}
        	}
        	else
			{
				Projectile.tileCollide = false;
				float num42 = 20f;
				float num43 = 5f;
				Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num44 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector2.X;
				float num45 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector2.Y;
				float num46 = (float)Math.Sqrt((double)(num44 * num44 + num45 * num45));
				if (num46 > 3000f)
				{
					Projectile.Kill();
				}
				num46 = num42 / num46;
				num44 *= num46;
				num45 *= num46;
				if (Projectile.velocity.X < num44)
				{
					Projectile.velocity.X = Projectile.velocity.X + num43;
					if (Projectile.velocity.X < 0f && num44 > 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X + num43;
					}
				}
				else if (Projectile.velocity.X > num44)
				{
					Projectile.velocity.X = Projectile.velocity.X - num43;
					if (Projectile.velocity.X > 0f && num44 < 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X - num43;
					}
				}
				if (Projectile.velocity.Y < num45)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + num43;
					if (Projectile.velocity.Y < 0f && num45 > 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num43;
					}
				}
				else if (Projectile.velocity.Y > num45)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - num43;
					if (Projectile.velocity.Y > 0f && num45 < 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num43;
					}
				}
				if (Main.myPlayer == Projectile.owner)
				{
					Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
					Rectangle value2 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
					if (rectangle.Intersects(value2))
					{
						Projectile.Kill();
					}
				}
        	}
        	Projectile.rotation += 0.5f;
			return;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(250, 250, 250, 50);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	if (Projectile.owner == Main.myPlayer)
        	{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("MageHammerBoom").Type, (int)((double)Projectile.damage * 0.25), Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        	SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 60;
			Projectile.height = 60;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
			for (int num621 = 0; num621 < 40; num621++)
			{
				int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Main.rand.Next(2) == 0 ? 89 : 229, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 70; num623++)
			{
				int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Main.rand.Next(2) == 0 ? 89 : 229, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, Main.rand.Next(2) == 0 ? 89 : 229, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
        }
    }
}