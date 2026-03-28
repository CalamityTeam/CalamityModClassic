using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class JudgementBlah : ModProjectile
    {
    	int whiteLightTimer = 5;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Judgement");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.extraUpdates = 3;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
        	Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, ((float)Main.DiscoR / 200f), ((float)Main.DiscoG / 200f), ((float)Main.DiscoB / 200f));
        	whiteLightTimer--;
        	if (whiteLightTimer == 0)
        	{
				float spread = 180f * 0.0174f;
				double startAngle = Math.Atan2(Projectile.velocity.X, Projectile.velocity.Y)- spread/2;
				double deltaAngle = spread/8f;
				double offsetAngle;
				int i;
				if (Projectile.owner == Main.myPlayer)
				{
					for (i = 0; i < 1; i++ )
					{
					   	offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
					   	int projectile1 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( Math.Sin(offsetAngle) * 6f ), (float)( Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("WhiteOrbBlah").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
					    int projectile2 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, (float)( -Math.Sin(offsetAngle) * 6f ), (float)( -Math.Cos(offsetAngle) * 6f ), Mod.Find<ModProjectile>("WhiteOrbBlah").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
						Main.projectile[projectile1].velocity.X *= 0.1f;
					    Main.projectile[projectile1].velocity.Y *= 0.1f;
					    Main.projectile[projectile2].velocity.X *= 0.1f;
					    Main.projectile[projectile2].velocity.Y *= 0.1f;
					}
				}
				whiteLightTimer = 5;
        	}
			for (int num457 = 0; num457 < 2; num457++)
			{
				int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
				Main.dust[num458].noGravity = true;
				Main.dust[num458].velocity *= 0.5f;
				Main.dust[num458].velocity += Projectile.velocity * 0.1f;
			}
			float num472 = Projectile.Center.X;
			float num473 = Projectile.Center.Y;
			float num474 = 400f;
			bool flag17 = false;
			for (int num475 = 0; num475 < 200; num475++)
			{
				if (Main.npc[num475].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[num475].Center, 1, 1))
				{
					float num476 = Main.npc[num475].position.X + (float)(Main.npc[num475].width / 2);
					float num477 = Main.npc[num475].position.Y + (float)(Main.npc[num475].height / 2);
					float num478 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num476) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num477);
					if (num478 < num474)
					{
						num474 = num478;
						num472 = num476;
						num473 = num477;
						flag17 = true;
					}
				}
			}
			if (flag17)
			{
				float num483 = 10f;
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num484 = num472 - vector35.X;
				float num485 = num473 - vector35.Y;
				float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
				return;
			}
			return;
        }
        
        public override void OnKill(int timeLeft)
        {
        	SoundEngine.PlaySound(SoundID.Item122, Projectile.position);
        	if (Projectile.owner == Main.myPlayer)
			{
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y - 100, 0f, 0f, Mod.Find<ModProjectile>("WhiteBoltAuraBlah").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        }
    }
}