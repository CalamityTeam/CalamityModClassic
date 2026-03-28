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
	public class PhantasmalFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Phantasmal Fury");
			// Tooltip.SetDefault("Casts a phantasmal bolt that explodes into more bolts");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 320;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 62;
	        Item.height = 60;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item43;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("PhantasmalFury").Type;
	        Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.SpectreStaff);
	        recipe.AddIngredient(null, "RuinousSoul", 2);
	        recipe.AddIngredient(null, "DarkPlasma");
            recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}