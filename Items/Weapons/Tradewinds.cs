using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Tradewinds : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Tradewinds");
			//Tooltip.SetDefault("Casts fast moving sunlight feathers");
		}

    public override void SetDefaults()
    {
        Item.damage = 17;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 7;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 13;
        Item.useAnimation = 13;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5;
        Item.value = 150000;
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item7;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TradewindsProjectile").Type;
        Item.shootSpeed = 20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 6);
        recipe.AddIngredient(ItemID.SunplateBlock, 5);
        recipe.AddIngredient(ItemID.Feather, 3);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}