using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
	[AutoloadEquip(EquipType.Wings)]
public class ElysianWings : ModItem
{
	public int flameTimer = 10;


        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(230, 16, 3);
        }

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Elysian Wings");
		////Tooltip.SetDefault("Excellent acceleration: 3\nExcellent flight time: 230\nTemporary immunity to lava\nYou generate holy flames as your wings flap");
		Item.width = 36;
		Item.height = 32;
		Item.value = 10000000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
		player.moveSpeed = 1.6f;
		player.lavaMax += 920;
		modPlayer.elysianFire = true;
		if (hideVisual)
		{
			modPlayer.elysianFire = false;
            }
            if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
            {
                Dust.NewDust(player.position, player.width, player.height, 244, 0, 0, 0);
                flameTimer--;
                if (flameTimer <= 0)
                {
                    int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -0.5f, Mod.Find<ModProjectile>("HolyFlameBomb").Type, 380, 2f, player.whoAmI, 0f, 0f);
                    flameTimer = 10;
                }
            }
            else
            {
                flameTimer = 10;
            }
        }
	
	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
       ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.85f;
        ascentWhenRising = 0.15f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
    }
}}