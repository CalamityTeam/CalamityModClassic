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
	public class NightsRay : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Night's Ray");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 32;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 56;
	        Item.height = 56;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3.25f;
	        Item.value = 100000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item72;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("NightRay").Type;
	        Item.shootSpeed = 6f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.WandofSparking);
	        recipe.AddIngredient(ItemID.Vilethorn);
	        recipe.AddIngredient(ItemID.AmberStaff);
	        recipe.AddIngredient(ItemID.MagicMissile);
	        recipe.AddIngredient(null, "TrueShadowScale", 15);
	        recipe.AddIngredient(null, "PurifiedGel", 10);
	        recipe.AddTile(TileID.DemonAltar);
	        recipe.Register();
	    }
	}
}