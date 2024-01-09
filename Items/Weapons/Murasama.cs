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
	public class Murasama : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Murasama");
			//Tooltip.SetDefault("There will be blood!");
		}

		public override void SetDefaults()
		{
			Item.width = 72;
			Item.damage = 666;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 5;
			Item.knockBack = 6.5f;
			Item.autoReuse = false;
			Item.height = 78;
			Item.value = 30000000;
			Item.shoot = Mod.Find<ModProjectile>("Murasama").Type;
			Item.shootSpeed = 15f;
		}
		
		public override void ModifyTooltips(List<TooltipLine> list)
		{
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(108, 45, 199);
	            }
	        }
	    }
	}
}
