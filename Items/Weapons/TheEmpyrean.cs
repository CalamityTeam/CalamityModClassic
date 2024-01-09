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
	public class TheEmpyrean : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Empyrean");
			//Tooltip.SetDefault("70% chance to not consume gel");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 82;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 60;
			Item.height = 24;
			Item.useTime = 5;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3.5f;
			Item.UseSound = SoundID.Item34;
			Item.value = 900000;
			Item.rare = ItemRarityID.Cyan;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CosmicFire").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 70)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "MeldiateBar", 12);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}