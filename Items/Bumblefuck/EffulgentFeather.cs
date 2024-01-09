using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Bumblefuck {
public class EffulgentFeather : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Effulgent Feather");
		//Tooltip.SetDefault("It vibrates with fluffy golden energy");
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 12));
	}
	
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 150000;
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
}}