using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class Phantoplasm : ModItem
{
	public override void SetStaticDefaults()
	{
 		//DisplayName.SetDefault("Phantoplasm");
 		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 5));
 	}
	
	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 97500;
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
	
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(200, 200, 200, 0);
	}
}}