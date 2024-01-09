using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
	public class DarkEnergySpawn : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Spawn");
		}
    	
		public override void SetDefaults()
		{
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
				NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.Center.X - 200, (int)Projectile.Center.Y - 200, Mod.Find<ModNPC>("DarkEnergy").Type);
				NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.Center.X + 200, (int)Projectile.Center.Y - 200, Mod.Find<ModNPC>("DarkEnergy2").Type);
				NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.Center.X, (int)Projectile.Center.Y + 200, Mod.Find<ModNPC>("DarkEnergy3").Type);
				Projectile.ai[1] = -30;
			}
		}
	}
}