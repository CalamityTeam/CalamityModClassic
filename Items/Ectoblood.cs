using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class Ectoblood : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Ectoblood");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ectoblood");
		Item.width = 24;
		Item.height = 32;
		Item.scale = 0.8f;
		Item.maxStack = 999;
		Item.value = 3500;
		Item.rare = 8;
	}
}}