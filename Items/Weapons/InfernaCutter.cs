using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class InfernaCutter : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Inferna Cutter");
			/* Tooltip.SetDefault("Critical hits with the blade cause small explosions\n" +
				"Generates a number of small sparks when swung"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 85;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 60;
			Item.height = 46;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useTurn = true;
			Item.axe = 27;
			Item.useStyle = 1;
			Item.knockBack = 7f;
			Item.value = Item.buyPrice(0, 36, 0, 0);
			Item.rare = 5;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "PurityAxe");
			recipe.AddIngredient(ItemID.SoulofFright, 8);
			recipe.AddIngredient(null, "EssenceofChaos", 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				if ((player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1) ||
					player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3) ||
					player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5) ||
					player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7) ||
					player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9)))
				{
					float num339 = 0f;
					float num340 = 0f;
					float num341 = 0f;
					float num342 = 0f;
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
					{
						num339 = -7f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
					{
						num339 = -6f;
						num340 = 2f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5))
					{
						num339 = -4f;
						num340 = 4f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
					{
						num339 = -2f;
						num340 = 6f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
					{
						num340 = 7f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
					{
						num342 = 26f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
					{
						num342 -= 4f;
						num341 -= 20f;
					}
					if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
					{
						num341 += 6f;
					}
					if (player.direction == -1)
					{
						if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
						{
							num342 -= 8f;
						}
						if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
						{
							num342 -= 6f;
						}
					}
					num339 *= 1.5f;
					num340 *= 1.5f;
					num342 *= (float)player.direction;
					num341 *= player.gravDir;
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), (float)(hitbox.X + hitbox.Width / 2) + num342, (float)(hitbox.Y + hitbox.Height / 2) + num341, (float)player.direction * num340, num339 * player.gravDir, ProjectileID.Spark, (int)((float)Item.damage * 0.1f * player.GetDamage(DamageClass.Melee).Multiplicative), 0f, player.whoAmI, 0f, 0f);
				}
			}
			if (Main.rand.Next(4) == 0)
			{
				int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 6);
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Crit)
			{
				Projectile.NewProjectile(Entity.GetSource_FromThis(null),target.Center.X, target.Center.Y, 0f, 0f, 612, (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
			}
			target.AddBuff(BuffID.OnFire, 300);
			target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
		}
	}
}