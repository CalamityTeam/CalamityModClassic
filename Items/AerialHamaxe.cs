using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class AerialHamaxe : ModItem
{
	public override void SetStaticDefaults()
 	{
 		//DisplayName.SetDefault("Aerial Hamaxe");
 	}
	
    public override void SetDefaults()
    {
        Item.damage = 25;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 48;
        Item.height = 48;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useTurn = true;
        Item.axe = 20;
        Item.hammer = 75;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7;
        Item.value = 50000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();  //Specific hamaxe 1 recipe.
        recipe.AddIngredient(null, "AerialiteBar", 6);
        recipe.AddIngredient(ItemID.SunplateBlock, 5);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.NextBool(3))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BlueTorch);
        }
    }
}}