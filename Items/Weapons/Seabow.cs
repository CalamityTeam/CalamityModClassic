using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Seabow : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Seabow");
	}

    public override void SetDefaults()
    {
        Item.damage = 14;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 22;
        Item.height = 46;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.5f;
        Item.value = 45000;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 16f;
        Item.useAmmo = 40;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VictideBar", 2);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}