﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
    public class SirenLure : ModProjectile
    {
    	public int dust = 3;
    	
    	public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Siren Lure");
			Main.projFrames[Projectile.type] = 4;
			Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 120;
			Projectile.height = 120;
			DrawOriginOffsetY = 10;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.sentry = true;
        }

        public override void AI()
        {
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("SirenLure").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			if (!modPlayer.sirenWaifu)
        	{
        		Projectile.active = false;
        		return;
        	}
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.slWaifu = false;
				}
				if (modPlayer.slWaifu)
				{
					Projectile.timeLeft = 2;
				}
			}
        	dust--;
        	if (dust >= 0)
        	{
        		int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, DustID.Water, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
        	}
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 1.5f) / 255f);
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 12)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
			   Projectile.frame = 0;
			}
			Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2) + Main.player[Projectile.owner].gfxOffY - 180f;
			if (Main.player[Projectile.owner].gravDir == -1f)
			{
				Projectile.position.Y = Projectile.position.Y + 360f;
				Projectile.rotation = 3.14f;
			}
			else
			{
				Projectile.rotation = 0f;
			}
			Projectile.position.X = (float)((int)Projectile.position.X);
			Projectile.position.Y = (float)((int)Projectile.position.Y);
        	if (Projectile.owner == Main.myPlayer)
			{
				if (Projectile.ai[0] != 0f)
				{
					Projectile.ai[0] -= 1f;
					return;
				}
				bool flag18 = false;
				float num506 = Projectile.Center.X;
				float num507 = Projectile.Center.Y;
				float num508 = 1000f;
				NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
				if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(Projectile, false)) 
				{
					float num509 = ownerMinionAttackTargetNPC.position.X + (float)(ownerMinionAttackTargetNPC.width / 2);
					float num510 = ownerMinionAttackTargetNPC.position.Y + (float)(ownerMinionAttackTargetNPC.height / 2);
					float num511 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num509) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num510);
					if (num511 < num508 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height)) 
					{
						num508 = num511;
						num506 = num509;
						num507 = num510;
						flag18 = true;
					}
				}
				if (!flag18) 
				{
					for (int num512 = 0; num512 < 200; num512++) 
					{
						if (Main.npc[num512].CanBeChasedBy(Projectile, false)) 
						{
							float num513 = Main.npc[num512].position.X + (float)(Main.npc[num512].width / 2);
							float num514 = Main.npc[num512].position.Y + (float)(Main.npc[num512].height / 2);
							float num515 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num513) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num514);
							if (num515 < num508 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num512].position, Main.npc[num512].width, Main.npc[num512].height)) 
							{
								num508 = num515;
								num506 = num513;
								num507 = num514;
								flag18 = true;
							}
						}
					}
				}
				if (flag18)
				{
					float num516 = num506;
					float num517 = num507;
					num506 -= Projectile.Center.X;
					num507 -= Projectile.Center.Y;
					if (num506 < 0f) 
					{
						Projectile.spriteDirection = 1;
					} 
					else
					{
						Projectile.spriteDirection = -1;
					}
					int projectileType = Mod.Find<ModProjectile>("WaterSpearFriendly").Type;
					if (Main.rand.NextBool(9))
					{
						projectileType = Mod.Find<ModProjectile>("FrostMistFriendly").Type;
					}
					else if (Main.rand.NextBool(9))
					{
						projectileType = Mod.Find<ModProjectile>("SirenSongFriendly").Type;
					}
					float num403 = Main.rand.Next(12, 20);
					int damage = NPC.downedMoonlord ? 500 : 100;
					float damageMult = CalamityWorld1Point2.downedDoG ? 3f : 1f;
					Vector2 vector29 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num404 = num516 - vector29.X;
					float num405 = num517 - vector29.Y;
					float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
					num406 = num403 / num406;
					num404 *= num406;
					num405 *= num406;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 4f, Projectile.Center.Y, num404, num405, projectileType, (int)((float)damage * damageMult), Projectile.knockBack, Projectile.owner, 0f, 0f);
					Projectile.ai[0] = 12f;
					return;
				}
			}
        }
    }
}