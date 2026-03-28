using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Melee.Spears
{
    public class BansheeHook : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Banshee Hook");
		}
    	
        public override void SetDefaults()
        {
			Projectile.width = 40;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 90;
			Projectile.height = 40;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 2;
			Projectile.alpha = 255;
        }

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			Projectile.direction = player.direction;
			player.heldProj = Projectile.whoAmI;
			Projectile.Center = vector;
			if (player.dead)
			{
				Projectile.Kill();
				return;
			}
			if (!player.frozen)
			{
				if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
	        	{
					if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
					{
						Projectile.localAI[0] = 1f;
						Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X + (Projectile.velocity.X * 0.5f), Projectile.Center.Y + (Projectile.velocity.Y * 0.5f), 
						                         Projectile.velocity.X * 0.8f, Projectile.velocity.Y * 0.8f, Mod.Find<ModProjectile>("BansheeHookScythe").Type, (int)((double)Projectile.damage * 1.75), Projectile.knockBack * 0.85f, Projectile.owner, 0f, 0f);
					}
	        	}
				Projectile.spriteDirection = (Projectile.direction = player.direction);
				Projectile.alpha -= 127;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
				if (Projectile.localAI[0] > 0f)
				{
					Projectile.localAI[0] -= 1f;
				}
				float num = (float)player.itemAnimation / (float)player.itemAnimationMax;
				float num2 = 1f - num;
				float num3 = Projectile.velocity.ToRotation();
				float num4 = Projectile.velocity.Length();
				float num5 = 22f;
				Vector2 spinningpoint = new Vector2(1f, 0f).RotatedBy((double)(3.14159274f + num2 * 6.28318548f), default(Vector2)) * new Vector2(num4, Projectile.ai[0]);
				Projectile.position += spinningpoint.RotatedBy((double)num3, default(Vector2)) + new Vector2(num4 + num5, 0f).RotatedBy((double)num3, default(Vector2));
				Vector2 destination = vector + spinningpoint.RotatedBy((double)num3, default(Vector2)) + new Vector2(num4 + num5 + 40f, 0f).RotatedBy((double)num3, default(Vector2));
				Projectile.rotation = player.AngleTo(destination) + ((float)(Math.PI * 0.25)) * (float)player.direction; //or this
				if (Projectile.spriteDirection == -1)
				{
					Projectile.rotation += (float)Math.PI; //change this
				}
				player.DirectionTo(Projectile.Center);
				Vector2 value = player.DirectionTo(destination);
				Vector2 vector2 = Projectile.velocity.SafeNormalize(Vector2.UnitY);
				float num6 = 2f;
				int num7 = 0;
				while ((float)num7 < num6)
				{
					Dust dust = Dust.NewDustDirect(Projectile.Center, 14, 14, 60, 0f, 0f, 110, default(Color), 1f);
					dust.velocity = player.DirectionTo(dust.position) * 2f;
					dust.position = Projectile.Center + vector2.RotatedBy((double)(num2 * 6.28318548f * 2f + (float)num7 / num6 * 6.28318548f), default(Vector2)) * 10f;
					dust.scale = 1f + 0.6f * Main.rand.NextFloat();
					dust.velocity += vector2 * 3f;
					dust.noGravity = true;
					num7++;
				}
				for (int i = 0; i < 1; i++)
				{
					if (Main.rand.Next(3) == 0)
					{
						Dust dust2 = Dust.NewDustDirect(Projectile.Center, 20, 20, 60, 0f, 0f, 110, default(Color), 1f);
						dust2.velocity = player.DirectionTo(dust2.position) * 2f;
						dust2.position = Projectile.Center + value * -110f;
						dust2.scale = 0.45f + 0.4f * Main.rand.NextFloat();
						dust2.fadeIn = 0.7f + 0.4f * Main.rand.NextFloat();
						dust2.noGravity = true;
						dust2.noLight = true;
					}
				}
			}
			if (player.itemAnimation == 2)
			{
				Projectile.Kill();
				player.reuseDelay = 2;
			}
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
        	Vector2 vector53 = Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
        	Texture2D texture2D31 = (Projectile.spriteDirection == -1) ? ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Projectiles/Melee/Spears/BansheeHookAlt").Value : TextureAssets.Projectile[Projectile.type].Value;
			Microsoft.Xna.Framework.Color alpha4 = Projectile.GetAlpha(color25);
			Vector2 origin8 = new Vector2((float)texture2D31.Width, (float)texture2D31.Height) / 2f;
			origin8 = new Vector2((Projectile.spriteDirection == 1) ? ((float)texture2D31.Width - -8f) : -8f, -8f); //-8 -8
			SpriteBatch arg_E055_0 = Main.spriteBatch;
			Vector2 arg_E055_2 = vector53;
			Microsoft.Xna.Framework.Rectangle? sourceRectangle2 = null;
			arg_E055_0.Draw(texture2D31, arg_E055_2, sourceRectangle2, new Microsoft.Xna.Framework.Color(255, 255, 255, 127), Projectile.rotation, origin8, Projectile.scale, SpriteEffects.None, 0f);
        	return false;
        }
        
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
        	float f2 = Projectile.rotation - 0.7853982f * (float)Math.Sign(Projectile.velocity.X) + ((Projectile.spriteDirection == -1) ? 3.14159274f : 0f);
			float num4 = 0f;
			float scaleFactor = -95f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + f2.ToRotationVector2() * scaleFactor, 23f * Projectile.scale, ref num4))
			{
				return true;
			}
			return false;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (Projectile.owner == Main.myPlayer) 
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("BansheeHookBoom").Type, (int)((double)hit.Damage * 0.25), 10f, Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			}
        }
    }
}