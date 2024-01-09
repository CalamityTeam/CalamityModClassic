using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Zapper : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lazinator");
			//Tooltip.SetDefault("Zap");
		}

    public override void SetDefaults()
    {
        Item.damage = 30;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 4;
        Item.width = 46;
        Item.height = 22;
        Item.useTime = 7;
        Item.useAnimation = 7;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2f;
        Item.value = 180000;
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item12;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurpleLaser;
        Item.shootSpeed = 20f;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.LaserRifle);
        recipe.AddIngredient(null, "VictoryShard", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}