using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class SkyfringePickaxe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Skyfringe Pickaxe");
	}
		
    public override void SetDefaults()
    {
        Item.damage = 17;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 19;
        Item.height = 19;
        Item.useTime = 9;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 95;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 4;
        Item.value = 97000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 7);
        recipe.AddIngredient(ItemID.SunplateBlock, 3);
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