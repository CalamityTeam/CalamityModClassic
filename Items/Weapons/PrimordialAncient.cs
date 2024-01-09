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
	public class PrimordialAncient : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Primordial Ancient");
			//Tooltip.SetDefault("An ancient relic from an ancient land");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 235;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 30;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5;
	        Item.value = 10000000;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Ancient").Type;
	        Item.shootSpeed = 8f;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(43, 96, 222);
	            }
	        }
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "PrimordialEarth");
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 10);
	        recipe.AddIngredient(null, "CosmiliteBar", 10);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}