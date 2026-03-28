using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;

namespace CalamityModClassicPreTrailer.Projectiles.Melee
{
    public class CosmicDischarge : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Discharge");
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 4;
			Projectile.GetGlobalProjectile<CalamityGlobalProjectile>().trueMelee = true;
		}

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
			float num = 1.57079637f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			if (Projectile.localAI[1] > 0f)
			{
				Projectile.localAI[1] -= 1f;
			}
			Projectile.alpha -= 42;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = Projectile.velocity.ToRotation();
			}
			float num32 = (float)((Projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : -1);
			if (Projectile.ai[1] <= 0f)
			{
				num32 *= -1f;
			}
			Vector2 vector17 = (num32 * (Projectile.ai[0] / 30f * 6.28318548f - 1.57079637f)).ToRotationVector2();
			vector17.Y *= (float)Math.Sin((double)Projectile.ai[1]);
			if (Projectile.ai[1] <= 0f)
			{
				vector17.Y *= -1f;
			}
			vector17 = vector17.RotatedBy((double)Projectile.localAI[0], default(Vector2));
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] < 30f)
			{
				Projectile.velocity += 48f * vector17;
			}
			else
			{
				Projectile.Kill();
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
			Vector2 vector24 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
			if (player.direction != 1)
			{
				vector24.X = (float)player.bodyFrame.Width - vector24.X;
			}
			if (player.gravDir != 1f)
			{
				vector24.Y = (float)player.bodyFrame.Height - vector24.Y;
			}
			vector24 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
			Projectile.Center = player.RotatedRelativePoint(player.position + vector24, true) - Projectile.velocity;
			if (Projectile.alpha == 0)
			{
				for (int num51 = 0; num51 < 2; num51++)
				{
					Dust dust = Main.dust[Dust.NewDust(Projectile.position + Projectile.velocity * 2f, Projectile.width, Projectile.height, 67, 0f, 0f, 100, new Color(150, Main.DiscoG, 255), 2f)];
					dust.noGravity = true;
					dust.velocity *= 2f;
					dust.velocity += Projectile.localAI[0].ToRotationVector2();
					dust.fadeIn = 1.5f;
				}
				float num52 = 18f;
				int num53 = 0;
				while ((float)num53 < num52)
				{
					if (Main.rand.Next(4) == 0)
					{
						Vector2 position = Projectile.position + Projectile.velocity + Projectile.velocity * ((float)num53 / num52);
						Dust dust2 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, 187, 0f, 0f, 100, new Color(150, Main.DiscoG, 255), 1f)];
						dust2.noGravity = true;
						dust2.fadeIn = 0.5f;
						dust2.velocity += Projectile.localAI[0].ToRotationVector2();
						dust2.noLight = true;
					}
					num53++;
				}
			}
        }
        
        public override bool PreDraw(ref Color lightColor)
		{
        	Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
        	Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
			if (Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type])
			{
				color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
			}
			Vector2 projPos = Projectile.position;
        	projPos = new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition; //fuck it
			Texture2D texture2D22 = TextureAssets.Projectile[Projectile.type].Value;
			Microsoft.Xna.Framework.Color alpha3 = Projectile.GetAlpha(color25);
			if (Projectile.velocity == Vector2.Zero)
			{
				return false;
			}
			float num230 = Projectile.velocity.Length() + 16f;
			bool flag24 = num230 < 100f;
			Vector2 value28 = Vector2.Normalize(Projectile.velocity);
			Microsoft.Xna.Framework.Rectangle rectangle8 = new Microsoft.Xna.Framework.Rectangle(0, 0, texture2D22.Width, 36); //2 and 40
			Vector2 value29 = new Vector2(0f, Main.player[Projectile.owner].gfxOffY);
			float rotation24 = Projectile.rotation + 3.14159274f;
			Main.spriteBatch.Draw(texture2D22, Projectile.Center.Floor() - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, rectangle8.Size() / 2f - Vector2.UnitY * 4f, Projectile.scale, SpriteEffects.None, 0f);
			num230 -= 40f * Projectile.scale;
			Vector2 vector31 = Projectile.Center.Floor();
			vector31 += value28 * Projectile.scale * 24f;
			rectangle8 = new Microsoft.Xna.Framework.Rectangle(0, 62, texture2D22.Width, 18); //68 and 18
			if (num230 > 0f)
			{
				float num231 = 0f;
				while (num231 + 1f < num230)
				{
					if (num230 - num231 < (float)rectangle8.Height)
					{
						rectangle8.Height = (int)(num230 - num231);
					}
					Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, new Vector2((float)(rectangle8.Width / 2), 0f), Projectile.scale, SpriteEffects.None, 0f);
					num231 += (float)rectangle8.Height * Projectile.scale;
					vector31 += value28 * (float)rectangle8.Height * Projectile.scale;
				}
			}
			Vector2 value30 = vector31;
			vector31 = Projectile.Center.Floor();
			vector31 += value28 * Projectile.scale * 24f;
			rectangle8 = new Microsoft.Xna.Framework.Rectangle(0, 40, texture2D22.Width, 20); //46 and 18
			int num232 = 18;
			if (flag24)
			{
				num232 = 9;
			}
			float num233 = num230;
			if (num230 > 0f)
			{
				float num234 = 0f;
				float num235 = num233 / (float)num232;
				num234 += num235 * 0.25f;
				vector31 += value28 * num235 * 0.25f;
				for (int num236 = 0; num236 < num232; num236++)
				{
					float num237 = num235;
					if (num236 == 0)
					{
						num237 *= 0.75f;
					}
					Main.spriteBatch.Draw(texture2D22, vector31 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, new Vector2((float)(rectangle8.Width / 2), 0f), Projectile.scale, SpriteEffects.None, 0f);
					num234 += num237;
					vector31 += value28 * num237;
				}
			}
			rectangle8 = new Microsoft.Xna.Framework.Rectangle(0, 84, texture2D22.Width, 56); //90 and 48
			Main.spriteBatch.Draw(texture2D22, value30 - Main.screenPosition + value29, new Microsoft.Xna.Framework.Rectangle?(rectangle8), alpha3, rotation24, texture2D22.Frame(1, 1, 0, 0).Top(), Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(150, Main.DiscoG, 255, 200);
        }
        
        public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Projectile.localAI[1], (float)Projectile.width * Projectile.scale, DelegateMethods.CutTiles);
		}
        
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
        	if (projHitbox.Intersects(targetHitbox))
			{
				return true;
			}
        	float num8 = 0f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity, 16f * Projectile.scale, ref num8))
			{
				return true;
			}
			return false;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
        	Player player = Main.player[Projectile.owner];
        	target.AddBuff(BuffID.Frostburn, 300);
        	if (Main.rand.Next(3) == 0)
        	{
        		target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 120);
        	}
			if (Projectile.localAI[1] <= 0f && Projectile.owner == Main.myPlayer) 
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("CosmicIceBurst").Type, hit.Damage, 10f, Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			}
			Projectile.localAI[1] = 4f;
			player.AddBuff(Mod.Find<ModBuff>("CosmicFreeze").Type, 300);
        }
    }
}