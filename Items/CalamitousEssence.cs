using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items {
public class CalamitousEssence : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Calamitous Essence");
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.1f * num, 0.1f * num, 0.1f * num);
    }
	
	public override void SetDefaults()
	{
		Item.width = 10;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 125000;
		Item.expert = true;
	}
}}