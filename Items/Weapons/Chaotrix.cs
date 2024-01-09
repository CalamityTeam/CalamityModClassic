using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class Chaotrix : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Chaotrix");
			//Tooltip.SetDefault("Explodes on enemy hits");
		}

	    public override void SetDefaults()
	    {
	    	Item.CloneDefaults(ItemID.Yelets);
	        Item.damage = 110;
	        Item.useTime = 22;
	        Item.useAnimation = 22;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.channel = true;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.knockBack = 3.6f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.Yellow;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ChaotrixProjectile").Type;
	    }
	    
	    public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CruptixBar", 6);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}