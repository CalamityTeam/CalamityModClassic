using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class OrichalcumSpikedGemstoneProjectile : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Orichalcum Spiked Gemstone");
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.scale = 0.75f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 2;
            Projectile.penetrate = 6;
            Projectile.timeLeft = 600;
            AIType = 48;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
        	Vector2 velocity = Projectile.velocity;
            if (Projectile.velocity.Y != velocity.Y && (velocity.Y < -3f || velocity.Y > 3f))
			{
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
			}
        	if (Projectile.velocity.X != velocity.X)
			{
				Projectile.velocity.X = velocity.X * -0.5f;
			}
			if (Projectile.velocity.Y != velocity.Y && velocity.Y > 1f)
			{
				Projectile.velocity.Y = velocity.Y * -0.5f;
			}
            return false;
        }
        
        public override void OnKill(int timeLeft)
        {
        	if (Main.rand.Next(2) == 0)
        	{
        		Item.NewItem(Projectile.GetSource_FromThis(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, Mod.Find<ModItem>("OrichalcumSpikedGemstone").Type);
        	}
        }
    }
}