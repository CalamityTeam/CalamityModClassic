using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.SlimeGod {
public class Goobow : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Goobow");
	}

    public override void SetDefaults()
    {
        Item.damage = 42;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 20;
        Item.height = 36;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.5f;
        Item.value = 105000;
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 12f;
        Item.useAmmo = 40;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 18);
        recipe.AddIngredient(ItemID.Gel, 30);
        recipe.AddIngredient(ItemID.HellstoneBar, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}