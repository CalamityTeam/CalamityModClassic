using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class WulfrumBow : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Wulfrum Bow");
	}

    public override void SetDefaults()
    {
        Item.damage = 10;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 34;
        Item.height = 56;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.25f;
        Item.value = 25000;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 12f;
        Item.useAmmo = 40;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "WulfrumShard", 10);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}