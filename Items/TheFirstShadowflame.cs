using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class TheFirstShadowflame : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/TheFirstShadowflame");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("The First Shadowflame");
		////Tooltip.SetDefault("One of the first magical artifacts, granted to a disheveled race of humans long ago by the Tyrant King Yharim\nLittle did the humans know of the horrid curse that lied within...\nGrants shadowflame powers to all minions");
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = 5;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point1.shadowMinions = true;
	}
}}