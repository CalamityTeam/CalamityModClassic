using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
    [AutoloadEquip(EquipType.Wings)]
public class DrewsWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(361, 18f, 4f);
        }

        public override void SetDefaults()
    {
        //Tooltip.SetDefault("Drew's Wings");
        Item.width = 22;
        Item.height = 20;
        ////Tooltip.SetDefault("Absolutely Fabulous\nExcellent acceleration: 4\nExcellent flight time: 361");
        Item.value = 10000000;
        Item.expert = true;
        Item.accessory = true;
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
    
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
    	if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
    	{
    		Dust.NewDust(player.position, player.width, player.height, 91, 0, 0, 0, Color.White, 0.25f);
    		//base.WingUpdate(player, player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual);
			int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FabsolDust").Type, 438, 2f, player.whoAmI, 0f, 0f);
			Main.projectile[projectile1].timeLeft = 20;
    	}
    }
}}