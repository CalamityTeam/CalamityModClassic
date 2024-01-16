using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class EbonianGel : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/EbonianGel");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Blighted Gel");
		Item.width = 16;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 1000;
		Item.rare = 2;
	}
}}