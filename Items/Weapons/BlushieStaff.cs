using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class BlushieStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Staff of Blushie");
			//Tooltip.SetDefault("Hold your mouse, wait, wait, wait, and put your trust in the power of blue magic");
		}

		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 38;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.channel = true;
			Item.noMelee = true;
			Item.damage = 1;
			Item.knockBack = 1f;
			Item.autoReuse = false;
			Item.useTurn = false;
			Item.rare = ItemRarityID.Purple;
			Item.DamageType = DamageClass.Magic;
			Item.value = 100000000;
			Item.UseSound = SoundID.Item1;
			Item.shoot = Mod.Find<ModProjectile>("BlushieStaffProj").Type;
			Item.mana = 200;
			Item.shootSpeed = 0f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 0, 255);
	            }
	        }
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "Phantoplasm", 100);
	        recipe.AddIngredient(null, "ShadowspecBar", 50);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}