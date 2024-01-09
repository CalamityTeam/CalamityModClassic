using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.SlimeGod {
public class CrimsonCrusherBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Crimson Crusher Blade");
	}

    public override void SetDefaults()
    {
        Item.damage = 41;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 60;
        Item.height = 66;
        Item.useTime = 28;
        Item.useAnimation = 28;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7f;
        Item.value = 90000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
    
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(7))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
        }
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "EbonianGel", 15);
        recipe.AddIngredient(ItemID.CrimstoneBlock, 50);
        recipe.AddIngredient(ItemID.TissueSample, 5);
        recipe.AddIngredient(ItemID.IronBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(null, "EbonianGel", 15);
        recipe.AddIngredient(ItemID.CrimstoneBlock, 50);
        recipe.AddIngredient(ItemID.TissueSample, 5);
        recipe.AddIngredient(ItemID.LeadBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}