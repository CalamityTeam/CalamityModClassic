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
	public class FlareBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Flare Bolt");
			// Tooltip.SetDefault("Casts a slow-moving ball of flame");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 27;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 12;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.5f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("FlareBoltProjectile").Type;
	        Item.shootSpeed = 7.5f;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HellstoneBar, 6);
			recipe.AddIngredient(ItemID.Obsidian, 9);
			recipe.AddIngredient(ItemID.Fireblossom, 2);
			recipe.AddIngredient(ItemID.LavaBucket);
	        recipe.AddTile(TileID.Bookcases);
	        recipe.Register();
		}
	}
}