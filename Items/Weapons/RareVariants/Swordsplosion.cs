using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.RareVariants
{
	public class Swordsplosion : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Swordsplosion");
			// Tooltip.SetDefault("Sword swarm");
		}

		public override void SetDefaults()
		{
			Item.width = 84;
			Item.damage = 60;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 15f;
			Item.UseSound = SoundID.Item60;
			Item.autoReuse = true;
			Item.height = 84;
			Item.value = Item.buyPrice(1, 20, 0, 0);
			Item.rare = 10;
			Item.shoot = Mod.Find<ModProjectile>("EonBeam").Type;
			Item.shootSpeed = 16f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 22;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			switch (Main.rand.Next(4))
			{
				case 0: type = Mod.Find<ModProjectile>("EonBeam").Type; break;
				case 1: type = Mod.Find<ModProjectile>("EonBeamV2").Type; break;
				case 2: type = Mod.Find<ModProjectile>("EonBeamV3").Type; break;
				case 3: type = Mod.Find<ModProjectile>("EonBeamV4").Type; break;
			}
			float num72 = Main.rand.Next(22, 30);
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num78 = (float)Main.mouseX + Main.screenPosition.X + vector2.X;
			float num79 = (float)Main.mouseY + Main.screenPosition.Y + vector2.Y;
			if (player.gravDir == -1f)
			{
				num79 = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + vector2.Y;
			}
			float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
			float num81 = num80;
			if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
			{
				num78 = (float)player.direction;
				num79 = 0f;
				num80 = num72;
			}
			else
			{
				num80 = num72 / num80;
			}
			num78 *= num80;
			num79 *= num80;
			int num107 = 16;
			for (int num108 = 0; num108 < num107; num108++)
			{
				vector2 = new Vector2(player.position.X + (float)player.width * 0.5f + (float)(-(float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
				vector2.X = (vector2.X + player.Center.X) / 2f;
				vector2.Y -= (float)(100 * num108);
				num78 = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
				num79 = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;
				num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
				num80 = num72 / num80;
				num78 *= num80;
				num79 *= num80;
				float speedX4 = num78 + (float)Main.rand.Next(-360, 361) * 0.02f;
				float speedY5 = num79 + (float)Main.rand.Next(-360, 361) * 0.02f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X, vector2.Y, speedX4, speedY5, type, damage, knockback, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(5) == 0)
			{
				int num250 = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 66, (float)(player.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.3f);
				Main.dust[num250].velocity *= 0.2f;
				Main.dust[num250].noGravity = true;
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(Mod.Find<ModBuff>("HolyLight").Type, 500);
			target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 500);
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 500);
			target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 500);
		}
	}
}
