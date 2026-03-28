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
	public class EvergladeSpray : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Everglade Spray");
			// Tooltip.SetDefault("Fires a stream of burning green ichor");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 28;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 8;
	        Item.width = 30;
	        Item.height = 30;
	        Item.useTime = 6;
	        Item.useAnimation = 18;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4.5f;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
	        Item.UseSound = SoundID.Item13;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("EvergladeSprayProjectile").Type;
	        Item.shootSpeed = 10f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ichor, 20);
			recipe.AddIngredient(null, "DraedonBar", 3);
	        recipe.AddTile(TileID.Bookcases);
	        recipe.Register();
		}
	}
}