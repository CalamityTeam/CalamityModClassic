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
public class TarragonWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(190, 12, 2.5f);
        }
        public override void SetDefaults()
    {
        //Tooltip.SetDefault("Tarragon Wings");
        Item.width = 22;
        Item.height = 20;
        ////Tooltip.SetDefault("Born of the jungle\nExcellent acceleration: 2.5\nExcellent flight time: 190");
        Item.value = 450000;
        Item.rare = 8;
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
    		Dust.NewDust(player.position, player.width, player.height, 75, 0, 0, 0, Color.Red);
    		//base.WingUpdate(player, player.controlJump && player.wingTime > 0f && player.jump == 0 && player.velocity.Y != 0f && !hideVisual);
    	}
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 5);
        recipe.AddIngredient(ItemID.SoulofFlight, 30);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}