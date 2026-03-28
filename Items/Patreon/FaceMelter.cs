using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Patreon
{
	public class FaceMelter : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Face Melter");
			/* Tooltip.SetDefault("Fires music notes\n" +
							   "Right click summons an amplifier that shoots towards your mouse\n" +
							   "WOOO!! FAAAAAAANTASYY WORLDDDDD!"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 250;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 5;
			Item.width = 56;
			Item.height = 50;
			Item.useTime = 5;
			Item.useAnimation = 5;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 5f;
			Item.rare = 10;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 21;
			Item.value = Item.buyPrice(1, 80, 0, 0);
			Item.shoot = Mod.Find<ModProjectile>("MelterNote1").Type;
			Item.UseSound = SoundID.Item47;
			Item.autoReuse = true;
			Item.shootSpeed = 20f;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.TheAxe);
			recipe.AddIngredient(ItemID.MagicalHarp);
			recipe.AddIngredient(null, "SirensSong");
			recipe.AddIngredient(null, "CosmiliteBar", 10);
			recipe.AddIngredient(null, "NightmareFuel", 10);
			recipe.AddIngredient(null, "EndothermicEnergy", 10);
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				position = Main.MouseWorld;
				velocity.X = 0;
				velocity.Y = 0;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null),position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("MelterAmp").Type, damage, knockback, player.whoAmI);
				return false;
			}
			else
			{
				Item.useTime = 5;
				Item.useAnimation = 5;
				float SpeedX = velocity.X;
				float SpeedY = velocity.Y;
				int note = Main.rand.Next(0, 2);
				if (note == 0)
				{
					damage = (int)(damage * 1.5f);
					type = Mod.Find<ModProjectile>("MelterNote1").Type;
				}
				else
				{
					SpeedX *= 1.5f;
					SpeedY *= 1.5f;
					type = Mod.Find<ModProjectile>("MelterNote2").Type;
				}
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
				return false;
			}
		}
	}
}