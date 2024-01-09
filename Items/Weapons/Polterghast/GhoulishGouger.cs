using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons.Polterghast
{
	public class GhoulishGouger : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Ghoulish Gouger");
			//Tooltip.SetDefault("Throws a ghoulish scythe");
		}

		public override void SetDefaults()
		{
			Item.width = 68;
			Item.damage = 250;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.useTime = 12;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 60;
			Item.value = 1000000;
			Item.shoot = Mod.Find<ModProjectile>("GhoulishGouger").Type;
			Item.shootSpeed = 20f;
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
	}
}
