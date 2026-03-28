using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class Monsoon : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Monsoon");
		}

		public override void SetDefaults()
		{
			Item.damage = 63;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 30;
			Item.height = 62;
			Item.useTime = 21;
			Item.useAnimation = 21;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 95, 0, 0);
			Item.rare = 9;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
			Item.shoot = 1;
			Item.shootSpeed = 10f;
			Item.useAmmo = 40;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num117 = 0.314159274f;
			int num118 = 5;
			Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
			vector7.Normalize();
			vector7 *= 40f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
			for (int num119 = 0; num119 < num118; num119++)
			{
				float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
				Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
				if (!flag11)
				{
					value9 -= vector7;
				}
				if (type == ProjectileID.WoodenArrowFriendly)
				{
					if (Main.rand.Next(12) == 0)
					{
						type = 408;
					}
					if (Main.rand.Next(25) == 0)
					{
						type = Mod.Find<ModProjectile>("TyphoonArrow").Type;
					}
					int num121 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
					Main.projectile[num121].GetGlobalProjectile<CalamityGlobalProjectile>().forceRanged = true;
					Main.projectile[num121].noDropItem = true;
				}
				else
				{
					int num121 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
					Main.projectile[num121].noDropItem = true;
				}
			}
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.FragmentVortex, 15);
			recipe.AddIngredient(ItemID.Tsunami);
			recipe.AddIngredient(ItemID.SharkFin, 2);
			recipe.AddIngredient(null, "DepthCells", 10);
			recipe.AddIngredient(null, "Lumenite", 10);
			recipe.AddIngredient(null, "Tenebris", 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}