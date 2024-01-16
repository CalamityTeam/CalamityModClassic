using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
	public class VoidSpawn : ModProjectile
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Void Spawn");
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 20;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			bool dungeon = player.ZoneDungeon;
			bool hell = player.ZoneUnderworldHeight;
        	bool sky = player.ZoneSkyHeight;
			Projectile.ai[1]++;
			
			if (Projectile.ai[1] >= 0)
			{
				SoundEngine.PlaySound(SoundID.Roar, player.position);
				if (dungeon)
				{
					for (int num662 = 0; num662 < 2; num662++)
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DarkEnergySpawn").Type, 0, 0f, Main.myPlayer, 0f, 0f);
					}
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("CeaselessVoid").Type);
				}
				else if (hell)
				{
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("CosmicWraith").Type);
				}
				else if (sky)
				{
					NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("StormWeaverHead").Type);
				}
				Projectile.ai[1] = -30;
			}
		}
	}
}