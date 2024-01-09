using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;
using CalamityModClassic1Point0.Tiles;

namespace CalamityModClassic1Point0.Items.Placeables {
public class ParticleAccelerator : ModItem
{	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Particle Accelerator");
        Item.width = 36;
        Item.height = 22;
        Item.maxStack = 99;
        //AddTooltip("Used for advanced crafting");
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.useTime = 10;
        Item.useStyle = 1;
        Item.consumable = true;
        Item.value = 500000;
        Item.createTile = Mod.Find<ModTile>("ParticleAccelerator").Type;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
    	recipe.AddIngredient(null, "EssenceofEleum");
       	recipe.AddIngredient(null, "EssenceofCinder");
       	recipe.AddIngredient(null, "EssenceofChaos");
       	recipe.AddIngredient(ItemID.HallowedBar, 5);
       	recipe.AddIngredient(ItemID.SoulofMight);
       	recipe.AddIngredient(ItemID.SoulofSight);
       	recipe.AddIngredient(ItemID.SoulofFright);
       	recipe.AddTile(TileID.MythrilAnvil);
       	recipe.Register();
    }
}}