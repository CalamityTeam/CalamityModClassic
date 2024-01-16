using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;
using CalamityModClassic1Point1.NPCs;

namespace CalamityModClassic1Point1.Items {
public class ShadowEssence : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/ShadowEssence");
        return true;
    }
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.1f * num, 0.1f * num, 0.1f * num);
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Shadow Essence");
		////Tooltip.SetDefault("The essence of shadow masters");
		Item.width = 10;
		Item.height = 14;
		Item.scale = 0.5f;
		Item.maxStack = 999;
		Item.value = 125000;
		Item.expert = true;
	}
}}