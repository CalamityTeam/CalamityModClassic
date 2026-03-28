using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class SolarPixie : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Solar Pixie");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public override void AI()
        {
        	Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0.25f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
			bool flag64 = Projectile.type == Mod.Find<ModProjectile>("SolarPixie").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			player.AddBuff(Mod.Find<ModBuff>("SolarSpirit").Type, 3600);
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.SP = false;
				}
				if (modPlayer.SP)
				{
					Projectile.timeLeft = 2;
				}
			}
			Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2) + Main.player[Projectile.owner].gfxOffY - 60f;
			if (Main.player[Projectile.owner].gravDir == -1f)
			{
				Projectile.position.Y = Projectile.position.Y + 140f;
				Projectile.rotation = 3.14f;
			}
			else
			{
				Projectile.rotation = 0f;
			}
			Projectile.position.X = (float)((int)Projectile.position.X);
			Projectile.position.Y = (float)((int)Projectile.position.Y);
			float num395 = (float)Main.mouseTextColor / 200f - 0.35f;
			num395 *= 0.2f;
			Projectile.scale = num395 + 0.95f;
			if (Projectile.localAI[0] == 0f)
        	{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num501 = 25;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 244, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
				Projectile.localAI[0] += 1f;
        	}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			if (Projectile.owner == Main.myPlayer)
			{
				if (Projectile.ai[0] != 0f)
				{
					Projectile.ai[0] -= 1f;
					return;
				}
				float num396 = Projectile.position.X;
				float num397 = Projectile.position.Y;
				float num398 = 700f;
				bool flag11 = false;
				if (player.HasMinionAttackTargetNPC)
				{
					NPC npc = Main.npc[player.MinionAttackTargetNPC];
					if (npc.CanBeChasedBy(Projectile, false))
					{
						float num539 = npc.position.X + (float)(npc.width / 2);
						float num540 = npc.position.Y + (float)(npc.height / 2);
						float num541 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num539) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num540);
						if (num541 < num398 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
						{
							num398 = num541;
							num396 = num539;
							num397 = num540;
							flag11 = true;
						}
					}
				}
				else
				{
					for (int num399 = 0; num399 < 200; num399++)
					{
						if (Main.npc[num399].CanBeChasedBy(Projectile, true))
						{
							float num400 = Main.npc[num399].position.X + (float)(Main.npc[num399].width / 2);
							float num401 = Main.npc[num399].position.Y + (float)(Main.npc[num399].height / 2);
							float num402 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num400) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num401);
							if (num402 < num398 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num399].position, Main.npc[num399].width, Main.npc[num399].height))
							{
								num398 = num402;
								num396 = num400;
								num397 = num401;
								flag11 = true;
							}
						}
					}
				}
				if (flag11)
				{
					float num403 = 30f;
					Vector2 vector29 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
					float num404 = num396 - vector29.X;
					float num405 = num397 - vector29.Y;
					float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
					num406 = num403 / num406;
					num404 *= num406;
					num405 *= num406;
					int beam = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 4f, Projectile.Center.Y, num404, num405, 260, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
                    Main.projectile[beam].minion = true;
                    Projectile.ai[0] = 50f;
				}
			}
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(200, 200, 200, 200);
		}

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}