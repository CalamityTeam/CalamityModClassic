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
	public class PhantasmalFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Phantasmal Fury");
			//Tooltip.SetDefault("Casts a phantasmal bolt that explodes into more bolts");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 215;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 30;
	        Item.width = 62;
	        Item.height = 60;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 7.5f;
	        Item.value = 800000;
	        Item.UseSound = SoundID.Item43;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("PhantasmalFury").Type;
	        Item.shootSpeed = 12f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 0);
	            }
	        }
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.SpectreStaff);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddIngredient(null, "DarkPlasma");
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}