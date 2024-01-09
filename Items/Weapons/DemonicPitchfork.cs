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
	public class DemonicPitchfork : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Demonic Pitchfork");
			//Tooltip.SetDefault("Fires a demonic pitchfork");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 67;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 22;
	        Item.width = 56;
	        Item.height = 56;
	        Item.useTime = 24;
	        Item.useAnimation = 24;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 6;
	        Item.value = 450000;
	        Item.rare = ItemRarityID.LightPurple;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("DemonicPitchfork").Type;
	        Item.shootSpeed = 13f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "TrueShadowScale", 15);
	        recipe.AddIngredient(ItemID.DarkLance);
	        recipe.AddIngredient(ItemID.Obsidian, 20);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}