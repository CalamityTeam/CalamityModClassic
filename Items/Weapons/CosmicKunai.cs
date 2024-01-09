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
	public class CosmicKunai : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cosmic Kunai");
			//Tooltip.SetDefault("Fires a stream of short-range kunai");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.damage = 105;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.useTime = 1;
			Item.useAnimation = 5;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5.5f;
			Item.UseSound = SoundID.Item109;
			Item.autoReuse = true;
			Item.height = 48;
			Item.value = 1500000;
			Item.shoot = Mod.Find<ModProjectile>("CosmicKunai").Type;
			Item.shootSpeed = 28f;
			Item.rare = ItemRarityID.Cyan;
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
