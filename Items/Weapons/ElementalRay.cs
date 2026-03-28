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
	public class ElementalRay : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Ray");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 150;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 18;
	        Item.width = 62;
	        Item.height = 62;
	        Item.useTime = 16;
	        Item.useAnimation = 16;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item60;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ElementRay").Type;
	        Item.shootSpeed = 6f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "GalacticaSingularity", 5);
	        recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddIngredient(null, "TerraRay");
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}