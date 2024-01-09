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
public class EnergyOrb : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/EnergyOrb");
        return true;
    }
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.01f * num, 0.4f * num, 0.01f * num);
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Energy Orb");
		Item.width = 26;
		Item.height = 26;
		Item.maxStack = 999;
		Item.value = 125000;
		Item.rare = 8;
	}
}}