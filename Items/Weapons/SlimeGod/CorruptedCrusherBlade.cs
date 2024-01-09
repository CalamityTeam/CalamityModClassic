using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.SlimeGod {
public class CorruptedCrusherBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Corrupted Crusher Blade");
	}

    public override void SetDefaults()
    {
        Item.damage = 39;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 56;
        Item.height = 60;
        Item.useTime = 26;
        Item.useAnimation = 26;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 6.75f;
        Item.value = 90000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
    
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(7))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Shadowflame);
        }
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "EbonianGel", 15);
        recipe.AddIngredient(ItemID.EbonstoneBlock, 50);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddIngredient(ItemID.IronBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(null, "EbonianGel", 15);
        recipe.AddIngredient(ItemID.EbonstoneBlock, 50);
        recipe.AddIngredient(ItemID.ShadowScale, 5);
        recipe.AddIngredient(ItemID.LeadBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}}