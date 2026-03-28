using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;

namespace CalamityModClassicPreTrailer.Projectiles.Magic
{
    public class YharimsCrystal : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystal");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 22;
            Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.ignoreWater = true;
        }

        public override void AI()
        {
        	Player player = Main.player[Projectile.owner];
        	float num = 1.57079637f;
        	Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
        	float num26 = 30f;
			if (Projectile.ai[0] > 90f)
			{
				num26 = 15f;
			}
			if (Projectile.ai[0] > 120f)
			{
				num26 = 5f;
			}
			Projectile.damage = (int)((float)player.inventory[player.selectedItem].damage * player.GetDamage(DamageClass.Magic).Multiplicative);
			Projectile.ai[0] += 1f;
			Projectile.ai[1] += 1f;
			bool flag9 = false;
			if (Projectile.ai[0] % num26 == 0f)
			{
				flag9 = true;
			}
			int num27 = 10;
			bool flag10 = false;
			if (Projectile.ai[0] % num26 == 0f)
			{
				flag10 = true;
			}
			if (Projectile.ai[1] >= 1f)
			{
				Projectile.ai[1] = 0f;
				flag10 = true;
				if (Main.myPlayer == Projectile.owner)
				{
					float scaleFactor5 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
					Vector2 value12 = vector;
					Vector2 value13 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - value12;
					if (player.gravDir == -1f)
					{
						value13.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - value12.Y;
					}
					Vector2 vector11 = Vector2.Normalize(value13);
					if (float.IsNaN(vector11.X) || float.IsNaN(vector11.Y))
					{
						vector11 = -Vector2.UnitY;
					}
					vector11 = Vector2.Normalize(Vector2.Lerp(vector11, Vector2.Normalize(Projectile.velocity), 0.92f));
					vector11 *= scaleFactor5;
					if (vector11.X != Projectile.velocity.X || vector11.Y != Projectile.velocity.Y)
					{
						Projectile.netUpdate = true;
					}
					Projectile.velocity = vector11;
				}
			}
			Projectile.frameCounter++;
			int num28 = (Projectile.ai[0] < 120f) ? 3 : 1;
			if (Projectile.frameCounter >= num28)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 6)
				{
					Projectile.frame = 0;
				}
			}
			if (Projectile.soundDelay <= 0)
			{
				Projectile.soundDelay = num27;
				Projectile.soundDelay *= 2;
				if (Projectile.ai[0] != 1f)
				{
					SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
				}
			}
			if (flag10 && Main.myPlayer == Projectile.owner)
			{
				bool flag11 = !flag9 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
				bool flag12 = player.channel && flag11 && !player.noItems && !player.CCed;
				if (flag12)
				{
					if (Projectile.ai[0] == 1f)
					{
						Vector2 center3 = Projectile.Center;
						Vector2 vector12 = Vector2.Normalize(Projectile.velocity);
						if (float.IsNaN(vector12.X) || float.IsNaN(vector12.Y))
						{
							vector12 = -Vector2.UnitY;
						}
						int num29 = Projectile.damage;
						for (int l = 0; l < 7; l++)
						{
							Projectile.NewProjectile(Projectile.GetSource_FromThis(null), center3.X, center3.Y, vector12.X, vector12.Y, Mod.Find<ModProjectile>("YharimsCrystalBeam").Type, num29, Projectile.knockBack, Projectile.owner, (float)l, (float)Projectile.whoAmI);
						}
						Projectile.netUpdate = true;
					}
				}
				else
				{
					Projectile.Kill();
				}
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
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
        	Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
			if (Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type])
			{
				color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
        	Texture2D texture2D14 = TextureAssets.Projectile[Projectile.type].Value;
			int num215 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y7 = num215 * Projectile.frame;
			Vector2 vector27 = (Projectile.position + new Vector2((float)Projectile.width, (float)Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
			float scale5 = 1f;
			if (Main.player[Projectile.owner].shroomiteStealth && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].CountsAsClass(DamageClass.Ranged))
			{
				float num216 = Main.player[Projectile.owner].stealth;
				if ((double)num216 < 0.03)
				{
					num216 = 0.03f;
				}
				float arg_97B3_0 = (1f + num216 * 10f) / 11f;
				color25 *= num216;
				scale5 = num216;
			}
			if (Main.player[Projectile.owner].setVortex && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].CountsAsClass(DamageClass.Ranged))
			{
				float num217 = Main.player[Projectile.owner].stealth;
				if ((double)num217 < 0.03)
				{
					num217 = 0.03f;
				}
				float arg_9854_0 = (1f + num217 * 10f) / 11f;
				color25 = color25.MultiplyRGBA(new Microsoft.Xna.Framework.Color(Vector4.Lerp(Vector4.One, new Vector4(0.16f, 0.12f, 0f, 0f), 1f - num217)));
				scale5 = num217;
			}
			Main.spriteBatch.Draw(texture2D14, vector27, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, texture2D14.Width, num215)), Projectile.GetAlpha(color25), Projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)num215 / 2f), Projectile.scale, spriteEffects, 0f);
        	float scaleFactor2 = (float)Math.Cos((double)(6.28318548f * (Projectile.ai[0] / 30f))) * 2f + 2f;
			if (Projectile.ai[0] > 120f)
			{
				scaleFactor2 = 4f;
			}
			for (float num218 = 0f; num218 < 4f; num218 += 1f)
			{
				Main.spriteBatch.Draw(texture2D14, vector27 + Vector2.UnitY.RotatedBy((double)(num218 * 6.28318548f / 4f), default(Vector2)) * scaleFactor2, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, texture2D14.Width, num215)), Projectile.GetAlpha(color25).MultiplyRGBA(new Microsoft.Xna.Framework.Color(255, 255, 255, 0)) * 0.03f, Projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)num215 / 2f), Projectile.scale, spriteEffects, 0f);
			}
			return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 200);
        }
    }
}