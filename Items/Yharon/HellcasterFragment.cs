using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;
using CalamityModClassic1Point2.NPCs;

namespace CalamityModClassic1Point2.Items.Yharon {
public class HellcasterFragment : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Yharon Soul Fragment");
		//Tooltip.SetDefault("A shard of a godly soul");
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed *= 0f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.5f * num, 0.3f * num, 0.05f * num);
    }
	
	public override void SetDefaults()
	{
		Item.width = 10;
		Item.height = 14;
		Item.maxStack = 999;
		Item.value = 15000;
		Item.expert = true;
	}
}}