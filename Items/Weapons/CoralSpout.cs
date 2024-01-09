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
	public class CoralSpout : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Coral Spout");
			//Tooltip.SetDefault("Casts coral water spouts that lay on the ground and damage enemies");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 13;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 4;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 18;
	        Item.useAnimation = 18;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 50000;
	        Item.rare = ItemRarityID.Green;
	        Item.UseSound = SoundID.Item17;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("CoralSpike").Type;
	        Item.shootSpeed = 16f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "VictideBar", 2);
	        recipe.AddIngredient(ItemID.Coral, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}