using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class DesertFeather : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/DesertFeather");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Desert Feather");
		Item.width = 24;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 5000;
		Item.rare = 2;
	}
}}