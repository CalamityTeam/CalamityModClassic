using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TerraRay : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Terra Ray");
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 55;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.width = 58;
        Item.height = 58;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 1000000;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item60;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TerraRay").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "LivingShard", 7);
        recipe.AddIngredient(null, "NightsRay");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
        recipe = CreateRecipe();
        recipe.AddIngredient(null, "LivingShard", 7);
        recipe.AddIngredient(null, "CarnageRay");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}