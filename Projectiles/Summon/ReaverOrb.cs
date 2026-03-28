using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class ReaverOrb : ModProjectile
    {
    	public int dust = 3;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Reaver Orb");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
			Projectile.minion = true;
			Projectile.minionSlots = 0f;
            Projectile.timeLeft = 18000;
            Projectile.alpha = 50;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
        }

        public override void AI()
        {
        	bool flag64 = Projectile.type == Mod.Find<ModProjectile>("ReaverOrb").Type;
			Player player = Main.player[Projectile.owner];
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			if (!modPlayer.reaverOrb)
        	{
        		Projectile.active = false;
        		return;
        	}
			if (flag64)
			{
				if (player.dead)
				{
					modPlayer.rOrb = false;
				}
				if (modPlayer.rOrb)
				{
					Projectile.timeLeft = 2;
				}
			}
        	dust--;
        	if (dust >= 0)
        	{
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue = Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base;
				Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue = Projectile.damage;
				int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, 157, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
        	}
			if (Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base != Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue)
			{
				int damage2 = (int)(((float)Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionProjectileDamageValue /
					Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().spawnedPlayerMinionDamageValue) *
					Main.player[Projectile.owner].GetDamage(DamageClass.Summon).Base);
				Projectile.damage = damage2;
			}
			Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0f) / 255f, ((255 - Projectile.alpha) * 1f) / 255f, ((255 - Projectile.alpha) * 0f) / 255f);
			Projectile.position.X = Main.player[Projectile.owner].Center.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (float)(Projectile.height / 2) + Main.player[Projectile.owner].gfxOffY - 60f;
			if (Main.player[Projectile.owner].gravDir == -1f)
			{
				Projectile.position.Y = Projectile.position.Y + 120f;
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
				float num508 = 600f;
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
							flag18 = true;
						}
					}
				}
				if (flag18)
				{
					int num251 = Main.rand.Next(4, 9);
					for (int num252 = 0; num252 < num251; num252++)
					{
						Vector2 value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						while (value15.X == 0f && value15.Y == 0f)
						{
							value15 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
						}
						value15.Normalize();
						value15 *= (float)Main.rand.Next(90, 121) * 0.1f;
						int spore = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - 4f, Projectile.Center.Y, value15.X, value15.Y, 569 + Main.rand.Next(3), Projectile.damage, 1.5f, Projectile.owner, 0f, 0f);
						Main.projectile[spore].minion = true;
						Main.projectile[spore].minionSlots = 0f;
					}
					SoundEngine.PlaySound(SoundID.Item77, Projectile.position);
					Projectile.ai[0] = 50f;
				}
			}
        }

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return false;
		}
	}
}