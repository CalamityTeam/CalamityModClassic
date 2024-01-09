using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
    public class TheCommunity : ModItem
    {
    	public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 15));
		}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 10000000;
			Item.accessory = true;
        }
        
        public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
	            }
	        }
	    }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			modPlayer.community = true;
		}
    }
}
