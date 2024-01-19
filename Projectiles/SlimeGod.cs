﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class SlimeGod : ModProjectile
    {
    	public float dust = 0f;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Slime God");
			Main.projFrames[Projectile.type] = 2;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.minionSlots = 0f;
            Projectile.alpha = 75;
            Projectile.aiStyle = 26;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            AIType = 266;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
        }
        
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
        	fallThrough = false;
			return true;
		}

        public override void AI()
        {
        	if (dust == 0f)
        	{
        		int num226 = 16;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(Projectile.velocity) * new Vector2((float)Projectile.width / 2f, (float)Projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((double)((float)(num227 - (num226 / 2 - 1)) * 6.28318548f / (float)num226), default(Vector2)) + Projectile.Center;
					Vector2 vector7 = vector6 - Projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, DustID.ShadowbeamStaff, vector7.X * 1f, vector7.Y * 1f, 100, default(Color), 1.1f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].noLight = true;
					Main.dust[num228].velocity = vector7;
				}
				dust += 1f;
        	}
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("SlimeGod").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			player.AddBuff(Mod.Find<ModBuff>("SlimeGod").Type, 3600);
			if (!modPlayer.slimeGod)
        	{
        		Projectile.active = false;
        		return;
        	}
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.sGod = false;
				}
				if (modPlayer.sGod)
				{
					Projectile.timeLeft = 2;
				}
			}
		}
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.penetrate == 0)
            {
                Projectile.Kill();
            }
            return false;
        }
    }
}