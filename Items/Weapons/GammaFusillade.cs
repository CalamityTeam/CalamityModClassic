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
	public class GammaFusillade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Biofusillade");
			// Tooltip.SetDefault("Unleashes a concentrated beam of life energy");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 110;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 4;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("GammaLaser").Type;
	        Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "UeliaceBar", 8);
	        recipe.AddIngredient(ItemID.SpellTome);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}