using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class Bloodstone : ModItem
{
	public override void SetStaticDefaults()
	{
 		//DisplayName.SetDefault("Bloodstone");
 	}
	
	public override void SetDefaults()
	{
		Item.width = 13;
		Item.height = 10;
		Item.maxStack = 999;
		Item.value = 37500;
		Item.rare = ItemRarityID.Red;
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
}}