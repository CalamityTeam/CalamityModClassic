using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class Galaxia2 : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Orb");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 50;
            Projectile.penetrate = 2;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 20 + Main.rand.Next(40);
                if (Main.rand.Next(5) == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item9, Projectile.position);
                }
            }
            Projectile.alpha -= 15;
            int num58 = 150;
            if (Projectile.Center.Y >= Projectile.ai[1])
            {
                num58 = 0;
            }
            if (Projectile.alpha < num58)
            {
                Projectile.alpha = num58;
            }
            Projectile.localAI[0] += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * (float)Projectile.direction;
            Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.01f * (float)Projectile.direction;
            if (Main.rand.Next(8) == 0)
            {
                Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
                int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 66, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
                Main.dust[num59].velocity = value3 * 0.66f;
                Main.dust[num59].noGravity = true;
                Main.dust[num59].position = Projectile.Center + value3 * 12f;
            }
            if (Main.rand.Next(24) == 0)
            {
                int num60 = Gore.NewGore(Projectile.GetSource_FromThis(null), Projectile.Center, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), 16, 1f);
                Main.gore[num60].velocity *= 0.66f;
                Main.gore[num60].velocity += Projectile.velocity * 0.3f;
            }
            if (Projectile.ai[1] == 1f)
            {
                Projectile.light = 0.9f;
                if (Main.rand.Next(5) == 0)
                {
                    int num59 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 66, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
                    Main.dust[num59].noGravity = true;
                }
                if (Main.rand.Next(10) == 0)
                {
                    Gore.NewGore(Projectile.GetSource_FromThis(null), Projectile.position, new Vector2(Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18), 1f);
                }
            }
            float num472 = Projectile.Center.X;
			float num473 = Projectile.Center.Y;
			float num474 = 1600f;
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
				float num483 = 35f;
				Vector2 vector35 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float num484 = num472 - vector35.X;
				float num485 = num473 - vector35.Y;
				float num486 = (float)Math.Sqrt((double)(num484 * num484 + num485 * num485));
				num486 = num483 / num486;
				num484 *= num486;
				num485 *= num486;
				Projectile.velocity.X = (Projectile.velocity.X * 20f + num484) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num485) / 21f;
			}
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, Projectile.alpha);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	Player player = Main.player[Projectile.owner];
        	bool jungle = player.ZoneJungle;
        	bool snow = player.ZoneSnow;
        	bool beach = player.ZoneBeach;
        	bool corrupt = player.ZoneCorrupt;
        	bool crimson = player.ZoneCrimson;
        	bool dungeon = player.ZoneDungeon;
        	bool desert = player.ZoneDesert;
        	bool glow = player.ZoneGlowshroom;
        	bool hell = player.ZoneUnderworldHeight;
        	bool holy = player.ZoneHallow;
        	bool nebula = player.ZoneTowerNebula;
        	bool stardust = player.ZoneTowerStardust;
        	bool solar = player.ZoneTowerSolar;
        	bool vortex = player.ZoneTowerVortex;
        	bool bloodMoon = Main.bloodMoon;
	       	bool snowMoon = Main.snowMoon;
	       	bool pumpkinMoon = Main.pumpkinMoon;
	       	if (bloodMoon)
	       	{
	       		player.AddBuff(BuffID.Battle, 600);
	       	}
	       	if (snowMoon)
	       	{
	       		player.AddBuff(BuffID.RapidHealing, 600);
	       	}
	       	if (pumpkinMoon)
	       	{
	       		player.AddBuff(BuffID.WellFed, 600);
	       	}
        	if (jungle)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 1200);
        		player.AddBuff(BuffID.Thorns, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 206, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (snow)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 1200);
        		player.AddBuff(BuffID.Warmth, 600);
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 118, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        	else if (beach)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 1200);
        		player.AddBuff(BuffID.Wet, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 405, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (corrupt)
        	{
        		player.AddBuff(BuffID.Wrath, 600);
        		int ball = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 95, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[ball].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				Main.projectile[ball].penetrate = 1;
        	}
        	else if (crimson)
        	{
        		player.AddBuff(BuffID.Rage, 600);
        		int ball = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 280, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[ball].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				Main.projectile[ball].penetrate = 1;
            }
        	else if (dungeon)
        	{
        		target.AddBuff(BuffID.Frostburn, 1200);
        		player.AddBuff(BuffID.Dangersense, 600);
        		int ball = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 27, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[ball].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				Main.projectile[ball].penetrate = 1;
            }
        	else if (desert)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 1200);
        		player.AddBuff(BuffID.Endurance, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 661, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (glow)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("TemporalSadness").Type, 1200);
        		player.AddBuff(BuffID.Spelunker, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 131, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (hell)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 1200);
        		player.AddBuff(BuffID.Inferno, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 15, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (holy)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 1200);
        		player.AddBuff(BuffID.Heartreach, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 644, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (nebula)
        	{
        		player.AddBuff(BuffID.MagicPower, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 634, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else if (stardust)
        	{
        		player.AddBuff(BuffID.Summoning, 600);
        		int ball = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 614, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[ball].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
				Main.projectile[ball].penetrate = 1;
            }
        	else if (solar)
        	{
        		player.AddBuff(BuffID.Titan, 600);
        		Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 612, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
        	}
        	else if (vortex)
        	{
        		player.AddBuff(BuffID.AmmoReservation, 600);
        		int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 616, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        	else
        	{
        		target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 1200);
        		player.AddBuff(BuffID.DryadsWard, 600);
        		int ball = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X, Projectile.velocity.Y, 604, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                Main.projectile[ball].penetrate = 1;
            }
        }
    }
}