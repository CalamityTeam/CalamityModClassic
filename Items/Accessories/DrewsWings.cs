using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
[AutoloadEquip(EquipType.Wings)]
public class DrewsWings : ModItem
{
    public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Drew's Wings");
		//Tooltip.SetDefault("Absolutely Fabulous\nExcellent acceleration: 4\nExcellent flight time: 361");
	}

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 20;
        Item.value = 10000000;
        Item.expert = true;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
    	if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f)
		{
			int num59 = 4;
			if (player.direction == 1)
			{
				num59 = -40;
			}
			int num60 = Dust.NewDust(new Vector2(player.position.X + (float)(player.width / 2) + (float)num59, player.position.Y + (float)(player.height / 2) - 15f), 30, 30, DustID.GemDiamond, 0f, 0f, 100, default(Color), 2.4f);
			Main.dust[num60].noGravity = true;
			Main.dust[num60].velocity *= 0.3f;
			if (Main.rand.NextBool(10))
			{
				Main.dust[num60].fadeIn = 2f;
			}
			Main.dust[num60].shader = GameShaders.Armor.GetSecondaryShader(player.cWings, player);
		}
        player.wingTimeMax = 361;
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
        speed = 20f;
        acceleration *= 4.5f;
    }
}}