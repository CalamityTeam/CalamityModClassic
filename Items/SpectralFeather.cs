using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class SpectralFeather : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/SpectralFeather");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Spectral Feather");
		Item.width = 24;
		Item.alpha = 100;
		Item.height = 24;
		Item.maxStack = 999;
		Item.value = 10000;
		Item.rare = 3;
	}
}}