using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
	public class SlimeGodSpawn : ModProjectile
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Slime God Spawn");
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
			Projectile.ai[1]++;
			
			if (Projectile.ai[1] >= 0)
			{
				SoundEngine.PlaySound(SoundID.Roar, player.position);
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGod").Type);
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGodRun").Type);
				NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("SlimeGodCore").Type);
				Projectile.ai[1] = -30;
			}
		}
	}
}