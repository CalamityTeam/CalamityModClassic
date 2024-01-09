using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Perforators
{
	public class Aorta : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Aorta");
		}

	    public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.Valor);
	        Item.damage = 25;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.knockBack = 4.25f;
	        Item.value = 70000;
	        Item.rare = ItemRarityID.Orange;
	        Item.autoReuse = false;
	        Item.shoot = Mod.Find<ModProjectile>("AortaProjectile").Type;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "BloodSample", 6);
	        recipe.AddIngredient(ItemID.Vertebrae, 3);
	        recipe.AddIngredient(ItemID.CrimtaneBar, 3);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
		}
	}
}