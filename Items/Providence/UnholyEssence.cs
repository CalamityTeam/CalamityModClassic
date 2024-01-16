using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Providence {
public class UnholyEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
        }
        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Unholy Essence");
		////Tooltip.SetDefault("The essence of profaned creatures");
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 58750;
		Item.rare = 10;
	}
	
	public override void Update(ref float gravity, ref float maxFallSpeed)
    {
		maxFallSpeed = 0f;
        float num = (float)Main.rand.Next(90, 111) * 0.01f;
        num *= Main.essScale;
        Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 0.45f * num, 0.3f * num, 0f * num);
    }
}}