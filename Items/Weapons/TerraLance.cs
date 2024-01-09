using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TerraLance : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Terra Lance");
		}

	public override void SetDefaults()
	{
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 100;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 17;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 17;
		Item.knockBack = 8.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 44;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 885000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TerraLanceProjectile").Type;
		Item.shootSpeed = 11f;
	}
	
	public override bool CanUseItem(Player player)
    {
        for (int i = 0; i < 1000; ++i)
        {
            if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
            {
                return false;
            }
        }
        return true;
    }

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "StarnightLance");
		recipe.AddIngredient(null, "HellionFlowerSpear");
		recipe.AddIngredient(null, "ExsanguinationLance");
		recipe.AddIngredient(null, "LivingShard", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
