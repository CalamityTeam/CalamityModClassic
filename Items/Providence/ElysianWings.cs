using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Providence {
[AutoloadEquip(EquipType.Wings)]
public class ElysianWings : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Elysian Wings");
		//Tooltip.SetDefault("Excellent acceleration: 3\nExcellent flight time: 230\nTemporary immunity to lava");
	}
	
	public override void SetDefaults()
	{
		Item.width = 36;
		Item.height = 32;
		Item.value = 10000000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		player.moveSpeed = 1.6f;
		player.lavaMax += 920;
		player.wingTimeMax = 230;
		modPlayer.elysianFire = true;
		if (hideVisual)
		{
			modPlayer.elysianFire = false;
		}
	}
	
	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.85f;
        ascentWhenRising = 0.15f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
    }
	
	public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
    {
        speed = 16f;
        acceleration *= 3f;
    }
}}