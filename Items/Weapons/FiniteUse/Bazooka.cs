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
	public class Bazooka : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bazooka");
			/* Tooltip.SetDefault("Uses Grenade Shells\n" +
                "Does more damage to inorganic enemies\n" +
				"Can be used twice per boss battle"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 500;
	        Item.width = 66;
	        Item.height = 26;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 10f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            
	        Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/BazookaFull");
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("GrenadeRound").Type;
	        Item.useAmmo = Mod.Find<ModItem>("GrenadeRounds").Type;
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 2;
			}
		}

		public override bool OnPickup(Player player)
		{
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 2;
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return Item.GetGlobalItem<CalamityGlobalItem>().timesUsed < 2;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
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
			return true;
		}

		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 20);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.AdamantiteBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
            recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 20);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.TitaniumBar, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}