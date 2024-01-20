using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class ArcanumoftheVoid : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 1500000;
		Item.accessory = true;
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
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
		modPlayer.projRef = true;
	}
}}