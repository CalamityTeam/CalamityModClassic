using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class AstralPike : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Astral Pike");
	}

	public override void SetDefaults()
	{
		Item.width = 44;  //The width of the .png file in pixels divided by 2.
		Item.damage = 85;  //Keep this reasonable please.
		Item.crit += 25;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 13;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 13;
		Item.knockBack = 8.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 50;  //The height of the .png file in pixels divided by 2.
		Item.value = 350000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Lime;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("AstralPike").Type;
		Item.shootSpeed = 9f;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AstralBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
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
}}
