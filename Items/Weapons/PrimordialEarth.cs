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
	public class PrimordialEarth : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Primordial Earth");
			//Tooltip.SetDefault("An ancient relic from an ancient land");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 75;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 19;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7;
	        Item.value = 8500000;
	        Item.rare = ItemRarityID.Red;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("SupremeDustProjectile").Type;
	        Item.shootSpeed = 4f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "DeathValley");
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 3);
	        recipe.AddIngredient(ItemID.MeteoriteBar, 5);
	        recipe.AddIngredient(ItemID.Ectoplasm, 5);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}