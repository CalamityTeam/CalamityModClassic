using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons.FiniteUse
{
	public class Hydra : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hydra");
			/* Tooltip.SetDefault("Uses Explosive Shotgun Shells\n" +
                "Does more damage to everything\n" +
				"Can be used once per boss battle"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 120;
	        Item.width = 66;
	        Item.height = 30;
	        Item.useTime = 33;
	        Item.useAnimation = 33;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 10f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
	        Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/Hydra");
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("ExplosiveShellBullet").Type;
	        Item.useAmmo = Mod.Find<ModItem>("ExplosiveShells").Type;
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 1;
			}
		}

		public override bool OnPickup(Player player)
		{
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 1;
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return Item.GetGlobalItem<CalamityGlobalItem>().timesUsed < 1;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}

		public override void UpdateInventory(Player player)
		{
			if (!CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 0;
			}
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			for (int index = 0; index < 15; ++index)
			{
				float SpeedX = velocity.X + (float)Main.rand.Next(-65, 66) * 0.05f;
				float SpeedY = velocity.Y + (float)Main.rand.Next(-65, 66) * 0.05f;
				Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
			}
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				for (int i = 0; i < 58; i++)
				{
					if (player.inventory[i].type == Item.type)
					{
						player.inventory[i].GetGlobalItem<CalamityGlobalItem>().timesUsed++;
					}
				}
			}
			return false;
		}

		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Shotgun);
			recipe.AddRecipeGroup(RecipeGroupID.IronBar, 20);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}