using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.NPCs;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class DarksunFragment : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Darksun Fragment");
		//Tooltip.SetDefault("A shard of lunar and solar energy");
	}
		
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.5f * num, 0.5f * num, 0.5f * num);
    }
	
	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.maxStack = 999;
		Item.value = 80000;
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
}}