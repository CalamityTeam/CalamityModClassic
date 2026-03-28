using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Projectiles.Summon
{
    public class MechwormBody2 : ModProjectile
    {
		private int playerMinionSlots = 0;
		private bool runCheck = true;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mechworm");
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;
            Projectile.minionSlots = 0.5f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 4;
        }

        public override void AI()
		{
			Lighting.AddLight((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f), 0.15f, 0.01f, 0.15f);
			Player player9 = Main.player[Projectile.owner];
			if (player9.maxMinions > playerMinionSlots)
			{
				playerMinionSlots = player9.maxMinions;
			}
			if (runCheck)
			{
				runCheck = false;
				playerMinionSlots = player9.maxMinions;
			}
			CalamityPlayerPreTrailer modPlayer = player9.GetModPlayer<CalamityPlayerPreTrailer>();
			if ((int)Main.time % 120 == 0)
			{
				Projectile.netUpdate = true;
			}
			if (!player9.active || player9.maxMinions < playerMinionSlots)
			{
				Projectile.active = false;
				return;
			}
			int num1051 = 10;
			if (player9.dead)
			{
				modPlayer.mWorm = false;
			}
			if (modPlayer.mWorm)
			{
				Projectile.timeLeft = 2;
			}
			Vector2 value68 = Vector2.Zero;
			float num1064 = 0f;
			float scaleFactor17 = 0f;
			float scaleFactor18 = 1f;
			if (Projectile.ai[1] == 1f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
			int chase = (int)Projectile.ai[0];
			if (chase >= 0 && Main.projectile[chase].active)
			{
				value68 = Main.projectile[chase].Center;
				Vector2 arg_2DE6A_0 = Main.projectile[chase].velocity;
				num1064 = Main.projectile[chase].rotation;
				scaleFactor18 = MathHelper.Clamp(Main.projectile[chase].scale, 0f, 50f);
				scaleFactor17 = 16f;
				Main.projectile[chase].localAI[0] = Projectile.localAI[0] + 1f;
			} 
			else
			{
				return;
			}
			Projectile.alpha -= 42;
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Projectile.velocity = Vector2.Zero;
			Vector2 vector134 = value68 - Projectile.Center;
			if (num1064 != Projectile.rotation)
			{
				float num1068 = MathHelper.WrapAngle(num1064 - Projectile.rotation);
				vector134 = vector134.RotatedBy((double)(num1068 * 0.1f), default(Vector2));
			}
			Projectile.rotation = vector134.ToRotation() + 1.57079637f;
			Projectile.position = Projectile.Center;
			Projectile.scale = scaleFactor18;
			Projectile.width = (Projectile.height = (int)((float)num1051 * Projectile.scale));
			Projectile.Center = Projectile.position;
			if (vector134 != Vector2.Zero)
			{
				Projectile.Center = value68 - Vector2.Normalize(vector134) * scaleFactor17 * scaleFactor18;
			}
			Projectile.spriteDirection = ((vector134.X > 0f) ? 1 : -1);
			return;
		}
        
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation, tex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
	}
}