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
	public class ArchAmaryllis : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arch Amaryllis");
			// Tooltip.SetDefault("Casts a beaming bolt that explodes into smaller homing bolts");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 75;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 66;
	        Item.height = 68;
	        Item.useTime = 23;
	        Item.useAnimation = 23;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(0, 95, 0, 0);
            Item.rare = 9;
	        Item.UseSound = SoundID.Item109;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("BeamingBolt").Type;
	        Item.shootSpeed = 20f;
	    }

		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GleamingMagnolia");
	        recipe.AddIngredient(ItemID.FragmentNebula, 10);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}