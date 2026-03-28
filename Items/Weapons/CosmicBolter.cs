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
	public class CosmicBolter : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Bolter");
		}

		public override void SetDefaults()
		{
			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 76;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2.75f;
			Item.value = Item.buyPrice(0, 80, 0, 0);
			Item.rare = 8;
			Item.UseSound = SoundID.Item75;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("LunarBolt2").Type;
			Item.shootSpeed = 10f;
			Item.useAmmo = 40;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
			float num117 = 0.314159274f;
			int num118 = 2;
			Vector2 vector7 = new Vector2(velocity.X, velocity.Y);
			vector7.Normalize();
			vector7 *= 15f;
			bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector7, 0, 0);
			for (int num119 = 0; num119 < num118; num119++)
			{
				float num120 = (float)num119 - ((float)num118 - 1f) / 2f;
				Vector2 value9 = vector7.RotatedBy((double)(num117 * num120), default(Vector2));
				if (!flag11)
				{
					value9 -= vector7;
				}
				int num121 = Projectile.NewProjectile(Entity.GetSource_FromThis(null), vector2.X + value9.X, vector2.Y + value9.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("LunarBolt2").Type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
				Main.projectile[num121].noDropItem = true;
			}
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "LunarianBow");
			recipe.AddIngredient(null, "LivingShard", 5);
			recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}