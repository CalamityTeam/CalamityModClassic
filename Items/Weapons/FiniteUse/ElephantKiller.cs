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
	public class ElephantKiller : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elephant Killer");
			/* Tooltip.SetDefault("Uses Magnum Rounds\n" +
                "Does more damage to organic enemies\n" +
				"Can be used thrice per boss battle"); */
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 2000;
            Item.crit += 66;
	        Item.width = 46;
	        Item.height = 26;
	        Item.useTime = 19;
	        Item.useAnimation = 19;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 8f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
	        Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/Magnum");
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("MagnumRound").Type;
	        Item.useAmmo = Mod.Find<ModItem>("MagnumRounds").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 3;
			}
		}

		public override bool OnPickup(Player player)
		{
			if (CalamityPlayerPreTrailer.areThereAnyDamnBosses)
			{
				Item.GetGlobalItem<CalamityGlobalItem>().timesUsed = 3;
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return Item.GetGlobalItem<CalamityGlobalItem>().timesUsed < 3;
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
            recipe.AddIngredient(null, "LightningHawk");
            recipe.AddIngredient(ItemID.LunarBar, 30);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}