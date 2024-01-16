using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.NPCs;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class EssenceofEleum : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/EssenceofEleum");
        return true;
    }
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.15f * num, 0.05f * num, 0.5f * num);
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Essence of Eleum");
		////Tooltip.SetDefault("The essence of cold creatures");
		Item.width = 8;
		Item.height = 20;
		Item.maxStack = 999;
		Item.value = 25000;
		Item.rare = 5;
	}
}}