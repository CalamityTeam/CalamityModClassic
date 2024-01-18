﻿using System;
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
public class ElysianTracers : ModItem
{
	public int flameTimer = 10;


        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(300, 16, 3.5f);
        }

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Elysian Tracers");
		////Tooltip.SetDefault("Counts as wings\nExcellent acceleration: 3.5\nExcellent flight time: 300\nLudicrous speed!\nGreater mobility on ice\nWater and lava walking\nTemporary immunity to lava\nYou generate holy flames as your wings flap");
		Item.width = 36;
		Item.height = 32;
		Item.value = 10000000;
		Item.expert = true;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		player.accRunSpeed = 22f;
		player.rocketBoots = 3;
		player.moveSpeed = 1.6f;
		player.iceSkate = true;
		player.waterWalk = true;
		player.fireWalk = true;
		player.lavaMax += 920;
		modPlayer.IBoots = true;
		modPlayer.elysianFire = true;
		if (hideVisual)
		{
			modPlayer.IBoots = false;
			modPlayer.elysianFire = false;
		}
    	if (player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual)
    	{
    		Dust.NewDust(player.position, player.width, player.height, 107, 0, 0, 0, Color.Green);
    		Dust.NewDust(player.position, player.width, player.height, 244, 0, 0, 0);
    		//base.WingUpdate(player, player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual);
    		flameTimer--;
			if (flameTimer <= 0)
			{
    			int projectile1 = Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -0.5f, Mod.Find<ModProjectile>("HolyFlameBomb").Type, 500, 2f, player.whoAmI, 0f, 0f);
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
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "InfinityBoots");
		recipe.AddIngredient(null, "ElysianWings");
		recipe.AddIngredient(null, "CosmiliteBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}