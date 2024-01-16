using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.DevourerMunsters {
public class TwistingNether : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Twisting Nether");
		////Tooltip.SetDefault("From another plane of existence");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 508750;
		Item.rare = 10;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed = 0f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.5f * num, 0.1f * num, 0.7f * num);
    }
}}