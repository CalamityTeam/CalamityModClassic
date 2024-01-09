using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Projectiles
{
	public class BlushieStaffProj : ModProjectile
	{
		private const int xRange = 600;
		private const int yRange = 320;

		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Staff of Blushie");
		}

		public override void SetDefaults()
		{
			Projectile.width = 2;
			Projectile.height = 2;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (Main.myPlayer == Projectile.owner)
			{
				if (!player.channel || player.noItems || player.CCed)
				{
					Projectile.Kill();
				}
			}
			Projectile.Center = player.MountedCenter;
			Projectile.timeLeft = 2;
			player.itemTime = 2;
			player.itemAnimation = 2;

			Projectile.ai[0] += 1f;
			Projectile.damage = ((int)Projectile.ai[0] - 120) / 5;
			if (Projectile.damage >= 100 && Main.myPlayer == Projectile.owner)
			{
				if (player.statMana <= 0 && player.manaFlower)
				{
					player.QuickMana();
				}
				if (player.statMana > 0)
				{
					player.statMana--;
				}
				else
				{
					Projectile.Kill();
				}
			}

			if (Projectile.localAI[1] == 0f)
			{
				Projectile.localAI[1] = 37f;
				Projectile.localAI[0] = Main.rand.Next(xRange);
			}
			Projectile.localAI[0] = Next(Projectile.localAI[0]);
		}

		private float Next(float seed)
		{
			return (seed * Projectile.localAI[1] + 101f) % (4 * xRange * yRange);
		}

		public override bool? CanDamage()/* tModPorter Suggestion: Return null instead of true */
		{
			return Projectile.ai[0] > 120f;
		}

		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			hitbox.X -= xRange;
			hitbox.Width += 2 * xRange;
			hitbox.Y -= yRange;
			hitbox.Height += 2 * yRange;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			modifiers.FinalDamage.Base += target.defense / 2;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Projectile.penetrate++;
			target.AddBuff(BuffID.OnFire, 300);
			target.AddBuff(BuffID.Frostburn, 300);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			RasterizerState rasterizer = (RasterizerState)typeof(Main).GetField("Rasterizer", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Main.instance);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, rasterizer, null, Main.Transform);

			Vector2 center = Projectile.Center - Main.screenPosition;
			Vector2 aura = center + new Vector2(-xRange, yRange);
			Texture2D texture = ModContent.Request<Texture2D>("ExtraTextures/BlushieStaffAura").Value;
			Rectangle frame = new Rectangle(50, 0, 50, 32);
			int count = 2 * xRange / 50;
			SpriteEffects effects = Projectile.ai[0] % 30f < 15f ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			for (int k = 0; k < count; k++)
			{
				if (k == 0)
				{
					frame.X = effects == SpriteEffects.None ? 0 : 100;
				}
				else if (k == count - 1)
				{
					frame.X = effects == SpriteEffects.None ? 100 : 0;
				}
				else
				{
					frame.X = 50;
				}
				Main.EntitySpriteDraw(texture, aura, frame, Color.White, 0f, new Vector2(0f, 32f), 1f, effects, 0f);
				aura.X += 50f;
			}

			Vector2 topLeftGear = center + new Vector2(-400f, -100f);
			Vector2 topRightGear = center + new Vector2(200f, -200f);
			Vector2 bottomLeftGear = center + new Vector2(-xRange / 2, yRange / 2);
			Vector2 bottomRightGear = center + new Vector2(xRange - 100, yRange - 100);

			float alpha = Projectile.ai[0] / 60f;
			if (alpha > 0f)
			{
				if (alpha > 1f)
				{
					alpha = 1f;
				}
				DrawChains(Main.spriteBatch, topLeftGear, center, alpha);
				DrawChains(Main.spriteBatch, center, bottomLeftGear, alpha);
				DrawChains(Main.spriteBatch, bottomLeftGear, topLeftGear, alpha);
				DrawChains(Main.spriteBatch, center, bottomRightGear, alpha);
				DrawChains(Main.spriteBatch, bottomRightGear, topRightGear, alpha);
				DrawChains(Main.spriteBatch, topRightGear, center, alpha);
				DrawChains(Main.spriteBatch, new Vector2(bottomLeftGear.X, center.Y + yRange), bottomLeftGear, alpha);
				DrawChains(Main.spriteBatch, bottomRightGear, new Vector2(bottomRightGear.X, center.Y + yRange), alpha);
			}

			float scale = 1f;
			if (Projectile.ai[0] < 60f)
			{
				scale = 4f - 3f * Projectile.ai[0] / 60f;
			}
			texture = ModContent.Request<Texture2D>("ExtraTextures/BlushieStaffGear").Value;
			Vector2 origin = new Vector2(48f, 48f);
			Main.EntitySpriteDraw(texture, center, null, Color.White, Projectile.ai[0] / 20f, origin, 1.5f * scale, SpriteEffects.None, 0f);
			Main.EntitySpriteDraw(texture, topLeftGear, null, Color.White, Projectile.ai[0] / 10f, origin, scale, SpriteEffects.None, 0f);
			Main.EntitySpriteDraw(texture, topRightGear, null, Color.White, -Projectile.ai[0] / 8f, origin, 0.75f * scale, SpriteEffects.None, 0f);
			Main.EntitySpriteDraw(texture, bottomLeftGear, null, Color.White, -Projectile.ai[0] / 15f, origin, 1.4f * scale, SpriteEffects.None, 0f);
			Main.EntitySpriteDraw(texture, bottomRightGear, null, Color.White, Projectile.ai[0] / 10f, origin, scale, SpriteEffects.None, 0f);

			float seed = Projectile.localAI[0];
			texture = ModContent.Request<Texture2D>("ExtraTextures/BlushieStaffFire").Value;
			Vector2 topLeft = center + new Vector2(-xRange, -yRange);
			for (int k = (int)Projectile.ai[0] - 60; k < (int)Projectile.ai[0]; k++)
			{
				if (k > 120)
				{
					Main.spriteBatch.Draw(texture, topLeft + new Vector2(seed % (2 * xRange), seed % (2 * yRange) + k - Projectile.ai[0]), Color.White);
				}
				seed = Next(seed);
			}

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, rasterizer, null, Main.Transform);
			return false;
		}

		private void DrawChains(SpriteBatch spriteBatch, Vector2 start, Vector2 end, float alpha)
		{
			Texture2D texture = ModContent.Request<Texture2D>("ExtraTextures/BlushieStaffChain").Value;
			Vector2 unit = end - start;
			float distance = unit.Length() - 36f;
			unit.Normalize();
			float rotation = unit.ToRotation();
			start += unit * 18f;
			end -= unit * 18f;
			float offset = (Projectile.ai[0] * 2f) % texture.Width;
			start += unit * offset;
			distance -= offset;
			int count = (int)(distance / texture.Width);
			Color color = Color.White * alpha;
			Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
			for (int k = 0; k <= count; k++)
			{
				Main.EntitySpriteDraw(texture, start, null, color, rotation, origin, 1f, SpriteEffects.None, 0f);
				start += unit * texture.Width;
			}
		}
	}
}