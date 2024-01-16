using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class SirenLure : ModProjectile
    {
    	public int dust = 3;
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Siren Lure");
            Projectile.width = 50;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Main.projFrames[Projectile.type] = 8;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.sentry = true;
        }

        public override void AI()
        {
        	dust--;
        	if (dust >= 0)
        	{
        		int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 33, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
        	}
        	Projectile.ai[1] += 1f;
			if (Projectile.ai[1] >= 7200f)
			{
				Projectile.alpha += 5;
				if (Projectile.alpha > 255)
				{
					Projectile.alpha = 255;
					Projectile.Kill();
				}
			}
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] >= 10f)
			{
				Projectile.localAI[0] = 0f;
				int num416 = 0;
				int num417 = 0;
				float num418 = 0f;
				int num419 = Projectile.type;
				for (int num420 = 0; num420 < 1000; num420++)
				{
					if (Main.projectile[num420].active && Main.projectile[num420].owner == Projectile.owner && Main.projectile[num420].type == num419 && Main.projectile[num420].ai[1] < 3600f)
					{
						num416++;
						if (Main.projectile[num420].ai[1] > num418)
						{
							num417 = num420;
							num418 = Main.projectile[num420].ai[1];
						}
					}
				}
				if (num416 > 1)
				{
					Main.projectile[num417].netUpdate = true;
					Main.projectile[num417].ai[1] = 36000f;
					return;
				}
			}
        	Projectile.velocity.X = 0f;
        	Projectile.velocity.Y = 0f;
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 0.75f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f);
        	Projectile.frameCounter++;
			if (Projectile.frameCounter > 16)
			{
			    Projectile.frame++;
			    Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 7)
			{
			   Projectile.frame = 0;
			}
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
						Projectile.spriteDirection = -1;
					} 
					else
					{
						Projectile.spriteDirection = 1;
					}
					int projectileType = Mod.Find<ModProjectile>("WaterSpearFriendly").Type;
					if (Main.rand.Next(9) == 0)
					{
						projectileType = Mod.Find<ModProjectile>("FrostMistFriendly").Type;
					}
					else if (Main.rand.Next(9) == 0)
					{
						projectileType = Mod.Find<ModProjectile>("SirenSongFriendly").Type;
					}
					else
					{
						projectileType = Mod.Find<ModProjectile>("WaterSpearFriendly").Type;
					}
					float num403 = Main.rand.Next(12, 20); //modify the speed the projectile are shot.  Lower number = slower projectile.
					Vector2 vector29 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num404 = num516 - vector29.X;
					float num405 = num517 - vector29.Y;
					float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
					num406 = num403 / num406;
					num404 *= num406;
					num405 *= num406;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 4f, Projectile.Center.Y, num404, num405, projectileType, 40, Projectile.knockBack, Projectile.owner, 0f, 0f);
					Projectile.ai[0] = 25f;
					return;
				}
			}
        }
    }
}