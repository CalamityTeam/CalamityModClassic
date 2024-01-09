using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
    [AutoloadEquip(EquipType.Wings)]
public class TarragonWings : ModItem
{
        public override void SetStaticDefaults()
        {
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(190, 9f, 2.5f);
        }

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Tarragon Wings");
        Item.width = 22;
        Item.height = 20;
        //Tooltip.SetDefault("Born of the jungle");
        Item.value = 450000;
        Item.rare = 8;
        Item.accessory = true;
    }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
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
            }
            player.noFallDmg = true;
        }
    

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 5);
        recipe.AddIngredient(null, "EnergyOrb");
        recipe.AddIngredient(ItemID.SoulofFlight, 30);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}