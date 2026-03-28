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
	public class Cnidarian : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cnidarian");
		}

	    public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.CorruptYoyo);
	        Item.damage = 13;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = 5;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.knockBack = 3;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("CnidarianProjectile").Type;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 2);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}