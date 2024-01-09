﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class BiomeOrb : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Biome Orb");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
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
        	bool sky = player.ZoneSkyHeight;
        	int dustType = 0;
        	if (jungle)
        	{
        		dustType = 39;
        	}
        	else if (snow)
        	{
        		dustType = 51;
        	}
        	else if (beach)
        	{
        		dustType = 33;
        	}
        	else if (corrupt)
        	{
        		dustType = 14;
        	}
        	else if (crimson)
        	{
        		dustType = 5;
        	}
        	else if (dungeon)
        	{
        		dustType = 29;
        	}
        	else if (desert)
        	{
        		dustType = 32;
        	}
        	else if (glow)
        	{
        		dustType = 56;
        	}
        	else if (hell)
        	{
        		dustType = 6;
        	}
        	else if (sky)
        	{
        		dustType = 213;
        	}
        	else
        	{
        		dustType = 3;
        	}
            for (int num457 = 0; num457 < 10; num457++)
			{
				int num458 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, dustType, 0f, 0f, 100, default(Color), 1.2f);
				Main.dust[num458].noGravity = true;
				Main.dust[num458].velocity *= 0.5f;
				Main.dust[num458].velocity += Projectile.velocity * 0.1f;
			}
			return;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	Player player = Main.player[Projectile.owner];
        	bool jungle = player.ZoneJungle;
        	bool snow = player.ZoneSnow;
        	bool beach = player.ZoneBeach;
        	bool dungeon = player.ZoneDungeon;
        	bool desert = player.ZoneDesert;
        	bool glow = player.ZoneGlowshroom;
        	bool hell = player.ZoneUnderworldHeight;
        	if (jungle)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 180);
        	}
        	else if (snow)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 180);
        	}
        	else if (beach)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("CrushDepth").Type, 180);
        	}
        	else if (dungeon)
        	{
        		target.AddBuff(BuffID.Frostburn, 180);
        	}
        	else if (desert)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 180);
        	}
        	else if (glow)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("TemporalSadness").Type, 180);
        	}
        	else if (hell)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 180);
        	}
        	else
        	{
        		target.AddBuff(Mod.Find<ModBuff>("ArmorCrunch").Type, 180);
        	}
        }
    }
}