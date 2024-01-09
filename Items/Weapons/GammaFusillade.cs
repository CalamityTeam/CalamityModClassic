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
	public class GammaFusillade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Gamma Fusillade");
			//Tooltip.SetDefault("Unleashes a concentrated beam of gamma radiation");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 184;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 6;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 3;
	        Item.useAnimation = 3;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 3;
	        Item.value = 1250000;
	        Item.UseSound = SoundID.Item33;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("GammaLaser").Type;
	        Item.shootSpeed = 20f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "UeliaceBar", 8);
	        recipe.AddIngredient(ItemID.SpellTome);
	        recipe.AddIngredient(ItemID.SoulofMight, 10);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}