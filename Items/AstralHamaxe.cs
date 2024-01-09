using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class AstralHamaxe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Astral Hamaxe");
	}
		
    public override void SetDefaults()
    {
        Item.damage = 70;
        Item.crit += 25;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 38;
        Item.height = 36;
        Item.useTime = 10;
        Item.useAnimation = 20;
        Item.useTurn = true;
        Item.axe = 30;
        Item.hammer = 150;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5f;
        Item.value = 350000;
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 3;
    }
    
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.ShadowbeamStaff);
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AstralBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}