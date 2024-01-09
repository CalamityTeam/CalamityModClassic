using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;
using CalamityModClassic1Point0.NPCs;

namespace CalamityModClassic1Point0.Items {
public class LivingShard : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/LivingShard");
        return true;
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Living Shard");
		Item.width = 14;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 15000;
		Item.rare = 7;
	}
}}