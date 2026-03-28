using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class Murasama : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Murasama");
			Main.projFrames[Projectile.type] = 28;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 148;
            Projectile.height = 68;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 0;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().trueMelee = true;
		}

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
			float num = 0f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			if (Projectile.spriteDirection == -1)
			{
				num = 3.14159274f;
			}
			if (++Projectile.frame >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
			Projectile.soundDelay--;
			if (Projectile.soundDelay <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item15, Projectile.Center);
				Projectile.soundDelay = 24;
			}
			if (Main.myPlayer == Projectile.owner)
			{
				if (player.channel && !player.noItems && !player.CCed)
				{
					float scaleFactor6 = 1f;
					if (player.inventory[player.selectedItem].shoot == Projectile.type)
					{
						scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
					}
					Vector2 vector13 = Main.MouseWorld - vector;
					vector13.Normalize();
					if (vector13.HasNaNs())
					{
						vector13 = Vector2.UnitX * (float)player.direction;
					}
					vector13 *= scaleFactor6;
					if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
					{
						Projectile.netUpdate = true;
					}
					Projectile.velocity = vector13;
				}
				else
				{
					Projectile.Kill();
				}
			}
			Vector2 vector14 = Projectile.Center + Projectile.velocity * 3f;
			Lighting.AddLight(vector14, 3f, 0.2f, 0.2f);
			if (Main.rand.Next(3) == 0)
			{
				int num30 = Dust.NewDust(vector14 - Projectile.Size / 2f, Projectile.width, Projectile.height, 235, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 2f);
				Main.dust[num30].noGravity = true;
				Main.dust[num30].position -= Projectile.velocity;
			}
			Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
			Projectile.rotation = Projectile.velocity.ToRotation() + num;
			Projectile.spriteDirection = Projectile.direction;
			Projectile.timeLeft = 2;
			player.ChangeDir(Projectile.direction);
			player.heldProj = Projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(200, 0, 0, 0);
        }
    }
}