using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.DevourerMunsters {
public class ArmoredShell : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Armored Shell");
	}
	
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 30;
		Item.maxStack = 999;
		Item.value = 15000;
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