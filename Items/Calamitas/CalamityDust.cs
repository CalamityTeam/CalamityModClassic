using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Calamitas {
public class CalamityDust : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Ashes of Calamity");
		Item.width = 26;
		Item.height = 20;
		Item.maxStack = 999;
		Item.value = 20000;
		Item.rare = 7;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.25f * num, 0.05f * num, 0.25f * num);
    }
}}