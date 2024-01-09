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
public class EssenceofCinder : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture = "CalamityModClassic1Point0/Items/EssenceofCinder");
        return true;
    }
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0.0001f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.3f * num, 0.3f * num, 0.05f * num);
    }
	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Essence of Cinder");
		//Tooltip.SetDefault("The essence of sky and storm creatures");
		Item.width = 6;
		Item.height = 11;
		Item.maxStack = 999;
		Item.value = 25000;
		Item.rare = 5;
	}
}}