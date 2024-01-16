using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class CheatTestThing : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/CheatTestThing");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("lol");
		Item.width = 26;
		Item.height = 26;
		Item.value = 1;
		Item.rare = 1;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.immune = true;
	}
}}