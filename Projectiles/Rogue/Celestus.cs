using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Rogue
{
    public class Celestus : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Celestus");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 94;
            Projectile.height = 94;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().rogue = true;
		}
        
        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((Main.DiscoR - Projectile.alpha) * 0.5f) / 255f, ((Main.DiscoG - Projectile.alpha) * 0.5f) / 255f, ((Main.DiscoB - Projectile.alpha) * 0.5f) / 255f);
        	if (Projectile.soundDelay == 0)
			{
				Projectile.soundDelay = 8;
				SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
			}
        	if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[1] += 1f;
				if (Projectile.ai[1] >= 40f)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
					Projectile.netUpdate = true;
				}
        	}
        	else
			{
				float num42 = 25f;
				float num43 = 5f;
				Vector2 vector2 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num44 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector2.X;
				float num45 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector2.Y;
				float num46 = (float)Math.Sqrt((double)(num44 * num44 + num45 * num45));
				if (num46 > 4000f)
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
        	Projectile.rotation += 1f;
			return;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(250, 250, 250, 50);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(30) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("ExoFreeze").Type, 240);
            }
            target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 100);
            target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 100);
            target.AddBuff(BuffID.CursedInferno, 100);
            target.AddBuff(BuffID.Frostburn, 100);
            target.AddBuff(BuffID.OnFire, 100);
            target.AddBuff(BuffID.Ichor, 100);
            if (Projectile.owner == Main.myPlayer)
        	{
                float spread = 45f * 0.0174f;
                double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y) - spread / 2;
                double deltaAngle = spread / 8f;
                double offsetAngle;
                int i;
                for (i = 0; i < 4; i++)
                {
                    offsetAngle = (startAngle + deltaAngle * (i + i * i) / 2f) + 32f * i;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 2f), (float)(Math.Cos(offsetAngle) * 2f), Mod.Find<ModProjectile>("Celestus2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 2f), (float)(-Math.Cos(offsetAngle) * 2f), Mod.Find<ModProjectile>("Celestus2").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                }
            }
        	SoundEngine.PlaySound(SoundID.Item122, Projectile.position);
        }
    }
}